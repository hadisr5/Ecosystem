using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataTables.AspNet.Core;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Collections;
using Seventy.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;

namespace Extensions
{
    public static class IQueryableExtensions
    {
        #region Sort
        private const string SEARCH_VALUE_SEPERATOR = ",,";
        private const string OBJECT_FEILD_SEPERATOR = ".";
        private const string EQUAL = "==";
        private const string CONTAINS = "Contains";
        private const string DESENDING = "descending";

        public static IQueryable<T> Sort<T>(this IQueryable<T> query, string field, bool isDesending = false)
        {
            if (field.IsNotNullOrWhiteSpace())

                try
                {
                    query = query.OrderBy(field + (isDesending ? $" {DESENDING}" : string.Empty));
                }
                catch (Exception ex) { }
            return query;
        }
        public static IQueryable<T> Sort<T, Tkey>(this IQueryable<T> query, Expression<Func<T, Tkey>> predictate, bool isDesending = false)
        {
            if (predictate.IsNotNull())
                if (isDesending) query = query.OrderByDescending(predictate).AsQueryable<T>();
                else query = query.OrderBy(predictate).AsQueryable<T>();
            return query;
        }
        #endregion
        #region Paging
        public static IQueryable<T> Paging<T>(this IQueryable<T> query, int pageNumber, int number, out int total)
        {
            total = -1;
            if (pageNumber < 0) pageNumber = 0;
            var skip = pageNumber * number;
            try
            {
                total = query.Count();
                query = query.Skip(skip).Take(number);
            }
            catch (Exception ex) { }
            return query;
        }
        #endregion 
        #region Filter
        public static IQueryable<T> Filter<T>(this IQueryable<T> query, string field, string value)
        {
            if (field.IsNotNullOrWhiteSpace())
                try
                {
                    query = query.Where($"{field}.Contains({value})");
                }
                catch (Exception)
                {
                    query = query.Where($"{field}=={value}");
                }

            return query;
        }

