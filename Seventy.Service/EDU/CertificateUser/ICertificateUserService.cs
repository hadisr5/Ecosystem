using Seventy.Data;
using Seventy.ViewModel.EDU.CertificateUser;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.CertificateUser
{
    public interface ICertificateUserService : BaseService.IBaseService<DomainClass.EDU.CertificateUser>
    {
        Task<PagedList<CertificateUserViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters
          , Expression<Func<CertificateUserViewModel, bool>> filter = null,
          Func<IQueryable<CertificateUserViewModel>, IOrderedQueryable<CertificateUserViewModel>> orderBy = null);
    }
}
