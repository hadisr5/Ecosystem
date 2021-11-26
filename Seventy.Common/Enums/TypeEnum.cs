using System.ComponentModel.DataAnnotations;

namespace Seventy.Common.Enums
{
    public enum TypeEnum
    {
        [Display(Name = "آزمون")]
        Exam = 1,

        [Display(Name = "تمرین")]
        Exercise = 2,

        [Display(Name = "کوییز")]
        Quiz = 3,

        [Display(Name = "همه")]
        All = 4
    }

    public enum CourseEnum
    {
        [Display(Name = "بلند مدت")]
        Long = 1,

        [Display(Name = "تک مهارتی")]
        Single = 2,

        [Display(Name = "همه")]
        All = 3
    }

    public enum FileTypeEnum
    {
        [Display(Name = "HTML")]
        HTML = 2,

        [Display(Name = "ویدیو")]
        Video = 1,

        [Display(Name = "سایر")]
        Others = 0
    }

    public enum ContentTypeEnum
    {
        [Display(Name = "ویدیو")]
        Video = 1,

        [Display(Name = "HTML")]
        Html = 2,

        [Display(Name = "کتابخانه")]
        Library = 3,

        [Display(Name = "همه")]
        All = 4
    }
}
