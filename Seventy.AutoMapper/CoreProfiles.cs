using Seventy.ViewModel.Core.UserGroupMember;
using AutoMapper;
using Seventy.DomainClass.Core;
using Seventy.ViewModel.Core.Users;

namespace Seventy.ViewModel.Core
{
    public partial class CoreProfiles : Profile
    {
        public CoreProfiles()
        {
            CreateMap<UserDocumentsViewModel, UserDocuments>().ReverseMap();
            CreateMap<DocumentsViewModel, Documents>().ReverseMap();
            CreateMap<KMcategoryViewModel, KMcategory>().ReverseMap();
            CreateMap<KmExperienceViewModel, KMExperience>().ReverseMap();
            CreateMap<KmNeedsViewModel, KMNeeds>().ReverseMap();
            CreateMap<MessagesViewModel, Messages>().ReverseMap();
            CreateMap<PlaceLayersViewModel, PlaceLayers>().ReverseMap();
            CreateMap<PlacesViewModel, Places>().ReverseMap();
            CreateMap<RolePermissionsViewModel, RolePermissions>().ReverseMap();
            CreateMap<RolesViewModel, Roles>().ReverseMap();
            CreateMap<TagsViewModel, Tags>().ReverseMap();
            CreateMap<TicketsViewModel, Tickets>().ReverseMap();
            CreateMap<UserAccessViewModel, UserAccess>().ReverseMap();
            CreateMap<LogsViewModel, Logs>().ReverseMap();
            CreateMap<PermissionsViewModel, Permissions>().ReverseMap();
            CreateMap<UserGroupsViewModel, UserGroups>().ReverseMap();
            CreateMap<FilesViewModel, Files>().ReverseMap();
            CreateMap<UserGroupMembersViewModel, UserGroupMembers>().ReverseMap();
            CreateMap<UserProfilesViewModel, UserProfiles>().ReverseMap();
            CreateMap<UsersViewModel, DomainClass.Core.Users>().ReverseMap();
            CreateMap<DocumentTypeViewModel, DocumentType>().ReverseMap();

            CreateMap<UserGroupMembersEditModel, UserGroupMembers>().ReverseMap();
            CreateMap<Access, AccessViewModel>().ReverseMap();
        }
    }
}

