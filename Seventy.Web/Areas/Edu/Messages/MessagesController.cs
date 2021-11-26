using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Seventy.Data;
using Seventy.DomainClass.Core;
using Seventy.Service.Core.Message;
using Seventy.Service.Core.UserGroup;
using Seventy.Service.Core.UserGroupMember;
using Seventy.Service.Core.UserProfiles;
using Seventy.Service.Users;
using Seventy.WebFramework.Filters;

namespace Seventy.Web.Areas.Edu.Messages
{
    [Area("Edu")]
    [Authorize(Policy = "user")]
    public class MessagesController : Controller
    {
        private readonly IUserProfilesService _userProfilesService;
        private readonly IUserGroupService _userGroupService;
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;
        private readonly IMessageService _messageService;

        public MessagesController( 
            IUserProfilesService userProfilesService,
            IMessageService messageService, IUserManager userManager, IMapper mapper, IUserGroupService userGroupService, IUserGroupMemberService groupMemberService)
        {
            _userProfilesService = userProfilesService;
            this._userGroupService = userGroupService;
            _groupMemberService = groupMemberService;
            this._mapper = mapper;
            this._userManager = userManager;
            this._messageService = messageService;
        }

        public IUserGroupMemberService _groupMemberService { get; }


        #region Messages = Get
        [HttpGet]
        [Route("/Edu/Messages")]
        [UserAccess(Common.Enums.eAccessControl.MessagesMessages , Common.Enums.eAccessType.None, 1)]
        public IActionResult Messages(CancellationToken ct, int? msgType, int? receiverID)
        {
            ViewData["msgType"] = msgType;
            ViewData["receiverID"] = receiverID;
            return View();
        }
        
