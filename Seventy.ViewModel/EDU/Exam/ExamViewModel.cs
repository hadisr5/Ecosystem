using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Seventy.DomainClass.EDU.Exam;

namespace Seventy.ViewModel.EDU
{
    public class ExamViewModel : CoreBaseViewModel 
    {
		[Display(Name = "نام درس")]
		public int LessonID { get; set; }
		[Display(Name = "درس")]
		public string LessonTitle { get; set; }
		[Display(Name = "عنوان آزمون")]
		public string Title { get; set; }
		[Display(Name = "شروع")]
		public string StartDate { get; set; }
		[Display(Name = "خاتمه")]
		public string EndDate { get; set; }
		[Display(Name = "تعداد سوال")]
		public int QuestionCount { get; set; }
		[Display(Name = "بارم")]
		public int Barom { get; set; }
		[Display(Name ="نمره قبولی")]
		public int PassingGrade { get; set; }
		
        [Display(Name ="نوع آزمون")]
        public string Type { get; set; }

		[Display(Name = "ترتیب تصادفی سوالات")]
		public bool RandomQuestionsOrder { get; set; } = false;
		[Display(Name = "ترتیب تصادفی گزینه ها")]
		public bool RandomQuestionOptionsOrder { get; set; } = false;
		[Display(Name ="وضعیت آزمون")]
		public ExamEnums.ExamState ExamState { get; set; }

        [Display(Name ="فایل")]
        public int? FileID { get; set; }

		[Display(Name = "مدت زمان (دقیقه)")]
		public int Time { get; set; }

		[Display(Name = "تاریخ شروع به آزمون")]
		public DateTime StartTime { get; set; }


		[Display(Name = "سوال ها")]
        public ICollection<ExamQuestions> Questions { get; set; }
    }
}
