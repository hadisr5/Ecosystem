using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.EDU
{
    
    public class TrainingContentEvalIndexViewModel : CoreBaseViewModel 
    {
        [Display(Name = "محتوی")]
        public int ContentID { get; set; }
		[Display(Name = "دسته بندی شاخص")]
		public string IndexCategory { get; set; }

    }
}
