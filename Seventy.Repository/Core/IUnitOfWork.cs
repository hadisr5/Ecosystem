using Seventy.Repository.Core.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Repository.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IDeductionsRepository Deductions { get; }
        IFinancialTransactionsRepository FinancialTransactions { get; }
        IGoroohAccountRepository GoroohAccount { get; }
        IKolAccountRepository KolAccount { get; }
        IMoeinAccountRepository MoeinAccount { get; }
        ITafsiliAccountRepository TafsiliAccount { get; }
        ISettlementRequestRepository SettlementRequest { get; }
        ICateringPackageRepository CateringPackage { get; }
        ICertificateRepository Certificate { get; }
        ICertificateUserRepository CertificateUser { get; }
        IContentObservationRepository ContentObservation { get; }
        ICourseCategoryRepository CourseCategory { get; }
        ICourseGroupsRepository CourseGroups { get; }
        ICourseObservationRepository CourseObservation { get; }
        ICourseRegistrationRepository CourseRegistration { get; }
        ICourseRepository Course { get; }
        IDocumentsRepository Documents { get; }
        IDocumentTypeRepository DocumentType { get; }
        IExamAnswerSheetRepository ExamAnswerSheet { get; }
        IExamQuestionsRepository ExamQuestions { get; }
        IExamRepository Exam { get; }
        IExamUserRepository ExamUser { get; }
        IExerciseRepository Exercise { get; }
        IExerciseUserRepository ExerciseUser { get; }
        IFavoriteCoursesRepository FavoriteCourses { get; }
        IFileRepository File { get; }
        IForumRepository Forum { get; }
        IKMcategoryRepository KMcategory { get; }
        IKMExperienceRepository KMExperience { get; }
        IKMNeedsRepository KMNeeds { get; }
        ILessonObservationRepository LessonObservation { get; }
        ILessonRepository Lesson { get; }
        ILMSRepository LMS { get; }
        ILogsRepository Logs { get; }
        IMessagesRepository Messages { get; }
        IPermissionsRepository Permissions { get; }
        IPlaceLayersRepository PlaceLayers { get; }
        IPlacesRepository Places { get; }
        IPollRepository Poll { get; }
        IPollUserRepository PollUser { get; }
        IQuestionOptionsRepository QuestionOptions { get; }
        IQuestionsRepository Questions { get; }
        IRelatedCoursesRepository RelatedCourses { get; }
        IRequestedCoursesRepository RequestedCourses { get; }
        IRequestForContentRepository RequestForContent { get; }
        IRolePermissionsRepository RolePermissions { get; }
        IRolesRepository Roles { get; }
        ITagsRepository Tags { get; }
        ITeacherEvalIndexRepository TeacherEvalIndex { get; }
        ITeacherEvalResultRepository TeacherEvalResult { get; }
        ITeacherLessonRepository TeacherLesson { get; }
        ITeacherLikeRepository TeacherLike { get; }
        ITermLessonRepository TermLesson { get; }
        ITermRepository Term { get; }
        ITicketsRepository Tickets { get; }
        ITrainingCenterRepository TrainingCenter { get; }
        ITrainingContentRepository TrainingContent { get; }
        ITrainingEvalIndexRepository TrainingEvalIndex { get; }
        ITrainingEvalResultRepository TrainingEvalResult { get; }
        ITrainingWeekContentRepository TrainingWeekContent { get; }
        ITrainingWeekRepository TrainingWeek { get; }
        IUserAccessRepository UserAccess { get; }
        IUserContentRepository UserContent { get; }
        IUserDocumentsRepository UserDocuments { get; }
        IUserGroupMembersRepository UserGroupMembers { get; }
        IUserGroupsRepository UserGroups { get; }
        IUserLessonRepository UserLesson { get; }
        IUserProfilesRepository UserProfiles { get; }
        IUsersRepository Users { get; }
        IUserTrainingWeekContentRepository UserTrainingWeekContent { get; }
        IAccessRepository Access { get; }
        IUserRoleRepository UserRole { get; }
        IDefaultRoleAccessRepository DefaultRoleAccess { get; }
        IAccessPermissionGroupRepository AccessPermissionGroup { get; }
        IPermissionGroupRepository PermissionGroup { get; }
        IUserPermissionGroupRepository UserPermissionGroup { get; }
        IMenuAccessRepository MenuAccess { get; }

        int Complete();
        Task<int> CompleteAsync(CancellationToken cancellationToken);
    }
}
