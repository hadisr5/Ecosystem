using Seventy.Data;
using Seventy.Repository.Core;
using Seventy.Service.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Seventy.Service.Users;
using Seventy.ViewModel.Core;
using Microsoft.EntityFrameworkCore;

namespace Seventy.Service.Core.Documents
{
    public class DocumentsService : BaseService<DomainClass.Core.Documents>, IDocumentsService
    {
        private readonly IUserManager _userManager;

        public DocumentsService(IUnitOfWork uow, IUserManager userManager) : base(uow)
        {
            _userManager = userManager;
        }

        public override IEnumerable<DomainClass.Core.Documents> Table() => _uow.Documents.Table.AsEnumerable();
        public override IEnumerable<DomainClass.Core.Documents> TableNoTracking() => _uow.Documents.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.Core.Documents> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            var result = await _uow.Documents.GetByIDAsync(cancellationToken, ids);
            return result;
        }

        public override async Task<bool> DeleteAsync(DomainClass.Core.Documents entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Documents.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.Core.Documents> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Documents.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.Documents> InsertAsync(DomainClass.Core.Documents entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Documents.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.Core.Documents> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Documents.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.Documents> UpdateAsync(DomainClass.Core.Documents entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Documents.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.Core.Documents> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Documents.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.Core.Documents>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.Core.Documents, bool>> filter = null, Func<IQueryable<DomainClass.Core.Documents>, IOrderedQueryable<DomainClass.Core.Documents>> orderBy = null)
        {
            return await _uow.Documents.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }

        public async Task<PagedList<DocumentsViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters)
        {
            try
            {
                var user = await _userManager
                    .GetCurrentUserAsync(new CancellationToken());

                IQueryable<DocumentsViewModel> query;
                var userRoles = await _uow.UserRole.TableNoTracking.Include(i => i.Role).Where(w => w.UserID == user.ID).ToListAsync();
                //if (user.RoleID.Equals(2))
                if (userRoles.Where(w => w.RoleID.Equals(2)).FirstOrDefault() != null)
                {
                    query = _uow.Documents.TableNoTracking
                       .Select(a => new DocumentsViewModel
                       {
                           ID = a.ID,
                           IsActive = a.IsActive,
                           Description = a.Description,
                           RegUserID = a.RegUserID,
                           RegDate = a.RegDate,
                           FilePath = a.FilePath,
                           DocType = a.DocType,
                           Section = a.Section,
                           UserId = a.UserID
                       });
                }
                else
                {
                    query = _uow.Documents.TableNoTracking
                       .Where(a => a.UserID.Equals(1))
                       .Select(a => new DocumentsViewModel
                       {
                           ID = a.ID,
                           IsActive = a.IsActive,
                           Description = a.Description,
                           RegUserID = a.RegUserID,
                           RegDate = a.RegDate,
                           FilePath = a.FilePath,
                           DocType = a.DocType,
                           Section = a.Section,
                           UserId = a.UserID
                       });
                }

                return await PagedList<DocumentsViewModel>.ToPagedList(query,
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
