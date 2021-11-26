using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Seventy.Data;
using Seventy.Service.Core.Messenger;
using Seventy.Service.Core.Roles;
using Seventy.Service.Core.UserProfiles;
using Seventy.Service.Core.UserRole;
using Seventy.Service.Users;
using Seventy.ViewModel.Core.Users;
using Seventy.Web.Areas.Edu.AnswerQuestion;
using Seventy.WebFramework.Filters;

namespace Seventy.Web.Features.Account
{
    public class AccountController : Controller
    {
        private readonly IUserRoleService _userRoleService;
        private readonly IRolesService _rolesService;
        private readonly IUserManager _userManager;
        private readonly IMessageSender _smsService;
        private readonly IUserProfilesService _userProfilesService;

        public AccountController(IUserManager userManager, IMessageSender smsService, IUserProfilesService UserProfilesService
            , IUserRoleService userRoleService, IRolesService rolesService)
        {
            _userManager = userManager;
            _userProfilesService = UserProfilesService;
            _smsService = smsService;
            _userRoleService = userRoleService;
            _rolesService = rolesService;
        }

        #region login
        [HttpGet, AllowAnonymous, Route("login", Name = "Login")]
        [UserAccess(Common.Enums.eAccessControl.LoginView, Common.Enums.eAccessType.View, 1, true)]
        public IActionResult Login(LoginViewModel model)
        {
            ModelState.Clear();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Route("login")]
        [UserAccess(Common.Enums.eAccessControl.LoginValidation, Common.Enums.eAccessType.None, 2, true)]
        public async Task<IActionResult> Login(CancellationToken cancellationToken, LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var loginResult = await _userManager.Login(model, cancellationToken);
            switch (loginResult)
            {
                case Service.Core.Users.LoginResult.Success:
                    var user = await _userManager.FindByMobileAsync(model.Mobile, cancellationToken);
                    var cookieClaims = CreateCookieClaimsAsync(user);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, cookieClaims, new Microsoft.AspNetCore.Authentication.AuthenticationProperties { IsPersistent = true, IssuedUtc = DateTimeOffset.UtcNow, ExpiresUtc = DateTimeOffset.UtcNow.AddDays(10) });

                    // todo goto witch page ?
                    return Redirect(model.ReturnUrl ?? "/");
                case Service.Core.Users.LoginResult.Error:
                case Service.Core.Users.LoginResult.Disable:
                    ViewData["err"] = "اکانت شما غیر فعال است";
                    return View(model);
                case Service.Core.Users.LoginResult.NotExist:
                    ViewData["err"] = "نام کاربری و رمز عبور مطابقت ندارد";
                    return View(model);
            }
            return View(model);
        }
        #endregion

        #region Register
        [HttpGet, AllowAnonymous, Route("Register", Name = "Register")]
        [UserAccess(Common.Enums.eAccessControl.Register, Common.Enums.eAccessType.View, 3, true)]