        public static IQueryable<T> Filter<T>(this IQueryable<T> query, IDataTablesRequest request) where T : class, new()
        {
            var value = request.Search.Value;
            var searchFields = request.Columns.Where(x => x.IsSearchable && x.Field.IsNotNullOrWhiteSpace()).Select(x => new SearchField { Field = x.Name ?? x.Field, Search = x.Search }).ToArray();
            try
            {
                PropertyInfo[] properties = typeof(T).GetProperties();
                var search = "";
                foreach (SearchField searchField in searchFields)
                {
                    var nestedValue = value;
                    if (nestedValue.IsNotNullOrWhiteSpace())
                    {
                        if (searchField.Search.IsNotNull() && searchField.Search.Value.IsNotNullOrWhiteSpace())
                        {
                            nestedValue = searchField.Search.Value;
                        }
                        PropertyInfo property = null;
                        if (searchField.Field.Contains('.'))
                        {
                            property = properties.Where(x => x.Name == searchField.Field.Split('.')[0]).FirstOrDefault();
                        }
                        else
                        {
                            property = properties.Where(x => x.Name == searchField.Field).FirstOrDefault();
                        }
                        if (property.IsNotNull())
                        {
                            var type = property.PropertyType;
                            var propertyName = property.Name;
                            if (searchFields.Where(x => x.Field.Contains(property.Name)).Count() > 0 && nestedValue.IsNotNullOrWhiteSpace())
                            {
                                if ((!type.IsGenericType || type.IsNullableType()) && !searchField.Field.Contains('.'))
                                {
                                    var command = CONTAINS;
                                    search += searchStringGenerate(property, nestedValue, command);
                                }
                                else
                                {
                                    var nestedName = searchField.Field.Split('.')[1];
                                    string qs = null;
                                    if (type.IsCollectionType() || type.IsListType() || type.IsEnumerableType())
                                    {
                                        var nestedType = type.GetGenericArguments()[0];
                                        var NestedProperty = nestedType.GetProperty(nestedName).PropertyType;
                                        if (NestedProperty.IsNullableType())
                                        {
                                            NestedProperty = NestedProperty.GetGenericArguments()[0];
                                        }
                                        if (NestedProperty == typeof(string))
                                        {
                                            var command = CONTAINS;
                                            qs = $"{propertyName}.Any(s=>s.{command}({value}))";
                                            query = query.Where(qs);
                                        }
                                        else if (NestedProperty.IsNumericType())
                                        {
                                            var command = EQUAL;
                                            if (nestedValue.Contains(SEARCH_VALUE_SEPERATOR))
                                            {
                                                var values = nestedValue.Split(SEARCH_VALUE_SEPERATOR);
                                                qs = $"{propertyName}.Any(s=>";
                                                for (int i = 0; i < values.Count(); i++)
                                                {
                                                    if (Decimal.TryParse(values[i], out decimal parsInt))
                                                    {
                                                        qs += $"s.{nestedName}{command}{values[i]} || ";
                                                    }
                                                }
                                                qs = qs.Remove(qs.Length - 4);
                                                qs += ")";
                                                query = query.Where(qs);
                                            }
                                            else
                                            {
                                                if (Decimal.TryParse(nestedValue, out decimal parsInt))
                                                {
                                                    qs = $"{propertyName}.Any(s=>s.{nestedName}{command}{nestedValue})";
                                                    query = query.Where(qs);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var NestedProperty = type.GetProperty(nestedName).PropertyType;
                                        if (NestedProperty == typeof(string))
                                        {
                                            var command = CONTAINS;
                                            qs = $"{propertyName}.{nestedName}.{command}(\"{value}\")";
                                            query = query.Where(qs);
                                        }
                                        else if (NestedProperty.IsNumericType())
                                        {
                                            var command = EQUAL;
                                            if (nestedValue.Contains(SEARCH_VALUE_SEPERATOR))
                                            {
                                                var values = nestedValue.Split(SEARCH_VALUE_SEPERATOR);
                                                qs = "(";
                                                for (int i = 0; i < values.Count(); i++)
                                                {
                                                    if (Decimal.TryParse(values[i], out decimal parsInt))
                                                    {
                                                        qs += $"{propertyName}.{nestedName}{command}{values[i]} || ";
                                                    }
                                                }
                                                qs = qs.Remove(qs.Length - 4);
                                                qs += ")";
                                                query = query.Where(qs);
                                            }
                                            else
                                            {
                                                if (Decimal.TryParse(nestedValue, out decimal parsInt))
                                                {
                                                    qs = $"{propertyName}.Any(s=>s.{nestedName}{command}{nestedValue})";
                                                    query = query.Where(qs);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (search.IsNotNullOrWhiteSpace())
                {
                    search = search.Remove(search.Length - 4);
                    query = query.Where(search);
                }
            }
            catch (Exception ex) { }

            return query;
        }

        private static string searchStringGenerate(PropertyInfo property, string value, string command = CONTAINS)
        {
            var type = property.PropertyType;
            var propertyName = property.Name;
            string search = "(";
            if (type.IsNullableType())
            {
                type = type.GetGenericArguments()[0];
            }
            string[] values = null;
            if (value.Contains(SEARCH_VALUE_SEPERATOR))
            {
                values = value.Split(SEARCH_VALUE_SEPERATOR);
            }
            if (type == typeof(string))
            {
                if (values.IsNotNull())
                {
                    for (int i = 0; i < values.Count(); i++)
                    {
                        search += $"{propertyName}.{command}(\"{values[i]}\") || ";
                    }
                }
                else
                {
                    search += $"{propertyName}.{command}(\"{value}\") || ";
                }
            }
            else if (type.IsNumericType())
            {
                if (values.IsNotNull())
                {
                    for (int i = 0; i < values.Count(); i++)
                    {
                        if (Decimal.TryParse(values[i], out decimal parsInt))
                        {
                            search += $"{propertyName}=={parsInt} || ";
                        }
                    }
                }
                else
                {
                    if (Decimal.TryParse(value, out decimal parsInt))
                    {
                        search += $"{propertyName}=={parsInt} || ";
                    }
                }
            }
            //ToDo
            //else if (type == typeof(DateTime))
            //{

            //}
            if (search.Length < 4)
            {
                return "";
            }
            search = search.Remove(search.Length - 4);
            search += ") || ";
            return search;
        }
        public static IQueryable<T> Filter<T>(this IQueryable<T> query, Expression<Func<T, bool>> predictate)
        {
            if (predictate.IsNotNull())
                query = query.Where(predictate);
            return query;
        }
        #endregion

        /// <summary>
        /// Sort, Paging, Filter
        /// </summary>
        public static IQueryable<T> SPF<T, Tkey>(this IQueryable<T> query, IDataTablesRequest request, Expression<Func<T, Tkey>> baseSort, out int total) where T : class, new()
        {
            query = query.Sort(baseSort);
            if (request.Columns.IsNotNull() && request.Columns.Count() > 0)
            {
                foreach (var column in request.Columns.Where(x => x.Sort.IsNotNull()))
                {
                    if (column.Sort.IsNotNull())
                    {
                        var field = column.Name ?? column.Field;
                        if (column.Sort.Direction == SortDirection.Ascending)
                        {
                            query = query.Sort(field);
                        }
                        else
                        {
                            query = query.Sort(field, true);
                        }
                    }
                }

                if (request.Columns.Where(x => x.IsSearchable && x.Field.IsNotNullOrWhiteSpace()).Count() > 0)
                {
                    query = query.Filter(request);
                }
            }
            var page = 0;
            if (request.Start > 0)
            {
                page = request.Start / request.Length;
            }
            return query.Paging(page, request.Length, out total);
        }

        public static async Task<GridResponseModel> LoadDataAsync<T, Tkey>(this IQueryable<T> query, IDataTablesRequest request, Expression<Func<T, Tkey>> baseSort, CancellationToken cancellationToken = default) where T : class, new()
        {
            var data = await query.SPF<T, Tkey>(request, baseSort, out int total).ToListAsync(cancellationToken);

            var model = new GridResponseModel()
            {
                draw = request.Draw,
                data = data,
                recordsTotal = total,
                recordsFiltered = total
            };
            return model;
        }
    }
    public class SearchField
    {
        public string Field { get; set; }
        public ISearch Search { get; set; }
    }
}
