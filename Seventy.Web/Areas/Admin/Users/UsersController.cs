using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Seventy.Data;
using Seventy.Service.Core.Users;
using Seventy.Service.Users;
using Seventy.WebFramework.Filters;
using Seventy.ViewModel.Core.Users;
using Seventy.Web.Areas.Edu.AnswerQuestion;
using Seventy.Service.Core.Roles;

namespace Seventy.Web.Areas.Admin.Users
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class UsersController : Controller
    {
        private static IUserManager _userManager;
        private static IRolesService _roleService;
        private static INotif _notifManager;
        private readonly IMapper _mapper;

        public UsersController(IUserManager userManager, IRolesService RolesService, INotif notifManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleService = RolesService;
            _notifManager = notifManager;
            _mapper = mapper;
        }

        [Route("/admin/users/list")]
        [UserAccess(Common.Enums.eAccessControl.UsersIndex, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Index(string searchTerm, RoleSelect roleSelect, int pageNumber)
        {
            int total = 0;
             var (list, totalPages) = await ReadList(searchTerm, roleSelect, pageNumber);
            ViewBag.TotalPages = totalPages;

            return View(list);
        }
        [UserAccess(Common.Enums.eAccessControl.UsersReadList, Common.Enums.eAccessType.None, 1)]
        public async Task<(List<UserListViewModel> , int TotalPages)> ReadList(string searchTerm, RoleSelect roleSelect, int pageNumber)
        {
            roleSelect = roleSelect == 0 ? RoleSelect.All : roleSelect;
            var list = await _userManager.GetByRole(roleSelect);

            if (Debugger.IsAttached && list.Count == 0)
            {
                list = _userManager.TableNoTracking()
                    .Select(s => new UserListViewModel
                    {
                        Mobile = s.Mobile,
                        ID = s.ID
                    }).ToList();
            }


            if (string.IsNullOrEmpty(searchTerm) == false)
            {
                list = list.Where(u => (u.Name?.Contains(searchTerm) ?? false)
                                       || (u.Family?.Contains(searchTerm) ?? false)
                                       || (u.Mobile?.Contains(searchTerm) ?? false)
                ).ToList();
            }
            int totalPages= list.Count / 10+(list.Count % 10 >0 ? 1 : 0);

            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            list = list.OrderByDescending(o => o.ID).ToList();

            if (pageNumber > 1)
            {
                pageNumber--;
                list = list.Skip(pageNumber * 10).Take(10).ToList();
            }
            else
            {
                list = list.Take(10).ToList();
            }

            return (list,totalPages);
        }

        [UserAccess(Common.Enums.eAccessControl.UsersRoleList, Common.Enums.eAccessType.None, 1)]
        public IEnumerable<DomainClass.Core.Roles> GetAllRoles()
        {
            return _roleService.TableNoTracking();
        }


        [Route("/admin/users/listPartial")]
        [UserAccess(Common.Enums.eAccessControl.UsersListPartial, Common.Enums.eAccessType.None, 1)]

        public async Task<IActionResult> ListPartial(string searchTerm, RoleSelect roleSelect, int pageNumber)
        {
            int total = 0;
            var (list, totalPages) = await ReadList(searchTerm, roleSelect, pageNumber);
            ViewBag.TotalPages = totalPages;
            return View(list);
        }

        [Route("/admin/users/EditUser")]
        [UserAccess(Common.Enums.eAccessControl.ShowChangeUserInfo, Common.Enums.eAccessType.View, 1)]
        public async Task<IActionResult> EditUser(int id, CancellationToken cancellationToken)
        {
            return View(await _userManager.FindByIDAsync(id, cancellationToken));
        }

        [HttpPost, ValidateAntiForgeryToken]
        [UserAccess(Common.Enums.eAccessControl.UsersEditUserPost, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> EditUserPost(Seventy.DomainClass.Core.Users model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View("EditUser", model);
            }

            var oldConfig = await _userManager.FindByIDAsync(model.ID.Value, cancellationToken);
            model.Password = oldConfig.Password;
            model.RegDate = oldConfig.RegDate;
            var curUser = await _userManager.UpdateAsync(model, cancellationToken);
            if (curUser != null)
            {
                ViewBag.success = "کاربر با موفقیت ویرایش شد.";
                return View("EditUser", model);
            }

            return View("EditUser", model);
        }

        [Route("/admin/users/SendMessage")]
        [UserAccess(Common.Enums.eAccessControl.ShowSendMessage, Common.Enums.eAccessType.View, 2)]
        public async Task<IActionResult> SendMessage()
        {
            ViewBag.Users = _userManager.TableNoTracking();
            return View(new Seventy.DomainClass.Core.Messages());
        }

        [HttpPost, ValidateAntiForgeryToken]
        [UserAccess(Common.Enums.eAccessControl.UsersSendMessagePost, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> SendMessagePost(Seventy.DomainClass.Core.Messages model, CancellationToken cancellationToken)
        {
            ViewBag.Users = _userManager.Table();

            if (!ModelState.IsValid)
            {
                return View("SendMessage", model);
            }

            var curUser = await _userManager.GetCurrentUserAsync(cancellationToken);
            model.MsgType = "1";
            model.SenderUserID = curUser.ID.Value;
            var loginResult = await _notifManager.InsertAsync(model, cancellationToken);
            if (loginResult != null)
            {
                ViewBag.success = "پیام شما با موفقیت ارسال شد.";
                return View("SendMessage", model);
            }

            return View("SendMessage", model);
        }

        [Route("/admin/users/MessageList")]
        [UserAccess(Common.Enums.eAccessControl.UsersMessageList, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> MessageList()
        {
            var notifs = _notifManager.TableNoTracking();
            return View(notifs);
        }

        [UserAccess(Common.Enums.eAccessControl.UsersGetEmailById, Common.Enums.eAccessType.None, 1)]
        public static async Task<string> GetEmailbyId(int id, CancellationToken cancellationToken)
        {
            var d = await _userManager.FindByIDAsync(id, cancellationToken);
            return d.Mobile;
        }


        [Route("/admin/users/ChangePassword")]
        [UserAccess(Common.Enums.eAccessControl.UsersChangePassword2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> ChangePassword(int id,
            string newPassword,
            CancellationToken ct)
        {
            try
            {
                var user = _userManager.GetByID(id);
                if (user == null)
                {
                    throw new Exception("کاربر یافت نشد");
                }

                if (string.IsNullOrEmpty(newPassword))
                {
                    throw new Exception("رمز عبور ارسال شده خالی است");
                }

                if (newPassword.Length < 6)
                {
                    throw new Exception("رمز عبور نمی تواند کمتر از 6 کاراکتر باشد");
                }

                user.Password = ((UserManager) _userManager).HashPassword(newPassword);

                await _userManager.UpdateAsync(user, ct);
                TempData["success"] = "رمز عبور با موفقیت تغییر یافت";
            }
            catch (Exception e)
            {
                TempData["err"] = e.Message;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [Route("/admin/users/save")]
        [UserAccess(Common.Enums.eAccessControl.UsersSave2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Save(DomainClass.Core.UsersViewModel model,
            CancellationToken ct)
        {
            try
            {
                /*if (ModelState.IsValid==false)
                {
                                        throw  new Exception("مقادیر اشتباه ارسال شده است");
                }*/

                if (string.IsNullOrEmpty(model.Mobile))
                {
                    throw  new Exception("شماره موبایل خالی است");
                }

                if (model.Mobile.Length!=11)
                {
                    throw  new Exception("شماره موبایل باید 11 کاراکتر باشد");
                }
                
               System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"[0-9]");

               if (regex.IsMatch(model.Mobile)==false)
               {
                   throw  new Exception("شماره موبایل اشتباه است ، مجازید فقط عدد وارد نمایید");
               }

               if (string.IsNullOrEmpty(model.Password)){
                   throw  new Exception("رمز خالی است");

               }

               if (model.Password.Length>20 || model.Password.Length<6)
               {
                   throw  new Exception("رمز عبور باید حداقل 6 کاراکتر و حداکثر 20 کاراکتر باشد");
               }
                
                if (model.ID.HasValue == false || model.ID == 0)
                {
                    var result = await _userManager.Register(new RegisterViewModel
                    {
                        Mobile = model.Mobile,
                        Password = model.Password,
                        ConfirmedPassword = model.Password,
                    }, ct);

                    switch (result)
                    {
                        case RegisterResult.Success:
                            TempData["success"] = "با موفقیت ثبت شد";
                            break;
                        case RegisterResult.Error:
                            throw new Exception("خطایی رخ داد");
                        case RegisterResult.UserExist:
                            throw new Exception("شماره موبایل قبلا ثبت شده است");
                    }
                }
                else
                {
                    throw new Exception("امکان ویرایش وجود ندارد");
                }
            }
            catch (Exception e)
            {
                TempData["err"] = e.Message;
                return RedirectToAction("Index");
            }


            return RedirectToAction("Index");
        }
    }
}