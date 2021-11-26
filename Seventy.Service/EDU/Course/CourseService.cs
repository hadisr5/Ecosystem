using Seventy.Data;
using Seventy.Repository.Core;
using Seventy.ViewModel.EDU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Seventy.Common.Enums;
using Seventy.Service.Users;
using Seventy.ViewModel.EDU.Course;
using DataTables.AspNet.Core;
using Extensions;
using Seventy.ViewModel;

namespace Seventy.Service.EDU.Course
{
    public class CourseService : BaseService.BaseService<DomainClass.EDU.Course.Course>, ICourseService
    {
        private readonly IUserManager _userManager;

        public CourseService(IUnitOfWork uow, IUserManager userManager) : base(uow)
        {
            _userManager = userManager;
        }

        public override IEnumerable<DomainClass.EDU.Course.Course> Table() => _uow.Course.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.Course.Course> TableNoTracking() => _uow.Course.TableNoTracking.AsEnumerable();
        public IEnumerable<DomainClass.EDU.Course.Course> TableNoTracking(int reguserID)
        {
            return _uow.Course.TableNoTracking.Where(w => w.RegUserID == reguserID && w.IsActive == true).OrderByDescending(o => o.ID).AsEnumerable();
        }

        public override async Task<DomainClass.EDU.Course.Course> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.Course.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.Course.Course entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Course.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.Course.Course> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Course.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Course.Course> InsertAsync(DomainClass.EDU.Course.Course entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Course.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.Course.Course> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Course.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Course.Course> UpdateAsync(DomainClass.EDU.Course.Course entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Course.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.Course.Course> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Course.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public async Task<PagedList<CourseViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters
            , Expression<Func<CourseViewModel, bool>> filter = null, Func<IQueryable<CourseViewModel>
                , IOrderedQueryable<CourseViewModel>> orderBy = null)
        {
            try
            {
                var courseList = _uow.Course.TableNoTracking;
                var courseCategories = _uow.CourseCategory.TableNoTracking;
                var files = _uow.File.TableNoTracking;

                var query = from c in courseList
                            from cc in courseCategories.Where(x => x.ID == c.CategoryID)
                            from f in files.Where(x => x.ID == c.PhotoFileID).DefaultIfEmpty()
                            select new CourseViewModel
                            {
                                ID = c.ID,
                                CategoryID = c.CategoryID,
                                CategoryTitle = cc.SecondaryCat,
                                CourseType = c.CourseType,
                                Achievements = c.Achievements,
                                Duration = c.Duration,
                                HozoriType = c.HozoriType,
                                Price = c.Price,
                                PublishState = c.PublishState,
                                RequiredDocuments = c.RequiredDocuments,
                                Title = c.Title,
                                PhotoURL = "",
                                Description = c.Description,
                                IsActive = c.IsActive,
                                RegDate = c.RegDate,
                                RegUserID = c.RegUserID
                            };

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return await PagedList<CourseViewModel>.ToPagedList(orderBy(query),
                            genericPagingParameters.PageNumber,
                            genericPagingParameters.PageSize);
                }
                else
                {
                    return await PagedList<CourseViewModel>.ToPagedList(query,
                            genericPagingParameters.PageNumber,
                            genericPagingParameters.PageSize);
                }
            }
            catch
            {
                return null;
            }

        }

