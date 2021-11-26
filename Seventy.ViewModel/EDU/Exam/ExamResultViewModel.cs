namespace Seventy.ViewModel.EDU
{
    public class ExamResultViewModel  
    {
        public int TotalQuestionCount { get; set; }

        public int CorrectAnswerCount { get; set; }

        public double? Result { get; set; }

        public bool IsAnswersheetSaved { get; set; }
        public string Message { get; set; }

    }
}
