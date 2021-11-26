using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Seventy.ViewModel.EDU.CertificateUser;

namespace Seventy.Service.EDU.CertificateUser
{
    public class CertificateUserService : BaseService.BaseService<DomainClass.EDU.CertificateUser>, ICertificateUserService
    {
        public CertificateUserService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.CertificateUser> Table() => _uow.CertificateUser.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.CertificateUser> TableNoTracking() => _uow.CertificateUser.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.CertificateUser> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.CertificateUser.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.CertificateUser entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.CertificateUser.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.CertificateUser> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.CertificateUser.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.CertificateUser> InsertAsync(DomainClass.EDU.CertificateUser entity, CancellationToken cancellationToken)
        {
            var result = await _uow.CertificateUser.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.CertificateUser> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.CertificateUser.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;

        }

        public override async Task<DomainClass.EDU.CertificateUser> UpdateAsync(DomainClass.EDU.CertificateUser entity, CancellationToken cancellationToken)
        {
            var result = await _uow.CertificateUser.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.CertificateUser> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.CertificateUser.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.EDU.CertificateUser>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.CertificateUser, bool>> filter = null, Func<IQueryable<DomainClass.EDU.CertificateUser>, IOrderedQueryable<DomainClass.EDU.CertificateUser>> orderBy = null)
        {
            return await _uow.CertificateUser.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }

        public async Task<PagedList<CertificateUserViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters
               , Expression<Func<CertificateUserViewModel, bool>> filter = null,
               Func<IQueryable<CertificateUserViewModel>
                   , IOrderedQueryable<CertificateUserViewModel>> orderBy = null)
        {
            try
            {
                var items = _uow.CertificateUser.TableNoTracking;
                var cert = _uow.Certificate.TableNoTracking;
                var course = _uow.Course.TableNoTracking;
                var courseGroups = _uow.CourseGroups.TableNoTracking;
                var userProfiles = _uow.UserProfiles.TableNoTracking;

                var query = from item in items
                            from c in course.Where(x => x.ID == item.CourseID)
                            from cg in courseGroups.Where(x => x.ID == item.CourseGroupID)
                            from ce in cert.Where(x => x.ID == item.CertificateID)
                            from prof in userProfiles.Where(x => x.UserID == item.UserID).DefaultIfEmpty()
                            select new CertificateUserViewModel
                            {
                                ID = item.ID,
                                Description = item.Description,
                                IsActive = item.IsActive,
                                RegDate = item.RegDate,
                                RegUserID = item.RegUserID,
                                UserID = item.UserID,
                                UserName = prof.FirstName + " " + prof.LastName,
                                Grade = item.Grade,
                                CourseName = c.Title,
                                CourseGroupName = cg.Title,
                                CertificateName = ce.Title,
                                CourseID = item.CourseID,
                                CourseGroupID = item.CourseGroupID,
                                CertificateID = item.CertificateID
                            };

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return await PagedList<CertificateUserViewModel>.ToPagedList(orderBy(query),
                            genericPagingParameters.PageNumber,
                            genericPagingParameters.PageSize);
                }
                else
                {
                    return await PagedList<CertificateUserViewModel>.ToPagedList(query,
                            genericPagingParameters.PageNumber,
                            genericPagingParameters.PageSize);
                }

            }
            catch { return null; }
        }

    }
}