        public async Task<PagedList<CourseViewModel>> GetByTeacherAsync(CourseEnum type, GenericPagingParameters genericPagingParameters
            , Expression<Func<CourseViewModel, bool>> filter = null, Func<IQueryable<CourseViewModel>
                , IOrderedQueryable<CourseViewModel>> orderBy = null)
        {
            try
            {
                var user = await _userManager
                    .GetCurrentUserAsync(new CancellationToken());

                IQueryable<DomainClass.EDU.Course.Course> items;

                var courses = await _uow.TermLesson.TableNoTracking
                    .Where(a => a.TeacherID.Equals(user.ID))
                    .Select(a => a.CourseID)
                    .ToListAsync();

                switch (type)
                {
                    case CourseEnum.Long:
                        items = _uow.Course
                            .TableNoTracking
                            .Where(a => a.CourseType.Equals("بلند مدت") &&
                               courses.Contains((int)a.ID));
                        break;

                    case CourseEnum.Single:
                        items = _uow.Course
                            .TableNoTracking
                            .Where(a => a.CourseType.Equals("تک مهارتی") &&
                                        courses.Contains((int)a.ID));
                        break;

                    default:
                        items = _uow.Course
                            .TableNoTracking;
                        break;
                }

                var cats = _uow.CourseCategory.TableNoTracking;

                var query =
                    from e in items
                    from cat in cats.Where(a => a.ID.Equals(e.CategoryID))
                    select new CourseViewModel
                    {
                        ID = e.ID,
                        Description = e.Description,
                        IsActive = e.IsActive,
                        RegDate = e.RegDate,
                        RegUserID = e.RegUserID,
                        CourseType = e.CourseType,
                        Achievements = e.Achievements,
                        CategoryID = e.CategoryID,
                        CategoryTitle = cat.PrimaryCat + " (" + cat.SecondaryCat + ")",
                        CourseTeacher = "",
                        Duration = e.Duration,
                        HozoriType = e.HozoriType,
                        PhotoURL = e.PhotoFileID.ToString(),
                        Price = e.Price,
                        PublishState = e.PublishState,
                        RequiredDocuments = e.RequiredDocuments,
                        Title = e.Title,
                        PhotoFileID = e.PhotoFileID
                    };

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return await PagedList<CourseViewModel>.ToPagedList(orderBy(query),
                        genericPagingParameters.PageNumber,
                        genericPagingParameters.PageSize);
                }
                else
                {
                    return await PagedList<CourseViewModel>.ToPagedList(query,
                        genericPagingParameters.PageNumber,
                        genericPagingParameters.PageSize);
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<PagedList<CourseViewModel>> GetByTypeAsync(CourseEnum type, GenericPagingParameters genericPagingParameters
            , Expression<Func<CourseViewModel, bool>> filter = null, Func<IQueryable<CourseViewModel>
                , IOrderedQueryable<CourseViewModel>> orderBy = null)
        {
            try
            {
                IQueryable<DomainClass.EDU.Course.Course> items;


                switch (type)
                {
                    case CourseEnum.Long:
                        items = _uow.Course
                            .TableNoTracking
                            .Where(a => a.CourseType.Equals("بلند مدت"));
                        break;

                    case CourseEnum.Single:
                        items = _uow.Course
                            .TableNoTracking
                            .Where(a => a.CourseType.Equals("تک مهارتی"));
                        break;

                    default:
                        items = _uow.Course
                            .TableNoTracking
                            .Where(a => a.CourseType.Equals("بلند مدت"));
                        break;
                }

                var cats = _uow.CourseCategory.TableNoTracking;

                var query =
                    from e in items
                    from cat in cats.Where(a => a.ID.Equals(e.CategoryID))
                    select new CourseViewModel
                    {
                        ID = e.ID,
                        Description = e.Description,
                        IsActive = e.IsActive,
                        RegDate = e.RegDate,
                        RegUserID = e.RegUserID,
                        CourseType = e.CourseType,
                        Achievements = e.Achievements,
                        CategoryID = e.CategoryID,
                        CategoryTitle = cat.PrimaryCat + " (" + cat.SecondaryCat + ")",
                        CourseTeacher = "",
                        Duration = e.Duration,
                        HozoriType = e.HozoriType,
                        PhotoURL = e.PhotoFileID.ToString(),
                        Price = e.Price,
                        PublishState = e.PublishState,
                        RequiredDocuments = e.RequiredDocuments,
                        Title = e.Title
                    };

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return await PagedList<CourseViewModel>.ToPagedList(orderBy(query),
                        genericPagingParameters.PageNumber,
                        genericPagingParameters.PageSize);
                }
                else
                {
                    return await PagedList<CourseViewModel>.ToPagedList(query,
                        genericPagingParameters.PageNumber,
                        genericPagingParameters.PageSize);
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<PagedList<CourseViewModel>> GetCustomCourseAsync(CourseEnum type, GenericPagingParameters genericPagingParameters
            , Expression<Func<CourseViewModel, bool>> filter = null, Func<IQueryable<CourseViewModel>
                , IOrderedQueryable<CourseViewModel>> orderBy = null)
        {
            try
            {
                var user = await _userManager
                    .GetCurrentUserAsync(new CancellationToken());

                IQueryable<DomainClass.EDU.Course.Course> items;
                var userRoles = await _uow.UserRole.TableNoTracking.Include(i => i.Role).Where(w => w.UserID == user.ID).ToListAsync();

                //if (user.RoleID.Equals(2))
                if (userRoles.Where(w => w.RoleID.Equals(2)).FirstOrDefault() != null)
                {
                    switch (type)
                    {
                        case CourseEnum.Long:
                            items = _uow.Course
                                .TableNoTracking
                                .Where(a => a.CourseType.Equals("بلند مدت"));
                            break;

                        case CourseEnum.Single:
                            items = _uow.Course
                                .TableNoTracking
                                .Where(a => a.CourseType.Equals("تک مهارتی"));
                            break;

                        default:
                            items = _uow.Course
                                .TableNoTracking;
                            break;
                    }
                }
                else
                {
                    var ids = await _uow.CourseRegistration.TableNoTracking
                        .Where(a => a.UserID.Equals(user.ID))
                        .Select(a => a.CourseID)
                        .ToListAsync();

                    switch (type)
                    {
                        case CourseEnum.Long:
                            items = _uow.Course
                                .TableNoTracking
                                .Where(a => a.CourseType.Equals("بلند مدت") &&
                                            ids.Contains((int)a.ID));
                            break;

                        case CourseEnum.Single:
                            items = _uow.Course
                                .TableNoTracking
                                .Where(a => a.CourseType.Equals("تک مهارتی") &&
                                            ids.Contains((int)a.ID));
                            break;

                        default:
                            items = _uow.Course
                                .TableNoTracking
                                .Where(a =>
                                            ids.Contains((int)a.ID));
                            break;
                    }
                }

                var cats = _uow.CourseCategory.TableNoTracking;
                var users = _uow.UserProfiles.TableNoTracking;

                var query =
                    from e in items
                    from cat in cats.Where(a => a.ID.Equals(e.CategoryID))
                    select new CourseViewModel
                    {
                        ID = e.ID,
                        Description = e.Description,
                        IsActive = e.IsActive,
                        RegDate = e.RegDate,
                        RegUserID = e.RegUserID,
                        CourseType = e.CourseType,
                        Achievements = e.Achievements,
                        CategoryID = e.CategoryID,
                        CategoryTitle = cat.PrimaryCat + " (" + cat.SecondaryCat + ")",
                        CourseTeacher = "",
                        Duration = e.Duration,
                        HozoriType = e.HozoriType,
                        PhotoURL = e.PhotoFileID.ToString(),
                        Price = e.Price,
                        PublishState = e.PublishState,
                        RequiredDocuments = e.RequiredDocuments,
                        Title = e.Title
                    };

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return await PagedList<CourseViewModel>.ToPagedList(orderBy(query),
                        genericPagingParameters.PageNumber,
                        genericPagingParameters.PageSize);
                }
                else
                {
                    return await PagedList<CourseViewModel>.ToPagedList(query,
                        genericPagingParameters.PageNumber,
                        genericPagingParameters.PageSize);
                }
            }
            catch
            {
                return null;
            }
            //return null;

        }

        public async Task<GridResponseModel> GetCustomCourseAsync(CourseEnum type, IDataTablesRequest request)
        {
            try
            {
                var user = await _userManager
                    .GetCurrentUserAsync(new CancellationToken());

                IQueryable<DomainClass.EDU.Course.Course> items;
                var userRoles = await _uow.UserRole.TableNoTracking.Include(i => i.Role).Where(w => w.UserID == user.ID).ToListAsync();

                //if (user.RoleID.Equals(2))
                if (userRoles.Where(w => w.RoleID.Equals(2)).FirstOrDefault() != null)
                {
                    switch (type)
                    {
                        case CourseEnum.Long:
                            items = _uow.Course
                                .TableNoTracking
                                .Where(a => a.CourseType.Equals("بلند مدت"));
                            break;

                        case CourseEnum.Single:
                            items = _uow.Course
                                .TableNoTracking
                                .Where(a => a.CourseType.Equals("تک مهارتی") || a.CourseType.Equals("تک مهارت"));
                            break;

                        default:
                            items = _uow.Course
                                .TableNoTracking;
                            break;
                    }
                }
                else
                {
                    var ids = await _uow.CourseRegistration.TableNoTracking
                        .Where(a => a.UserID.Equals(user.ID))
                        .Select(a => a.CourseID)
                        .ToListAsync();

                    switch (type)
                    {
                        case CourseEnum.Long:
                            items = _uow.Course
                                .TableNoTracking
                                .Where(a => a.CourseType.Equals("بلند مدت") &&
                                            ids.Contains((int)a.ID));
                            break;

                        case CourseEnum.Single:
                            items = _uow.Course
                                .TableNoTracking
                                .Where(a => a.CourseType.Equals("تک مهارتی") || a.CourseType.Equals("تک مهارت") &&
                                            ids.Contains((int)a.ID));
                            break;

                        default:
                            items = _uow.Course
                                .TableNoTracking
                                .Where(a =>
                                            ids.Contains((int)a.ID));
                            break;
                    }
                }

                var cats = _uow.CourseCategory.TableNoTracking;
                var users = _uow.UserProfiles.TableNoTracking;

                var query =
                    from e in items
                    .SPF(request, x => x.ID, out int total)
                    from cat in cats.Where(a => a.ID.Equals(e.CategoryID))
                    select new CourseViewModel
                    {
                        ID = e.ID,
                        Description = e.Description,
                        IsActive = e.IsActive,
                        RegDate = e.RegDate,
                        RegUserID = e.RegUserID,
                        CourseType = e.CourseType,
                        Achievements = e.Achievements,
                        CategoryID = e.CategoryID,
                        CategoryTitle = cat.PrimaryCat + " (" + cat.SecondaryCat + ")",
                        CourseTeacher = "",
                        Duration = e.Duration,
                        HozoriType = e.HozoriType,
                        PhotoURL = e.PhotoFileID.ToString(),
                        Price =e.Price,// e.Price.ToString("N2"),
                        PublishState = e.PublishState,
                        RequiredDocuments = e.RequiredDocuments,
                        Title = e.Title
                    };
                var data = await query.ToListAsync();
                var model = new GridResponseModel()
                {
                    draw = request.Draw,
                    data = data,
                    recordsTotal = total,
                    recordsFiltered = total
                };
                return model;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public async Task<PagedList<CourseViewModel>> GetLongTermPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<CourseViewModel, bool>> filter = null, Func<IQueryable<CourseViewModel>, IOrderedQueryable<CourseViewModel>> orderBy = null)
        {
            return null;
            //try
            //{
            //    var courseList = _uow.Course.TableNoTracking;
            //    var courseCategories = _uow.CourseCategory.TableNoTracking;
            //    var termlesson = _uow.TermLesson.TableNoTracking;

            //    var query = from c in courseList
            //                join a in courseCategories on c.CategoryId equals a.Id
            //                join p in termlesson on c.Id equals
            //                from f in _uow.File.TableNoTracking.Where(x => x.Id == c.PhotoFileID).DefaultIfEmpty()
            //                select new CourseViewModel
            //                {
            //                    ID = c.ID,
            //                    CategoryID = c.CategoryID,
            //                    CategoryTitle = cc.SecondaryCat,
            //                    CourseType = c.CourseType,
            //                    Achievements = c.Achievements,
            //                    Duration = c.Duration,
            //                    HozoriType = c.HozoriType,
            //                    Price = c.Price,
            //                    PublishState = c.PublishState,
            //                    RequiredDocuments = c.RequiredDocuments,
            //                    Title = c.Title,
            //                    PhotoURL = "",
            //                    Description = c.Description,
            //                    IsActive = c.IsActive,
            //                    RegDate = c.RegDate,
            //                    RegUserID = c.RegUserID
            //                };

            //    if (filter != null)
            //    {
            //        query = query.Where(filter);
            //    }

            //    if (orderBy != null)
            //    {
            //        return await PagedList<CourseViewModel>.ToPagedList(orderBy(query),
            //                genericPagingParameters.PageNumber,
            //                genericPagingParameters.PageSize);
            //    }
            //    else
            //    {
            //        return await PagedList<CourseViewModel>.ToPagedList(query,
            //                genericPagingParameters.PageNumber,
            //                genericPagingParameters.PageSize);
            //    }
            //}
            //catch
            //{
            //    return null;
            //}
        }

        public override async Task<PagedList<DomainClass.EDU.Course.Course>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.Course.Course, bool>> filter = null, Func<IQueryable<DomainClass.EDU.Course.Course>, IOrderedQueryable<DomainClass.EDU.Course.Course>> orderBy = null)
        {
            return await _uow.Course.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
