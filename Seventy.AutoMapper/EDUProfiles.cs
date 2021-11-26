using Seventy.ViewModel.EDU.Exam;
using Seventy.ViewModel.EDU.Lesson;
using Seventy.ViewModel.EDU.TermLesson;

namespace Seventy.ViewModel.EDU
{
    using AutoMapper;
    using Extensions;
    using Seventy.DomainClass.Core;
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
    using Seventy.ViewModel.Core;
    using Seventy.ViewModel.EDU.CertificateUser;
    using Seventy.ViewModel.EDU.Course;
    using Seventy.ViewModel.EDU.Exam.ExamAnswerSheet;

    public partial class EDUProfiles : Profile
    {
        public EDUProfiles()
        {
            #region ViewModel
            CreateMap<CourseRegistrationViewModel, CourseRegistration>().ReverseMap();
            CreateMap<CourseRegistrationEditModel, CourseRegistration>().ReverseMap();
            CreateMap<UserLessonViewModel, UserLesson>().ReverseMap();
            CreateMap<CertificateUserViewModel, DomainClass.EDU.CertificateUser>().ReverseMap();
            
            CreateMap<ExerciseViewModel, Exercise>().ReverseMap();
            CreateMap<ExamViewModel, DomainClass.EDU.Exam.Exam>().ReverseMap();
            CreateMap<ExamViewModel, DomainClass.EDU.Exam.Exam>()
                .ForMember(x => x.StartDate, opt => opt.MapFrom(x => x.StartDate.ToGeorgianDateTime()))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(x => x.EndDate.ToGeorgianDateTime()));
            CreateMap<TeacherLessonViewModel, TeacherLesson>().ReverseMap();
            CreateMap<TeacherLikeViewModel, TeacherLike>().ReverseMap();
            CreateMap<CertificateViewModel, Certificate>().ReverseMap();
            CreateMap<ForumViewModel, Forum>().ReverseMap();
            CreateMap<QuestionsViewModel, Questions>().ReverseMap();
            CreateMap<ContentObservationViewModel, ContentObservation>().ReverseMap();
            CreateMap<CourseCategoryViewModel, CourseCategory>().ReverseMap();
            CreateMap<CourseGroupsViewModel, CourseGroups>().ReverseMap();
            CreateMap<CourseObservationViewModel, CourseObservation>().ReverseMap();
            CreateMap<ExamAnswerSheetViewModel, ExamAnswerSheet>().ReverseMap();
            CreateMap<ExamQuestionsViewModel, ExamQuestions>().ReverseMap();

            CreateMap<UserTrainingWeekContentViewModel, UserTrainingWeekContent>().ReverseMap();
            CreateMap<TrainingEvalIndexViewModel, TrainingEvalIndex>().ReverseMap();
            CreateMap<TrainingEvalResultViewModel, TrainingEvalResult>().ReverseMap();
            CreateMap<TrainingWeekListViewModel, DomainClass.EDU.TrainingWeek.TrainingWeek >().ReverseMap();
            CreateMap<TrainingContentViewModel, TrainingContent>().ReverseMap();

            CreateMap<ExamUserViewModel, ExamUser>().ReverseMap();
            CreateMap<ExerciseUserViewModel, ExerciseUser>().ReverseMap();
            CreateMap<TrainingWeekContentViewModel, TrainingWeekContent>().ReverseMap();
            CreateMap<FavoriteCoursesViewModel, FavoriteCourses>().ReverseMap();
            CreateMap<PollUserViewModel, PollUser>().ReverseMap();
            CreateMap<TrainingEvalIndexViewModel, TrainingEvalIndex>().ReverseMap();
            CreateMap<TrainingEvalResultViewModel, TrainingEvalResult>().ReverseMap();
            CreateMap<QuestionOptionsViewModel, QuestionOptions>().ReverseMap();
            CreateMap<CourseViewModel, DomainClass.EDU.Course.Course>().ReverseMap();
            CreateMap<LessonObservationViewModel, LessonObservation>().ReverseMap();
            CreateMap<LMSViewModel, LMS>().ReverseMap();
            CreateMap<RequestedCoursesViewModel, RequestedCourses>().ReverseMap();
            CreateMap<PollViewModel, Poll>().ReverseMap();
            CreateMap<UserContentViewModel, UserContent>().ReverseMap();
            CreateMap<RelatedCoursesViewModel, RelatedCourses>().ReverseMap();
            CreateMap<TermViewModel, Term>().ReverseMap();
            CreateMap<RequestForContentViewModel, RequestForContent>().ReverseMap();
            CreateMap<CateringPackageViewModel, CateringPackage>().ReverseMap();
            CreateMap<TrainingCenterViewModel, TrainingCenter>().ReverseMap();
            CreateMap<LessonViewModel, DomainClass.EDU.Lesson.Lesson>().ReverseMap();
            CreateMap<TermLessonViewModel, DomainClass.EDU.Term.TermLesson>().ReverseMap();
            #endregion

            #region EditModel
            CreateMap<DocumentTypeEditModel, DocumentType>().ReverseMap();
            CreateMap<CourseCategoryEditModel, CourseCategory>().ReverseMap();
            CreateMap<RelatedCoursesEditModel, RelatedCourses>().ReverseMap();
            CreateMap<TeacherLessonEditModel, TeacherLesson>().ReverseMap();
            CreateMap<TermEditModel, Term>().ReverseMap();
            CreateMap<TermLessonEditModel, DomainClass.EDU.Term.TermLesson>().ReverseMap();
            CreateMap<TrainingWeekContentEditModel, TrainingWeekContent>().ReverseMap();
            CreateMap<TrainingWeekEditModel, DomainClass.EDU.TrainingWeek.TrainingWeek>().ReverseMap();
            CreateMap<QuestionEditViewModel, Questions>().ReverseMap();
            CreateMap<LessonEditViewModel, DomainClass.EDU.Lesson.Lesson>().ReverseMap();
            #endregion
        }
    }
}

