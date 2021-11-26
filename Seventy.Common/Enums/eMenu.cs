using System.ComponentModel.DataAnnotations;

namespace Seventy.Common.Enums
{
    public enum eMenu
    {
        [Display(Name = "آزمون ساز عمومی")]
        GeneralExamMaker = 0,
        
        [Display(Name = "تخصیص آزمون آزمون به فراگیر")]
        AllocateExamToStudent = 1,
        
        [Display(Name = "آزمون های منتظر تصحیح")]
        WaitningExamsForReview = 2,
        
        [Display(Name = "دوره های آموزشی تک مهارتی")]
        SingleSkillCourse = 3,
        
        [Display(Name = "دوره های آموزشی بلند مدت")]
        LongCourses = 4,
        
        [Display(Name = "لیست درس گفتار ها")]
        SpeechLessons = 5,
        
        [Display(Name = "لیست آزمون ها")]
        ExamList = 6,

        [Display(Name = "لیست تمرین ها")]
        ExerciseList = 7,
        
        [Display(Name = "لیست کوئیز ها")]
        QuizList = 8,

        [Display(Name = "ثبت مدارک کاربر")]
        SubmitUserDocuments = 9,
        
        [Display(Name = "ثبت نام در دوره")]
        RegisterInCourse = 10,
        
        [Display(Name = "مدیریت درس")]
        LessonManagement = 11,
        
        [Display(Name = "تخصیص درس به ترم")]
        AllocateLessonToTerm = 12,
        
        [Display(Name = "تخصیص محتوا به درس گفتار")]
        AllocateContentToTraining = 13,
        
        [Display(Name = "صدور گواهینامه")]
        CertificateIssued = 14,
        
        [Display(Name = "دوره های درخواستی")]
        RequestedCourses = 15,
        
        [Display(Name = "نتیجه ارزیابی شاخص")]
        TheResultOfTheEvaluation = 16,
        
        [Display(Name = "مدیریت دوره")]
        CourseManagement = 17,
        
        [Display(Name = "دسته بندی دوره")]
        CourseCategories = 18,
        
        [Display(Name = "مدیریت گروه آموزشی")]
        CourseGroupManagement = 19,
        
        [Display(Name = "مدیریت ترم ها")]
        TermManagement = 20,
        
        [Display(Name = "مدیریت درس ها")]
        LessonsManagement = 21,
        
        [Display(Name = "مدیریت درس گفتار")]
        TrainingWeeksManagement = 22,
        
        [Display(Name = "مدیریت محتوای آموزشی")]
        TrainingContentManagement = 23,
        
        [Display(Name = "بانک سوالات")]
        QuestionsBank = 24,
        
        [Display(Name = "ثبت پکیج پذیرایی")]
        AddCateringPackage = 25,
        
        [Display(Name = "ثبت نوع مدرک")]
        AddDocument = 26,
        
        [Display(Name = "دوره های مرتبط")]
        RelatedCourses = 27,
        
        [Display(Name = "تعریف شاخص ارزیابی")]
        CreateTrainingEval = 28,
        
        [Display(Name = "فایل منیجر")]
        FileManager = 29,
    }
}
