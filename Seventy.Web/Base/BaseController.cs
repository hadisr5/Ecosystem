using DataTables.AspNet.Core;
using Extensions;
using Microsoft.AspNetCore.Mvc;
using Seventy.DomainClass.Core;
using Seventy.Repository.Core;
using Seventy.Service.Users;
using Seventy.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Web
{
    public class BaseController : Controller
    {
        protected readonly IUnitOfWork _uow;
        protected readonly AutoMapper.IMapper _mapper;
        protected readonly IUserManager _userManager;

        public BaseController(IUnitOfWork uow
            , AutoMapper.IMapper mapper
            , IUserManager userManager)
        {
            _uow = uow;
            _mapper = mapper;
            _userManager = userManager;
        }
        protected async Task<Users> GetCurrentUser(CancellationToken cancellationToken)
        {
            return await _userManager.GetCurrentUserAsync(cancellationToken);
        }
        protected int? GetCurrentUserId()
        {
            return _userManager.GetCurrentUserID();
        }
    }
}
