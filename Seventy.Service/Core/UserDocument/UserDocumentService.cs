using System;
using System.Linq;
using Seventy.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Seventy.Repository.Core;
using Seventy.DomainClass.Core;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Seventy.Service.Core.UserDocument
{
    public class UserDocumentService : BaseService.BaseService<UserDocuments>, IUserDocumentService
    {
        public UserDocumentService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<UserDocuments> Table() => _uow.UserDocuments.Table.AsEnumerable();
        public override IEnumerable<UserDocuments> TableNoTracking() => _uow.UserDocuments.TableNoTracking.AsEnumerable();

        public override async Task<UserDocuments> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.UserDocuments.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(UserDocuments entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.UserDocuments.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<UserDocuments> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.UserDocuments.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<UserDocuments> InsertAsync(UserDocuments entity, CancellationToken cancellationToken)
        {
            var result = await _uow.UserDocuments.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<UserDocuments> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.UserDocuments.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<UserDocuments> UpdateAsync(UserDocuments entity, CancellationToken cancellationToken)
        {
            var result = await _uow.UserDocuments.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<UserDocuments> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.UserDocuments.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<UserDocuments>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<UserDocuments, bool>> filter = null, Func<IQueryable<UserDocuments>, IOrderedQueryable<UserDocuments>> orderBy = null)
        {
            return await _uow.UserDocuments.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }

        public async Task<List<UserDocumentsViewModel>> GetDocumentsByUserIdAsync(int userId,
            CancellationToken cancellationToken)
        {
            try
            {
                var documents = await _uow.UserDocuments.TableNoTracking
                    .Include(a => a.DocumentType)
                    .Join(_uow.UserProfiles.TableNoTracking,
                        doc => doc.UserID,
                        user => user.UserID,
                        (doc, user) => new { Document = doc, User = user })
                    .Where(a => a.Document.UserID.Equals(userId))
                    .Select(a => new UserDocumentsViewModel
                    {
                        ID = a.Document.ID,
                        UserName = a.User.FirstName + " " + a.User.LastName,
                        FileID = a.Document.FileID,
                        IsActive = a.Document.IsActive,
                        Description = a.Document.Description,
                        RegUserID = a.Document.RegUserID,
                        RegDate = a.Document.RegDate,
                        DocumentTypeID = a.Document.DocumentTypeID,
                        DocumentTypeTitle = a.Document.DocumentType.Title,
                        UserID = a.Document.UserID
                    })
                    .ToListAsync(cancellationToken);

                return documents;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