        [HttpPost]
        [Route("/Edu/MessagesList")]
        [UserAccess(Common.Enums.eAccessControl.MessagesMessagesList, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> MessagesList(CancellationToken ct,int page)
        {

          var list= await  _messageService.GetAllPaginatedAsync(new GenericPagingParameters
            {
                PageNumber = page,
                PageSize = 5
            },null,queryable => queryable.OrderByDescending(q=>q.ID));


         // var viewModels= list.Select(l => _mapper.Map(l, new MessagesViewModel()));
          
          
            return View("List",list);
        }
        #endregion

        #region Messages = Post


        [HttpPost]
        [Route("/Edu/MessagesPost")]
        [UserAccess(Common.Enums.eAccessControl.MessagesMessagesPost, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult>
         MessagesPost(CancellationToken ct
         , MessagesViewModel viewModel, MsgType? messageType, int targetId)
        {


            try
            {
                if (messageType == null)
                {
                    throw new System.Exception("نوع پیغام که گروهی باشد یا فردی را انتخاب نکرده اید");

                }


                var currentUserId = _userManager.GetCurrentUserID();

                if (currentUserId.HasValue == false)
                {
                    throw new System.Exception("کاربر وارد نشده است");
                }

                // آیا نوع آن گروهی است یا فردی ؟
                if (messageType == MsgType.Person)
                {
                    viewModel.MsgType = "شخصی";
                }
                else
                {
                    viewModel.MsgType = "گروهی";
                }


                List<Seventy.DomainClass.Core.Messages> messageList = new List<Seventy.DomainClass.Core.Messages>();

                // اگر گروهی است گروه را پیدا کن
                if (messageType == MsgType.Group)
                {

                    var group = await _userGroupService.GetByIDAsync(ct, targetId);
                    if (group == null)
                        throw new System.Exception("کد ارسالی برای گروه اشتباه است");

                    // اگر گروه را پیداکردی  اشخاص آن گروه را دربیاور
                    PagedList<UserGroupMembers> allMembers = await _groupMemberService.GetAllPaginatedAsync(new Data.GenericPagingParameters { PageSize = int.MaxValue }, ugm => ugm.UserGroupID == group.ID);

                    // برای هر کدام یک بالک اینزرت کن

                    for (int i = 0; i < allMembers.TotalCount; i++)
                    {


                        //چک می کند به خودش نفرستد
                        if (allMembers[i].UserID == currentUserId)
                        {
                            continue;
                        }

                        var message = MapAndGet(allMembers[i].ID, viewModel, currentUserId);

                        messageList.Add(message);
                    }
                }
                // اگر فردی است  آیا فرد انتخاب شده است ؟
                else if (messageType == MsgType.Person)
                {


                    //چک می کند به خودش نفرستد
                    if (targetId == currentUserId)
                    {
                        throw new Exception("نمی توانید به خودتان پیغام بفرستید ");
                    }

                    // آیا فرد انتخاب شده وجود دارد ؟
                    var selectedUser = await _userManager.GetByIDAsync(ct, targetId);
                    if (selectedUser == null)
                    {
                        throw new Exception("کاربر انتخاب شده وجود ندارد");
                    }

                    // برای آن فرد ذخیره کن

                    var message = MapAndGet(selectedUser.ID, viewModel, currentUserId);

                    messageList.Add(message);

                   

                }
                else
                {
                    throw new Exception("نوع پیام شناسایی نشد");
                }

                
                bool success = await _messageService.InsertRangeAsync(messageList, ct);
                if (success == false)
                {
                    throw new Exception("خطا: ذخیره اطلاعات بصورت یکجا سمت سرویس خطا داد");
                }

                TempData["Message"] = "با موفقیت ذخیره گردید";

                return RedirectToAction("Messages");
            }
            catch (System.Exception e)
            {

                TempData["Error"] = e.Message;
                return View("Messages",viewModel);
            }



            // اگر جایی به خطا خوردی برگرد و به کاربر نشان بده

            return View("Messages");
        }
        [UserAccess(Common.Enums.eAccessControl.MessagesMapAndGet, Common.Enums.eAccessType.None, 1)]
        public Seventy.DomainClass.Core.Messages MapAndGet(int? UserID, MessagesViewModel viewModel, int? currentUserId)
        {
            var message = _mapper.Map(viewModel, new DomainClass.Core.Messages());

            message.RegUserID = currentUserId;
            message.SenderUserID = currentUserId.Value;

            // دریافت کننده در گروه
            message.ReceiverUserID = UserID.Value;
            message.RegUserID = currentUserId;

            return message;
        }

        [UserAccess(Common.Enums.eAccessControl.MessagesGetMessageTypes, Common.Enums.eAccessType.None, 1)]
        public static async Task<IEnumerable<SelectListItem>> GetMessageTypes(int? selected = null)
        {
            List<dynamic> list = new List<dynamic>
            {
                new {Name = "گروهی"  , Id=((int) MsgType.Group).ToString()},
                new {Name = "شخص"  , Id=((int) MsgType.Person).ToString()},
            };
            return new SelectList(list, "Id", "Name", selected);
        }
        [UserAccess(Common.Enums.eAccessControl.MessagesGetMessageKindType, Common.Enums.eAccessType.None, 1)]
        public static async Task<IEnumerable<SelectListItem>> GetMessageKindType()
        {
            List<dynamic> list = new List<dynamic>
            {
                new {Name = "هشدار"  , Id=((int) MsgKindType.Alert).ToString()},
                new {Name = "پیام"  , Id=((int) MsgKindType.PM).ToString()},
            };
            return new SelectList(list, "Id", "Name");
        }
        #endregion

        #region Called by Ajax


        [HttpGet]
        [Route("/Edu/Messages/GetReceiversByMessageType")]
        [UserAccess(Common.Enums.eAccessControl.MessagesGetReceiverByMessageType, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult>
            GetReceiversByMessageType(CancellationToken ct, int messageType)
        {

            if (messageType == (int)MsgType.Group)
            {

                var groups =  _userGroupService.Table().ToList();


                var list = groups.AsQueryable().Select(s => new { name = s.Title, id = s.ID.ToString() }).ToList();

              //  var selectList = new SelectList(list, "Id", "Name");


                return Json(list);


            }
            else if (messageType == (int)MsgType.Person)
            {

                var users =  _userProfilesService.Table().ToList();
                var list = users.Select(s => new { name = s.FirstName+ " " + s.LastName, id = s.UserID.ToString() }).ToList();

               // var selectList = new SelectList(list, "Id", "Name");


                return Json(list);
            }
            else
            {
                throw new Exception("نوع شناسایی نشد");
            }


        }



        #endregion



    }


    public enum MsgType
    {

        Person = 0, Group = 1
    }


    public enum MsgKindType
    {

        Alert = 0, PM = 1
    }
}