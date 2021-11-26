using Seventy.Data;
using Seventy.Repository.Core.Repositories;
using Seventy.Repository.Persistence.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Repository.Persistence
{
    public class UnitOfWork : Core.IUnitOfWork
    {
        private readonly DataContext _db;
        public UnitOfWork(DataContext db)
        {
            _db = db;
        }


        #region Accounting
        private IDeductionsRepository _deductions;
        public IDeductionsRepository Deductions => _deductions == null ? _deductions = new DeductionsRepository(_db) : _deductions;

        private IFinancialTransactionsRepository _financialTransactionsRepository;
        public IFinancialTransactionsRepository FinancialTransactions => _financialTransactionsRepository == null ? _financialTransactionsRepository = new FinancialTransactionsRepository(_db) : _financialTransactionsRepository;

        private IGoroohAccountRepository _GoroohAccount;
        public IGoroohAccountRepository GoroohAccount => _GoroohAccount == null ? _GoroohAccount = new GoroohAccountRepository(_db) : _GoroohAccount;

        private IKolAccountRepository _KolAccount;
        public IKolAccountRepository KolAccount => _KolAccount == null ? _KolAccount = new KolAccountRepository(_db) : _KolAccount;

        private IMoeinAccountRepository _MoeinAccount;
        public IMoeinAccountRepository MoeinAccount => _MoeinAccount == null ? _MoeinAccount = new MoeinAccountRepository(_db) : _MoeinAccount;

        private ITafsiliAccountRepository _TafsiliAccount;
        public ITafsiliAccountRepository TafsiliAccount => _TafsiliAccount == null ? _TafsiliAccount = new TafsiliAccountRepository(_db) : _TafsiliAccount;

        private ISettlementRequestRepository _SettlementRequest;
        public ISettlementRequestRepository SettlementRequest => _SettlementRequest == null ? _SettlementRequest = new SettlementRequestRepository(_db) : _SettlementRequest;
        #endregion

        private IFileRepository _files;
        public IFileRepository Files => _files == null ? _files = new FileRepository(_db) : _files;


        private ICateringPackageRepository _cateringPackage;
        public ICateringPackageRepository CateringPackage => _cateringPackage == null ? _cateringPackage = new CateringPackageRepository(_db) : _cateringPackage;


        private ICertificateRepository _certificate;
        public ICertificateRepository Certificate => _certificate == null ? _certificate = new CertificateRepository(_db) : _certificate;


        private ICertificateUserRepository _certificateUser;
        public ICertificateUserRepository CertificateUser => _certificateUser == null ? _certificateUser = new CertificateUserRepository(_db) : _certificateUser;



        private IContentObservationRepository _contentObservation;
        public IContentObservationRepository ContentObservation => _contentObservation == null ? _contentObservation = new ContentObservationRepository(_db) : _contentObservation;


        private ICourseCategoryRepository _courseCategory;
        public ICourseCategoryRepository CourseCategory => _courseCategory == null ? _courseCategory = new CourseCategoryRepository(_db) : _courseCategory;



        private ICourseGroupsRepository _courseGroups;
        public ICourseGroupsRepository CourseGroups => _courseGroups == null ? _courseGroups = new CourseGroupsRepository(_db) : _courseGroups;



        private ICourseObservationRepository _courseObservation;
        public ICourseObservationRepository CourseObservation => _courseObservation == null ? _courseObservation = new CourseObservationRepository(_db) : _courseObservation;


        private ICourseRegistrationRepository _courseRegistration;
        public ICourseRegistrationRepository CourseRegistration => _courseRegistration == null ? _courseRegistration = new CourseRegistrationRepository(_db, Course, CateringPackage, Term) : _courseRegistration;


        private ICourseRepository _course;
        public ICourseRepository Course => _course == null ? _course = new CourseRepository(_db) : _course;


        private IDocumentsRepository _documents;
        public IDocumentsRepository Documents => _documents == null ? _documents = new DocumentsRepository(_db) : _documents;



        private IDocumentTypeRepository _documentType;
        public IDocumentTypeRepository DocumentType => _documentType == null ? _documentType = new DocumentTypeRepository(_db) : _documentType;


        private IExamAnswerSheetRepository _examAnswerSheet;
        public IExamAnswerSheetRepository ExamAnswerSheet => _examAnswerSheet == null ? _examAnswerSheet = new ExamAnswerSheetRepository(_db) : _examAnswerSheet;


        private IExamQuestionsRepository _examQuestions;
        public IExamQuestionsRepository ExamQuestions => _examQuestions == null ? _examQuestions = new ExamQuestionsRepository(_db) : _examQuestions;


        private IExamRepository _exam;
        public IExamRepository Exam => _exam == null ? _exam = new ExamRepository(_db) : _exam;



        private IExamUserRepository _examUser;
        public IExamUserRepository ExamUser => _examUser == null ? _examUser = new ExamUserRepository(_db) : _examUser;


        private IExerciseRepository _exercise;
        public IExerciseRepository Exercise => _exercise == null ? _exercise = new ExerciseRepository(_db) : _exercise;



        private IExerciseUserRepository _exerciseUser;
        public IExerciseUserRepository ExerciseUser => _exerciseUser == null ? _exerciseUser = new ExerciseUserRepository(_db) : _exerciseUser;


        private IFavoriteCoursesRepository favoriteCourses;
        public IFavoriteCoursesRepository FavoriteCourses => favoriteCourses == null ? favoriteCourses = new FavoriteCoursesRepository(_db) : favoriteCourses;

        private IFileRepository _file;
        public IFileRepository File => _file == null ? _file = new FileRepository(_db) : _file;


        private IForumRepository _forum;
        public IForumRepository Forum => _forum == null ? _forum = new ForumRepository(_db) : _forum;


        private IKMcategoryRepository _kmCategory;
        public IKMcategoryRepository KMcategory => _kmCategory == null ? _kmCategory = new KMcategoryRepository(_db) : _kmCategory;



        private IKMExperienceRepository _kmExperience;
        public IKMExperienceRepository KMExperience => _kmExperience == null ? _kmExperience = new KMExperienceRepository(_db) : _kmExperience;


        private IKMNeedsRepository _kmNeeds;
        public IKMNeedsRepository KMNeeds => _kmNeeds == null ? _kmNeeds = new KMNeedsRepository(_db) : _kmNeeds;


        private ILessonObservationRepository _lessonObservation;
        public ILessonObservationRepository LessonObservation => _lessonObservation == null ? _lessonObservation = new LessonObservationRepository(_db) : _lessonObservation;


        private ILessonRepository _lesson;
        public ILessonRepository Lesson => _lesson == null ? _lesson = new LessonRepository(_db) : _lesson;



        private ILMSRepository _lms;
        public ILMSRepository LMS => _lms == null ? _lms = new LMSRepository(_db) : _lms;


        private ILogsRepository _logs;
        public ILogsRepository Logs => _logs == null ? _logs = new LogsRepository(_db) : _logs;


        private IMessagesRepository _messages;
        public IMessagesRepository Messages => _messages == null ? _messages = new MessagesRepository(_db) : _messages;


        private IPermissionsRepository _permissions;
        public IPermissionsRepository Permissions => _permissions == null ? _permissions = new PermissionsRepository(_db) : _permissions;


        private IPlaceLayersRepository _placeLayers;
        public IPlaceLayersRepository PlaceLayers => _placeLayers == null ? _placeLayers = new PlaceLayersRepository(_db) : _placeLayers;



        private IPlacesRepository _places;
        public IPlacesRepository Places => _places == null ? _places = new PlacesRepository(_db) : _places;



        private IPollRepository _poll;
        public IPollRepository Poll => _poll == null ? _poll = new PollRepository(_db) : _poll;


        private IPollUserRepository _pollUser;
        public IPollUserRepository PollUser => _pollUser == null ? _pollUser = new PollUserRepository(_db) : _pollUser;


        private IQuestionOptionsRepository _questionOptions;
        public IQuestionOptionsRepository QuestionOptions => _questionOptions == null ? _questionOptions = new QuestionOptionsRepository(_db) : _questionOptions;


        private IQuestionsRepository _questions;
        public IQuestionsRepository Questions => _questions == null ? _questions = new QuestionsRepository(_db) : _questions;



        private IRelatedCoursesRepository _relatedCourses;
        public IRelatedCoursesRepository RelatedCourses => _relatedCourses == null ? _relatedCourses = new RelatedCoursesRepository(_db) : _relatedCourses;



        private IRequestedCoursesRepository _requestedCourses;
        public IRequestedCoursesRepository RequestedCourses => _requestedCourses == null ? _requestedCourses = new RequestedCoursesRepository(_db) : _requestedCourses;


        private IRequestForContentRepository _requestForContent;
        public IRequestForContentRepository RequestForContent => _requestForContent == null ? _requestForContent = new RequestForContentRepository(_db) : _requestForContent;



        private IRolePermissionsRepository _rolePermissions;
        public IRolePermissionsRepository RolePermissions => _rolePermissions == null ? _rolePermissions = new RolePermissionsRepository(_db) : _rolePermissions;



        private IRolesRepository _roles;
        public IRolesRepository Roles => _roles == null ? _roles = new RolesRepository(_db) : _roles;



        private ITagsRepository _tags;
        public ITagsRepository Tags => _tags == null ? _tags = new TagsRepository(_db) : _tags;


        private ITeacherEvalIndexRepository _teacherEvalIndex;
        public ITeacherEvalIndexRepository TeacherEvalIndex => _teacherEvalIndex == null ? _teacherEvalIndex = new TeacherEvalIndexRepository(_db) : _teacherEvalIndex;


        private ITeacherEvalResultRepository _teacherEvalResult;
        public ITeacherEvalResultRepository TeacherEvalResult => _teacherEvalResult == null ? _teacherEvalResult = new TeacherEvalResultRepository(_db) : _teacherEvalResult;


        private ITeacherLessonRepository _teacherLesson;
        public ITeacherLessonRepository TeacherLesson => _teacherLesson == null ? _teacherLesson = new TeacherLessonRepository(_db) : _teacherLesson;



        private ITeacherLikeRepository _teacherLike;
        public ITeacherLikeRepository TeacherLike => _teacherLike == null ? _teacherLike = new TeacherLikeRepository(_db) : _teacherLike;



        private ITermLessonRepository _termLesson;
        public ITermLessonRepository TermLesson => _termLesson == null ? _termLesson = new TermLessonRepository(_db) : _termLesson;



        private ITermRepository _term;
        public ITermRepository Term => _term == null ? _term = new TermRepository(_db) : _term;



        private ITicketsRepository _tickets;
        public ITicketsRepository Tickets => _tickets == null ? _tickets = new TicketsRepository(_db) : _tickets;



        private ITrainingCenterRepository _trainingCenter;
        public ITrainingCenterRepository TrainingCenter => _trainingCenter == null ? _trainingCenter = new TrainingCenterRepository(_db) : _trainingCenter;



        private ITrainingContentRepository _trainingContent;
        public ITrainingContentRepository TrainingContent => _trainingContent == null ? _trainingContent = new TrainingContentRepository(_db) : _trainingContent;



        private ITrainingEvalIndexRepository _trainingEvalIndex;
        public ITrainingEvalIndexRepository TrainingEvalIndex => _trainingEvalIndex == null ? _trainingEvalIndex = new TrainingEvalIndexRepository(_db) : _trainingEvalIndex;


        private ITrainingEvalResultRepository _trainingEvalResult;
        public ITrainingEvalResultRepository TrainingEvalResult => _trainingEvalResult == null ? _trainingEvalResult = new TrainingEvalResultRepository(_db) : _trainingEvalResult;



        private ITrainingWeekContentRepository _trainingWeekContent;
        public ITrainingWeekContentRepository TrainingWeekContent => _trainingWeekContent == null ? _trainingWeekContent = new TrainingWeekContentRepository(_db) : _trainingWeekContent;


        private ITrainingWeekRepository _trainingWeek;
        public ITrainingWeekRepository TrainingWeek => _trainingWeek == null ? _trainingWeek = new TrainingWeekRepository(_db) : _trainingWeek;



        private IUserAccessRepository _userAccess;
        public IUserAccessRepository UserAccess => _userAccess == null ? _userAccess = new UserAccessRepository(_db) : _userAccess;



        private IUserContentRepository _userContent;
        public IUserContentRepository UserContent => _userContent == null ? _userContent = new UserContentRepository(_db) : _userContent;



        private IUserDocumentsRepository _userDocuments;
        public IUserDocumentsRepository UserDocuments => _userDocuments == null ? _userDocuments = new UserDocumentsRepository(_db) : _userDocuments;



        private IUserGroupMembersRepository _userGroupMembers;
        public IUserGroupMembersRepository UserGroupMembers => _userGroupMembers == null ? _userGroupMembers = new UserGroupMembersRepository(_db) : _userGroupMembers;


        private IUserGroupsRepository _userGroups;
        public IUserGroupsRepository UserGroups => _userGroups == null ? _userGroups = new UserGroupsRepository(_db) : _userGroups;



        private IUserLessonRepository _userLesson;
        public IUserLessonRepository UserLesson => _userLesson == null ? _userLesson = new UserLessonRepository(_db) : _userLesson;



        private IUsersRepository _users;
        public IUsersRepository Users => _users == null ? _users = new UsersRepository(_db) : _users;


        private IUserProfilesRepository _userProfiles;
        public IUserProfilesRepository UserProfiles => _userProfiles == null ? _userProfiles = new UserProfilesRepository(_db) : _userProfiles;



        private IUserTrainingWeekContentRepository _userTrainingWeekContent;
        public IUserTrainingWeekContentRepository UserTrainingWeekContent => _userTrainingWeekContent == null ? _userTrainingWeekContent = new UserTrainingWeekContentRepository(_db) : _userTrainingWeekContent;


        private IAccessRepository _accesses;
        public IAccessRepository Access => _accesses == null ? _accesses = new AccessRepository(_db) : _accesses;

        private IUserRoleRepository _userRole;
        public IUserRoleRepository UserRole => _userRole == null ? _userRole = new UserRoleRepository(_db) : _userRole;


        private IDefaultRoleAccessRepository _defaultRoleAccess;
        public IDefaultRoleAccessRepository DefaultRoleAccess => _defaultRoleAccess == null ? _defaultRoleAccess = new DefaultRoleAccessRepository(_db) : _defaultRoleAccess;


        private IAccessPermissionGroupRepository _accessPermissionGroup;
        public IAccessPermissionGroupRepository   AccessPermissionGroup => _accessPermissionGroup == null ? _accessPermissionGroup = new AccessPermissionGroupRepository(_db) : _accessPermissionGroup;


        private IPermissionGroupRepository _permissionGroup;
        public IPermissionGroupRepository  PermissionGroup => _permissionGroup == null ? _permissionGroup = new PermissionGroupRepository(_db) : _permissionGroup;


        private IUserPermissionGroupRepository _UserPermissionGroup;
        public IUserPermissionGroupRepository  UserPermissionGroup => _UserPermissionGroup == null ? _UserPermissionGroup = new UserPermissionGroupRepository(_db) : _UserPermissionGroup;

       
        private IMenuAccessRepository _menuAccessRepository;
        public IMenuAccessRepository  MenuAccess=> _menuAccessRepository == null ? _menuAccessRepository = new MenuAccessRepository(_db) : _menuAccessRepository;


        public int Complete() => _db.SaveChanges();
        public async Task<int> CompleteAsync(CancellationToken cancellationToken) => await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