        public IActionResult Register(RegisterViewModel model)
        {
            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        [UserAccess(Common.Enums.eAccessControl.RegisterPost, Common.Enums.eAccessType.None, 4, true)]
        public async Task<IActionResult> RegisterPost(CancellationToken cancellationToken, RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Register), model);
            }

            if (!HttpContext.Session.Keys.Contains($"{model.Mobile}_Register"))
            {
                ModelState.AddModelError(nameof(model.ActivationCode), "کد فعال سازی را وارد نمایید");
                return RedirectToAction(nameof(this.Register));
            }
            else if (HttpContext.Session.GetString($"{model.Mobile}_Register") != model.ActivationCode)
            {
                ModelState.AddModelError(nameof(model.ActivationCode), "کد فعال سازی اشتباه");
                return RedirectToAction(nameof(this.Register));
            }
            var registerResult = await _userManager.Register(model, cancellationToken);
            switch (registerResult)
            {
                case Service.Core.Users.RegisterResult.Success:
                    {
                        HttpContext.Session.Remove($"{model.Mobile}_Register");
                        return Redirect(model.ReturnUrl ?? "/");
                    }
                case Service.Core.Users.RegisterResult.Error:
                    ModelState.AddModelError(nameof(model.Mobile), "");
                    return RedirectToAction(nameof(this.Register));
                case Service.Core.Users.RegisterResult.UserExist:
                    {
                        HttpContext.Session.Remove($"{model.Mobile}_Register");
                        return RedirectToAction(nameof(Login));
                    }
            }
            return View(model);
        }
        #endregion

        #region Activation Code
        [HttpGet, AllowAnonymous, Route("RegisterationCode")]
        //[UserAccess("صفحه ی کد فعای سازی ثبت نام", "5", true)]
        [UserAccess(Common.Enums.eAccessControl.AccountControllerRegistrationCode, Common.Enums.eAccessType.View, 5, true)]

        public IActionResult RegisterationCode(ActivationViewModel model)
        {
            ModelState.Clear();
            return View(nameof(this.RegisterationCode), model);
        }
        [HttpGet, AllowAnonymous, Route("ForgotPasswordCode")]
        [UserAccess(Common.Enums.eAccessControl.RegistrationCodeForgetPassword, Common.Enums.eAccessType.View, 5, true)]
        public IActionResult ForgotPasswordCode(ActivationViewModel model)
        {
            ModelState.Clear();
            return View(nameof(this.ForgotPasswordCode), model);
        }


        [HttpGet, AllowAnonymous, Route("ActivationCode")]
        [UserAccess(Common.Enums.eAccessControl.ActivationCode, Common.Enums.eAccessType.View, 5, true)]
        public async Task<IActionResult> SendRegisterationCode(CancellationToken cancellationToken, ActivationViewModel model)
        {
            return await ActivationCode(cancellationToken, model, ActivationCodeType.Registration);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [UserAccess(Common.Enums.eAccessControl.SendCode, Common.Enums.eAccessType.View, 6, true)]
        public async Task<IActionResult> SendCode(CancellationToken cancellationToken, ActivationViewModel model)
        {
            return await ActivationCode(cancellationToken, model, ActivationCodeType.ForgotPassword);
        }
        private async Task<IActionResult> ActivationCode(CancellationToken cancellationToken, ActivationViewModel model, ActivationCodeType type)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Mobile = model.Mobile.Trim();
            var code = new Random().Next(111111, 999999).ToString();
            AbstractMessage shortMessage = new ShortMessage(new SMSMessenger());
            SendMsgResult result;
            switch (type)
            {
                case ActivationCodeType.Registration:
                    result = await shortMessage.Verify(code, model.Mobile, "haftadlogin");
                    if (result.Sent)
                    {
                        HttpContext.Session.SetString($"{model.Mobile}_Register", code);
                        var registerViewModel = new RegisterViewModel();
                        registerViewModel.Mobile = model.Mobile;
                        registerViewModel.ReturnUrl = model.ReturnUrl;
                        return View(nameof(this.Register), registerViewModel);
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(model.Mobile), "خطا در ارسال کد فعال سازی");
                        return View(nameof(this.RegisterationCode), model);
                    }
                case ActivationCodeType.ForgotPassword:
                    result = await shortMessage.Verify(code, model.Mobile, "haftadforget");
                    if (result.Sent)
                    {
                        HttpContext.Session.SetString($"{model.Mobile}_ForgotPassword", code);
                        var forgotPasswordCodeView = new ForgotPasswordCodeViewModel();
                        forgotPasswordCodeView.Mobile = model.Mobile;
                        //forgotPasswordCodeView.ReturnUrl = model.ReturnUrl;
                        return View(nameof(this.ForgetPass), forgotPasswordCodeView);
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(model.Mobile), "خطا در ارسال کد فعال سازی");
                        return View(nameof(this.ForgotPasswordCode), model);
                    }
                default:
                    break;
            }
            return View(model);
        }
        #endregion

        #region Password


        [HttpGet, AllowAnonymous, Route("ForgetPass", Name = "ForgetPass")]
        [UserAccess(Common.Enums.eAccessControl.ForgetPass, Common.Enums.eAccessType.View, 7, true)]
        public IActionResult ForgetPass(ForgotPasswordCodeViewModel model)
        {
            ModelState.Clear();
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        [UserAccess(Common.Enums.eAccessControl.RegistrationCodeForgetPassPost, Common.Enums.eAccessType.View, 5, true)]
        public async Task<IActionResult> ForgetPassPost(CancellationToken cancellationToken, ForgotPasswordCodeViewModel model)
        {
            if (model.Mobile != null && model.Code != null)
            {
                try
                {
                    if (!HttpContext.Session.Keys.Contains($"{model.Mobile}_ForgotPassword"))
                    {
                        ModelState.AddModelError("Code", "مراحل به درستی انجام نشده.");
                        return View(nameof(this.ForgetPass), model);
                    }
                    else if (HttpContext.Session.GetString($"{model.Mobile}_ForgotPassword") != model.Code)
                    {
                        ModelState.AddModelError("Code", "کد فعال سازی اشتباه");
                        return View(nameof(this.ForgetPass), model);
                    }
                    var forgetResult = await _userManager.ForgetPassword(model.Mobile,model.Password, cancellationToken);
                    switch (forgetResult)
                    {
                        case Service.Core.Users.LoginResult.Success:
                            HttpContext.Session.Remove($"{model.Mobile}_ForgotPassword");
                            return RedirectToAction(nameof(this.Login), new { success = "رمز با موفقیت تغییر یافت " });
                        case Service.Core.Users.LoginResult.Error:
                            ModelState.AddModelError(nameof(model.Mobile), "خطایی رخ داده است");
                            return View(nameof(this.ForgetPass), model);
                        case Service.Core.Users.LoginResult.Disable:
                            ModelState.AddModelError(nameof(model.Mobile), "اکانت شما غیر فعال است");
                            return View(nameof(this.ForgetPass), model);
                        case Service.Core.Users.LoginResult.NotExist:
                            ModelState.AddModelError(nameof(model.Mobile), "شماره موبایل شما در سیستم یافت نشد");
                            return View(nameof(this.ForgetPass), model);
                        default:
                            break;
                    }
                }
                catch { }
            }
            return View(nameof(this.ForgetPass), model);
        }
        #endregion

        #region LogOut
        [Route("logout")]
        [UserAccess(Common.Enums.eAccessControl.LogOut, Common.Enums.eAccessType.None, 9)]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account", null);
        }
        [UserAccess(Common.Enums.eAccessControl.CreateCookieClaimsAsync, Common.Enums.eAccessType.None, 10, true)]
        private ClaimsPrincipal CreateCookieClaimsAsync(DomainClass.Core.Users user)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Mobile));
            identity.AddClaim(new Claim("DisplayName", user.Mobile));
            int? PhotoFileId = GetProfileAvatarAsync(user.ID).Result;
            identity.AddClaim(new Claim("ImgUrl", PhotoFileId.ToString()));
            // custom data
            identity.AddClaim(new Claim(ClaimTypes.UserData, user.ID.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
            var userRole = (_userRoleService.GetAllPaginatedAsync(new Data.GenericPagingParameters
            { PageNumber = 1, PageSize = 10 }, x => x.UserID == user.ID).Result).FirstOrDefault();
            if (userRole != null)
            {

                var role = (_rolesService.GetByIDAsync(new CancellationToken(), userRole.RoleID).Result).Title;
                identity.AddClaim(new Claim("RoleName", role));
            }
            else
            {
                identity.AddClaim(new Claim("RoleName", ""));
            }
            //if (user == (byte)RoleUser.User)
            //    identity.AddClaim(new Claim(ClaimTypes.Role, RoleUser.User.ToString()));
            //else
            //    identity.AddClaim(new Claim(ClaimTypes.Role, RoleUser.Admin.ToString()));
            // }
            return new ClaimsPrincipal(identity);
        }

        #endregion

        #region Profile
        private async Task<int?> GetProfileAvatarAsync(int? userid)
        {
            var userProfiles = await _userProfilesService.GetAllPaginatedAsync(new GenericPagingParameters
            {
                PageSize = 10
            },
            profiles => profiles.UserID == userid);
            return userProfiles.Where(s => s.UserID == userid).Select(x => x.PhotoFileId).FirstOrDefault();
        }
        #endregion
    }
}