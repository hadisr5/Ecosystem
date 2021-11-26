using AutoMapper;
using Seventy.Data;
using System.Threading;
using Seventy.Service.Users;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Seventy.Service.Core.UserGroup;
using Seventy.WebFramework.Filters;

// ReSharper disable Mvc.ViewNotResolved
// ReSharper disable Mvc.PartialViewNotResolved

namespace Seventy.Web.Areas.Edu.UserGroups
{
    [Area("Edu")]
    public class UserGroupsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;
        private readonly IUserGroupService _userGroupService;

        public UserGroupsController(IUserManager userManager, IMapper mapper, IUserGroupService userGroupService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _userGroupService = userGroupService;
        }

        [HttpGet]
        [Route("/Edu/UserGroups/Index")]
        [UserAccess(Common.Enums.eAccessControl.UserGroupsIndex , Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, int id = 0)
        {
            if (id == 0)
                return View();

            var model = await _userGroupService.GetByIDAsync(cancellationToken, id);

            return View(model);
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/UserGroups/Index")]
        [UserAccess(Common.Enums.eAccessControl.UserGroupsIndex2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, DomainClass.Core.UserGroups model)
        {
            if (!ModelState.IsValid)
            {
                TempData["err"] = "model is not valid";

                return View(model);
            }

            model.RegUserID = _userManager.GetCurrentUserID();

            if (model.ID == 0 || model.ID == null)
            {
                model.ID = null;

                var insert = await _userGroupService.InsertAsync(model, cancellationToken);

                if (insert != null)
                {
                    TempData["success"] = "با موفقیت افزوده شد";

                    return RedirectToAction("Index");
                }

                TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";

                return View(model);
            }

            var update = await _userGroupService.UpdateAsync(model, cancellationToken);

            if (update != null)
            {
                TempData["success"] = "با موفقیت بروز رسانی شد";

                return RedirectToAction("Index");
            }

            TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";

            return View(model);
        }

        [Route("/Edu/UserGroups/List")]
        [UserAccess(Common.Enums.eAccessControl.UserGroupsList, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> List(int page)
        {
            var model = await _userGroupService
                .GetAllPaginatedAsync(new GenericPagingParameters
                {
                    PageNumber = page,
                    PageSize = 10
                }, q => q.IsActive);

            return PartialView("List", model);
        }

        [Route("/Edu/UserGroups/Remove")]
        [UserAccess(Common.Enums.eAccessControl.UserGroupsDelete, Common.Enums.eAccessType.None, 1)]
        public async Task<string> Delete(CancellationToken cancellationToken, int id)
        {
            var entity = await _userGroupService.GetByIDAsync(cancellationToken, id);

            if (entity == null)
                return "آیتم مورد نظر یافت نشد";

            if (await _userGroupService.DeleteAsync(entity, cancellationToken))
                return "done";

            return "خطا در زمان حذف داده";
        }
    }
}