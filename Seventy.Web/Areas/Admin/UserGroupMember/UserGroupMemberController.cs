using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Seventy.DomainClass.Core;
using Seventy.Service.Core.UserGroup;
using Seventy.Service.Core.UserGroupMember;
using Seventy.Service.Users;
using Seventy.WebFramework.Filters;

namespace Seventy.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserGroupMemberController : Controller
    {
        private static IUserManager _UserManager;
        private static IUserGroupService _UserGroupService;
        private static IUserGroupMemberService _UserGroupMemberService;
        private static IMapper _mapper;
        public UserGroupMemberController(IUserManager UserManager, IUserGroupService UserGroupService, IMapper mapper,
            IUserGroupMemberService UserGroupMemberService)
        {
            _UserManager = UserManager;
            _mapper = mapper;
            _UserGroupService = UserGroupService;
            _UserGroupMemberService = UserGroupMemberService;
        }
        [HttpGet]
        [Route("/Admin/UserGroupMember/AssignGroup")]
        [UserAccess(Common.Enums.eAccessControl.UserGroupMemberAssignGroup , Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> AssignGroup(CancellationToken cancellationToken, int ID = 0)
        {
            ViewBag.AllGroups =  _UserGroupService.TableNoTracking();
            ViewBag.AllUsers = _UserManager.TableNoTracking();
            if (ID != 0)
            {
                var model = await _UserGroupService.GetByIDAsync(cancellationToken, new object[] { ID });
                var allMember = _UserGroupMemberService.Table();
                allMember = allMember.Where(q => q.UserGroupID == ID && q.IsActive == true);
                if (allMember.Count() != 0)
                {
                    //var d = _mapper.Map<List<Usergroup>>(allMember.ToList());
                    ViewBag.GroupUsers = allMember;
                }
                return View(model);
            }            
            return View();
        }
        [Route("/Admin/UserGroupMember/AssignMemberToGroup")]
        [UserAccess(Common.Enums.eAccessControl.UserGroupMemberAssignMemberToGroup, Common.Enums.eAccessType.None, 1)]
        public async Task<string> AssignMemberToGroup(CancellationToken cancellationToken, int GroupID,int UserID)
        {
            var AlreadyExist = _UserGroupMemberService.TableNoTracking();
            AlreadyExist = AlreadyExist.Where((q => q.UserGroupID == GroupID && q.UserID == UserID));
            if (AlreadyExist.FirstOrDefault() != null)
            {
                if (AlreadyExist.FirstOrDefault().IsActive == true)
                    return "این کاربر قبلا به گروه اضافه شده است";
                var entity = AlreadyExist.FirstOrDefault();
                entity.IsActive = true;
                var isUpdate = await _UserGroupMemberService.UpdateAsync(entity, cancellationToken);
                return "done";
            }

            var CurrentUserID = _UserManager.GetCurrentUserID();
            try
            {
                var isInserted = await _UserGroupMemberService.InsertAsync(new UserGroupMembers()
                {
                    RegUserID = CurrentUserID,
                    UserID = UserID,
                    UserGroupID = GroupID,
                }, cancellationToken);
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            return "done";
        }
        [Route("/Admin/UserGroupMember/RemoveMemberFromGroup")]
        [UserAccess(Common.Enums.eAccessControl.UserGroupMemberRemoveMemberFromGroup, Common.Enums.eAccessType.None, 1)]
        public async Task<string> RemoveMemberFromGroup(CancellationToken cancellationToken, int GroupID,int UserID)
        {
            var MustDeleteMember = _UserGroupMemberService.TableNoTracking().ToList();
            MustDeleteMember = MustDeleteMember.Where(q => q.UserGroupID == GroupID && q.UserID == UserID && q.IsActive == true).ToList();
            if(MustDeleteMember.Count != 0)
            {
                var isDeleted = await _UserGroupMemberService.DeleteAsync(MustDeleteMember.FirstOrDefault(), cancellationToken);
            }
            return "done";
        }
      
    }
}
