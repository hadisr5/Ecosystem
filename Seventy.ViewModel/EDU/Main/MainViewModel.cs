using Seventy.ViewModel.EDU.Course;
using Seventy.ViewModel.EDU.TrainingWeek.TrainingWeekSituationSummary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU.Main
{
    public enum CourseState
    {
        InProgress = 0,
        Pending = 1,
        Done = 2
    }
    public class MainTeacherViewModel : CoreBaseViewModel
    {
        [Display(Name = "درس ها")]
        public List<MainTeacherLessonViewModel> LessonsWithExams { get; set; }
    }

    public class MainStudentViewModel : CoreBaseViewModel
    {
        public int Balance { get; set; }
        public List<UserCourseDashboardViewModel> UserCourses { get; set; }
        public List<TermViewModel> Courses { get; set; }
        public List<TrainingWeekSituationSummaryViewModel> SummaryViewModel { get; set; }
        public int Progress { get; set; }
        //[Display(Name = "دوره ها")]
        //public List<MainUserContentViewModel> Contents { get; set; }
    }

    public class UserCourseDashboardViewModel
    {
        public int CourseID { get; set; }

        [Display(Name = "عنوان دوره")]
        public string Title { get; set; }

        [Display(Name = "مدت دوره")]
        public int Duration { get; set; }

        [Display(Name = "نوع حضور")]
        public string HozoriType { get; set; }

        [Display(Name = "مبلغ دوره")]
        public int Price { get; set; }

        [Display(Name = "تصویر دوره")]
        public string PhotoURL { get; set; }

        [Display(Name = "مدرس دوره")]
        public string CourseTeacher { get; set; }

        [Display(Name = "تصویر دوره")]
        public int? PhotoFileID { get; set; }       

        [Display(Name = "تاریخ شروع")]
        public DateTime StartDate { get; set; }

        [Display(Name = "تاریخ پایان")]
        public DateTime EndDate { get; set; }
        
        [Display(Name = "وضعیت")]
        public CourseState State { get; set; }
    }

    public class MainTeacherLessonViewModel : CoreBaseViewModel
    {
        public int LessonId { get; set; }

        [Display(Name = "نام درس")]
        public string LessonName { get; set; }

        [Display(Name = "عکس درس")]
        public int? PicId { get; set; }

        [Display(Name = "آزمون ها")]
        public ICollection<DomainClass.EDU.Exam.Exam> Exams { get; set; }
    }

    public class MainUserContentViewModel : CoreBaseViewModel
    {
        [Display(Name = "عنوان دوره")]
        public string TrainingContentTitle { get; set; }

        [Display(Name = "دوره")]
        public int TrainingContentId { get; set; }

        [Display(Name = "میزان پیشرفت")]
        public int? Progress { get; set; }

        [Display(Name = "میزان رضایت")]
        public int? LikeRank { get; set; }
    }
}
