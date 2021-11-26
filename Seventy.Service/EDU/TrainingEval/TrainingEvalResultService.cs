using System;
using System.Linq;
using Seventy.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Seventy.DomainClass.EDU;
using Seventy.Repository.Core;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Seventy.DomainClass.EDU.TrainingEval;

namespace Seventy.Service.EDU.TrainingEval
{
    public class TrainingEvalResultService : BaseService.BaseService<TrainingEvalResult>, ITrainingEvalResultService
    {
        public TrainingEvalResultService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<TrainingEvalResult> Table() => _uow.TrainingEvalResult.Table.AsEnumerable();
        public override IEnumerable<TrainingEvalResult> TableNoTracking() => _uow.TrainingEvalResult.TableNoTracking.AsEnumerable();

        public override async Task<TrainingEvalResult> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.TrainingEvalResult.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(TrainingEvalResult entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.TrainingEvalResult.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<TrainingEvalResult> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.TrainingEvalResult.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<TrainingEvalResult>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<TrainingEvalResult, bool>> filter = null,
            Func<IQueryable<TrainingEvalResult>, IOrderedQueryable<TrainingEvalResult>> orderBy = null)
        {
            return await _uow.TrainingEvalResult.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }

        public override async Task<TrainingEvalResult> InsertAsync(TrainingEvalResult entity, CancellationToken cancellationToken)
        {
            var checkNotExist = await _uow.TrainingEvalResult.TableNoTracking
                .AnyAsync(a => a.UserID.Equals(entity.UserID) &&
                               a.TrainingEvalIndexID.Equals(entity.TrainingEvalIndexID) &&
                               a.Result != 0, cancellationToken);

            if (checkNotExist)
                return null;

            var result = await _uow.TrainingEvalResult.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<TrainingEvalResult> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.TrainingEvalResult.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<TrainingEvalResult> UpdateAsync(TrainingEvalResult entity, CancellationToken cancellationToken)
        {
            var result = await _uow.TrainingEvalResult.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<TrainingEvalResult> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.TrainingEvalResult.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public async Task<PagedList<TrainingEvalResultViewModel>> GetAllPaginatedAsync(
            GenericPagingParameters genericPagingParameters,
            Expression<Func<TrainingEvalResultViewModel, bool>> filter = null,
            Func<IQueryable<TrainingEvalResultViewModel>,
            IOrderedQueryable<TrainingEvalResultViewModel>> orderBy = null)
        {
            try
            {
                var items = _uow.TrainingEvalResult.TableNoTracking;
                var indexes = _uow.TrainingEvalIndex.TableNoTracking;
                var users = _uow.UserProfiles.TableNoTracking;

                var query = from item in items
                            from index in indexes.Where(x => x.ID == item.TrainingEvalIndexID)
                            from user in users.Where(x => x.UserID == item.UserID)
                            select new TrainingEvalResultViewModel
                            {
                                ID = item.ID,
                                Description = item.Description,
                                IsActive = item.IsActive,
                                RegDate = item.RegDate,
                                RegUserID = item.RegUserID,
                                UserID = item.UserID,
                                Result = item.Result,
                                UserName = user.FirstName + " " + user.LastName,
                                TrainingEvalIndexID = item.TrainingEvalIndexID,
                                TrainingEvalIndexTitle = index.Title
                            };

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return await PagedList<TrainingEvalResultViewModel>.ToPagedList(orderBy(query),
                            genericPagingParameters.PageNumber,
                            genericPagingParameters.PageSize);
                }

                return await PagedList<TrainingEvalResultViewModel>.ToPagedList(query,
                    genericPagingParameters.PageNumber,
                    genericPagingParameters.PageSize);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<TrainingEvalIndexViewModel>> GetByType(int targetId, string targetType, CancellationToken cancellationToken)
        {
            try
            {
                var items = _uow.TrainingEvalIndex
                    .TableNoTracking
                    .Where(a => a.TargetID.Equals(targetId) &&
                                a.TargetType.Equals(targetType))
                    .Select(item => new TrainingEvalIndexViewModel
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
                    });

                foreach (var item in items)
                {
                    if (item.TargetType.Equals("دوره"))
                    {
                        var witch = await _uow.Course.TableNoTracking
                            .SingleOrDefaultAsync(a => a.ID.Equals(item.TargetID), cancellationToken);

                        item.TargetName = witch.Title;
                    }
                    else if (item.TargetType.Equals("درس"))
                    {
                        var witch = await _uow.Lesson.TableNoTracking
                            .SingleOrDefaultAsync(a => a.ID.Equals(item.TargetID), cancellationToken);

                        item.TargetName = witch.Title;
                    }
                    else if (item.TargetType.Equals("محتوی"))
                    {
                        var witch = await _uow.TrainingContent.TableNoTracking
                            .SingleOrDefaultAsync(a => a.ID.Equals(item.TargetID), cancellationToken);

                        item.TargetName = witch.Title;
                    }
                    else if (item.TargetType.Equals("مدرس"))
                    {
                        var witch = await _uow.TeacherLesson.TableNoTracking
                            .SingleOrDefaultAsync(a => a.ID.Equals(item.TargetID), cancellationToken);

                        if (witch == null)
                            continue;

                        {
                            var teacher = await _uow.UserProfiles.TableNoTracking
                                .SingleOrDefaultAsync(a =>
                                    a.UserID.Equals(witch.TeacherID), cancellationToken);

                            item.TargetName = teacher.FirstName + " " + teacher.LastName;
                        }
                    }
                }

                return await items.ToListAsync(cancellationToken);
            }
            catch
            {
                return null;
            }
        }
    }
}
