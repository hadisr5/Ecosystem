using System;
using System.Linq;
using Seventy.Data;
using System.Threading;
using Seventy.Repository.Core;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Seventy.DomainClass.EDU;
using Seventy.DomainClass.EDU.TrainingEval;

namespace Seventy.Service.EDU.TrainingEval
{
    public class TrainingEvalIndexService : BaseService.BaseService<TrainingEvalIndex>, ITrainingEvalIndexService
    {
        public TrainingEvalIndexService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<TrainingEvalIndex> Table() => _uow.TrainingEvalIndex.Table.AsEnumerable();
        public override IEnumerable<TrainingEvalIndex> TableNoTracking() => _uow.TrainingEvalIndex.TableNoTracking.AsEnumerable();

        public override async Task<TrainingEvalIndex> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.TrainingEvalIndex.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(TrainingEvalIndex entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.TrainingEvalIndex.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<TrainingEvalIndex> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.TrainingEvalIndex.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<TrainingEvalIndex>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<TrainingEvalIndex, bool>> filter = null,
            Func<IQueryable<TrainingEvalIndex>, IOrderedQueryable<TrainingEvalIndex>> orderBy = null)
        {
            return await _uow.TrainingEvalIndex.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }

        public override async Task<TrainingEvalIndex> InsertAsync(TrainingEvalIndex entity, CancellationToken cancellationToken)
        {
            var result = await _uow.TrainingEvalIndex.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<TrainingEvalIndex> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.TrainingEvalIndex.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<TrainingEvalIndex> UpdateAsync(TrainingEvalIndex entity, CancellationToken cancellationToken)
        {
            var result = await _uow.TrainingEvalIndex.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<TrainingEvalIndex> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.TrainingEvalIndex.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public async Task<PagedList<TrainingEvalIndexViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<TrainingEvalIndexViewModel, bool>> filter = null, Func<IQueryable<TrainingEvalIndexViewModel>, IOrderedQueryable<TrainingEvalIndexViewModel>> orderBy = null)
        {
            try
            {
                var items = _uow.TrainingEvalIndex.TableNoTracking;

                var query = from item in items
                            select new TrainingEvalIndexViewModel
                            {
                                ID = item.ID,
                                Description = item.Description,
                                IsActive = item.IsActive,
                                RegDate = item.RegDate,
                                RegUserID = item.RegUserID,
                                TargetID = item.TargetID,
                                TargetType = item.TargetType,
                                Title = item.Title,
                                TargetName = ""
                            };

                foreach (var item in query)
                {
                    if (item.TargetType.Equals("دوره"))
                    {
                        var witch = await _uow.Course.TableNoTracking
                            .SingleOrDefaultAsync(a => a.ID.Equals(item.TargetID));

                        item.TargetName = witch.Title;
                    }
                    else if (item.TargetType.Equals("درس"))
                    {
                        var witch = await _uow.Lesson.TableNoTracking
                            .SingleOrDefaultAsync(a => a.ID.Equals(item.TargetID));

                        item.TargetName = witch.Title;
                    }
                    else if (item.TargetType.Equals("محتوی"))
                    {
                        var witch = await _uow.TrainingContent.TableNoTracking
                            .SingleOrDefaultAsync(a => a.ID.Equals(item.TargetID));

                        item.TargetName = witch.Title;
                    }
                    else if (item.TargetType.Equals("مدرس"))
                    {
                        var witch = await _uow.TeacherLesson.TableNoTracking
                            .SingleOrDefaultAsync(a => a.ID.Equals(item.TargetID));

                        if (witch == null)
                            continue;

                        {
                            var teacher = await _uow.UserProfiles.TableNoTracking
                                .SingleOrDefaultAsync(a =>
                                    a.UserID.Equals(witch.TeacherID));


                            item.TargetName = teacher.FirstName + " " + teacher.LastName;
                        }
                    }
                }

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return await PagedList<TrainingEvalIndexViewModel>.ToPagedList(orderBy(query),
                            genericPagingParameters.PageNumber,
                            genericPagingParameters.PageSize);
                }

                return await PagedList<TrainingEvalIndexViewModel>.ToPagedList(query,
                    genericPagingParameters.PageNumber,
                    genericPagingParameters.PageSize);
            }
            catch
            {
                return null;
            }
        }
    }
}
