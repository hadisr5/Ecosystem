using Microsoft.EntityFrameworkCore;
using Seventy.Data;
using Seventy.DomainClass.EDU.Course;
using Seventy.Repository.Core;
using Seventy.Service.Users;
using Seventy.ViewModel.EDU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.UserCourseGroup
{
    public class CourseRegistrationService : BaseService.BaseService<DomainClass.EDU.Course.CourseRegistration>, ICourseRegistrationService
    {
        private readonly IUserManager _UserManagerService;

        public CourseRegistrationService(IUnitOfWork uow, IUserManager userManagerService) : base(uow)
        {
            this._UserManagerService = userManagerService;
        }

        public override IEnumerable<DomainClass.EDU.Course.CourseRegistration> Table() => _uow.CourseRegistration.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.Course.CourseRegistration> TableNoTracking() => _uow.CourseRegistration.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.Course.CourseRegistration> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.CourseRegistration.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.Course.CourseRegistration entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.CourseRegistration.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.Course.CourseRegistration> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.CourseRegistration.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Course.CourseRegistration> InsertAsync(DomainClass.EDU.Course.CourseRegistration entity, CancellationToken cancellationToken)
        {
            var result = await _uow.CourseRegistration.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.Course.CourseRegistration> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.CourseRegistration.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Course.CourseRegistration> UpdateAsync(DomainClass.EDU.Course.CourseRegistration entity, CancellationToken cancellationToken)
        {
            var result = await _uow.CourseRegistration.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.Course.CourseRegistration> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.CourseRegistration.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }


        public async Task<IEnumerable<CourseEnrollmentViewModel>> GetCourseListForEnrollment(string courseType = "بلند مدت")
        {
            var currentUserID = _UserManagerService.GetCurrentUserID();
            var UserCourseGroups = _uow.CourseRegistration.TableNoTracking.Where(x => x.UserID == currentUserID);
            var courses = _uow.Course.TableNoTracking.Where(x => x.CourseType == courseType);
            var courseGroups = _uow.CourseGroups.TableNoTracking;
            var users = _uow.UserProfiles.TableNoTracking;
            var files = _uow.File.TableNoTracking;

            var l = from c in courses
                    from cg in courseGroups.Where(x => x.CourseID == c.ID)
                    from uc in UserCourseGroups.Where(x => x.CourseGroupID == cg.ID).DefaultIfEmpty()
                    from f in files.Where(x => x.ID == c.PhotoFileID).DefaultIfEmpty()
                    select new CourseEnrollmentViewModel
                    {
                        PhotoPath = "",
                        Title = c.Title,
                        Duration = c.Duration,
                        Price = c.Price,
                        CourseType = c.CourseType,
                        IsRegistered = uc != null
                    };
            return await l.ToListAsync();
        }

        public override async Task<PagedList<CourseRegistration>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<CourseRegistration, bool>> filter = null, Func<IQueryable<CourseRegistration>, IOrderedQueryable<CourseRegistration>> orderBy = null)
        {
            return await _uow.CourseRegistration.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }

        public async Task<int> CheckForRegistration(CourseRegistration courseRegistration)
        {
            int userWallet = 0; // check in future
            int coursePrice = await _uow.CourseRegistration.RegisterationTotalPrice(courseRegistration);
            return coursePrice < userWallet ? 0 : coursePrice - userWallet;
        }

    }
}
