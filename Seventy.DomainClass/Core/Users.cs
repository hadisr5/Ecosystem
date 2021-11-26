using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seventy.DomainClass.Accounting;
using Seventy.DomainClass.EDU;
using Seventy.DomainClass.EDU.Course;
using Seventy.DomainClass.EDU.Exam;
using Seventy.DomainClass.EDU.Exercise;
using Seventy.DomainClass.EDU.Lesson;
using Seventy.DomainClass.EDU.Poll;
using Seventy.DomainClass.EDU.Teacher;
using Seventy.DomainClass.EDU.Term;
using Seventy.DomainClass.EDU.TrainingContent;
using Seventy.DomainClass.EDU.TrainingEval;
using Seventy.DomainClass.EDU.TrainingWeek;
using System.Collections.Generic;

namespace Seventy.DomainClass.Core
{
    public class Users : CoreBase
    {
        public string Mobile { get; set; }
        public string Password { get; set; }

        public ICollection<Deductions> Deductions { get; set; }
        public ICollection<FinancialTransactions> FinancialTransactionsUser { get; set; }
        public ICollection<FinancialTransactions> FinancialTransactionsRegUser { get; set; }
        public ICollection<SettlementRequest> SettlementRequestUser { get; set; }
        public ICollection<SettlementRequest> SettlementRequestRegUser { get; set; }
        public ICollection<TafsiliAccount> TafsiliAccount { get; set; }
        public ICollection<MoeinAccount> MoeinAccount { get; set; }
        public ICollection<KolAccount> KolAccount { get; set; }
        public ICollection<GoroohAccount> GoroohAccount { get; set; }
        public ICollection<CateringPackage> CateringPackage { get; set; }
        public ICollection<Certificate> Certificate { get; set; }
        public ICollection<CertificateUser> CertificateUserRegUser { get; set; }
        public ICollection<CertificateUser> CertificateUserUser { get; set; }
        public ICollection<ContentObservation> ContentObservationRegUser { get; set; }
        public ICollection<ContentObservation> ContentObservationUser { get; set; }
        public ICollection<Course> Course { get; set; }
        public ICollection<CourseCategory> CourseCategory { get; set; }
        public ICollection<CourseObservation> CourseObservationRegUser { get; set; }
        public ICollection<CourseObservation> CourseObservationUser { get; set; }
        public ICollection<CourseRegistration> CourseRegistrationRegUser { get; set; }
        public ICollection<CourseRegistration> CourseRegistrationUser { get; set; }
        public ICollection<DocumentType> DocumentType { get; set; }
        public ICollection<Documents> Documents { get; set; }
        public ICollection<Exam> Exam { get; set; }
        public ICollection<ExamQuestions> ExamQuestions { get; set; }
        public ICollection<ExamUser> ExamUserRegUser { get; set; }
        public ICollection<ExamUser> ExamUserUser { get; set; }
        public ICollection<ExerciseUser> ExerciseUserRegUser { get; set; }
        public ICollection<ExerciseUser> ExerciseUserUser { get; set; }
        public ICollection<FavoriteCourses> FavoriteCoursesRegUser { get; set; }
        public ICollection<FavoriteCourses> FavoriteCoursesUser { get; set; }
        public ICollection<Files> FilesRegUser { get; set; }
        public ICollection<Files> FilesUser { get; set; }
        public ICollection<Forum> Forum { get; set; }
        public ICollection<KMExperience> KmExperience { get; set; }
        public ICollection<KMNeeds> KmNeeds { get; set; }
        public ICollection<KMcategory> Kmcategory { get; set; }
        public ICollection<Lesson> Lesson { get; set; }
        public ICollection<LessonObservation> LessonObservationRegUser { get; set; }
        public ICollection<LessonObservation> LessonObservationUser { get; set; }
        public ICollection<LMS> Lms { get; set; }
        public ICollection<Logs> Logs { get; set; }
        public ICollection<Messages> MessagesReceiverUser { get; set; }
        public ICollection<Messages> MessagesRegUser { get; set; }
        public ICollection<Messages> MessagesSenderUser { get; set; }
        public ICollection<Permissions> Permissions { get; set; }
        public ICollection<PlaceLayers> PlaceLayers { get; set; }
        public ICollection<Places> Places { get; set; }
        public ICollection<Poll> Poll { get; set; }
        public ICollection<PollUser> PollUserRegUser { get; set; }
        public ICollection<PollUser> PollUserUser { get; set; }
        public ICollection<QuestionOptions> QuestionOptions { get; set; }
        public ICollection<Questions> Questions { get; set; }
        public ICollection<RelatedCourses> RelatedCourses { get; set; }
        public ICollection<RequestForContent> RequestForContentRegUser { get; set; }
        public ICollection<RequestForContent> RequestForContentUser { get; set; }
        public ICollection<RequestedCourses> RequestedCourses { get; set; }
        public ICollection<RolePermissions> RolePermissions { get; set; }
        public ICollection<Roles> Roles { get; set; }
        public ICollection<Tags> Tags { get; set; }
        public ICollection<TeacherLesson> TeacherLessonRegUser { get; set; }
        public ICollection<TeacherLesson> TeacherLessonTeacher { get; set; }
        public ICollection<TeacherLike> TeacherLikeRegUser { get; set; }
        public ICollection<TeacherLike> TeacherLikeTeacher { get; set; }
        public ICollection<TeacherLike> TeacherLikeUser { get; set; }
        public ICollection<Term> Term { get; set; }
        public ICollection<Tickets> Tickets { get; set; }
        public ICollection<TrainingCenter> TrainingCenter { get; set; }
        public ICollection<TrainingContent> TrainingContent { get; set; }
        public ICollection<TrainingEvalIndex> TrainingEvalIndex { get; set; }
        public ICollection<TrainingEvalResult> TrainingEvalResultRegUser { get; set; }
        public ICollection<TrainingEvalResult> TrainingEvalResultUser { get; set; }
        public ICollection<TrainingWeekContent> TrainingWeekContent { get; set; }
        public ICollection<UserAccess> UserAccess { get; set; }
        public ICollection<UserAccess> UserAccessPermision { get; set; }
        public ICollection<UserContent> UserContentRegUser { get; set; }
        public ICollection<UserContent> UserContentUser { get; set; }
        public ICollection<UserDocuments> UserDocumentsRegUser { get; set; }
        public ICollection<UserDocuments> UserDocumentsUser { get; set; }
        public ICollection<UserGroupMembers> UserGroupMembersRegUser { get; set; }
        public ICollection<UserGroupMembers> UserGroupMembersUser { get; set; }
        public ICollection<UserGroups> UserGroups { get; set; }
        public ICollection<UserProfiles> UserProfilesRegUser { get; set; }
        public UserProfiles UserProfile { get; set; }
        public ICollection<UserTrainingWeekContent> UserTrainingWeekContentRegUser { get; set; }
        public ICollection<UserTrainingWeekContent> UserTrainingWeekContentUser { get; set; }
        public ICollection<Access> Accesses { get; set; }
        public ICollection<UserRole> UserRolesReg { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<DefaultRoleAccess> DefaultRoleAccessesReg { get; set; }
        public ICollection<AccessPermissionGroup> AccessPermissionGroupReg { get; set; }
        public ICollection<PermissionGroup> AccessGroupReg { get; set; }
        public ICollection<UserPermissionGroup>  UserPermissionGroupReg { get; set; }
        public ICollection<UserPermissionGroup>  UserPermissionGroup { get; set; }
        public ICollection<TermLesson> TermLessons { get; set; }
        public ICollection<MenuAccess> MenuAccessReg { get; set; }
    }
    public class UsersConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("Users", "Core");

            builder.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);

            builder.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(124)
                .IsUnicode(false);

            builder.Property(e => e.RegDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            
        }
    }
}
