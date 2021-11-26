using Seventy.Data;
using Seventy.Service.BaseService;
using Seventy.ViewModel.EDU;
using Seventy.ViewModel.EDU.Exam.ExamAnswerSheet;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.ExamAnswerSheet
{
    public interface IExamAnswerSheetService : IBaseService<DomainClass.EDU.Exam.ExamAnswerSheet>
    {
        Task<IEnumerable<ExamUserViewModel>> GetPendingAnswersheetsForTeacher(int teacherUserID);
        /// <summary>
        /// Evaluate answersheet by teacher and update in database
        /// </summary>
        /// <param name="answerSheet"></param>
        /// <returns></returns>
        Task<ExamResultViewModel> EvaluateExamByTeacherAsync(IEnumerable<ExamAnswerSheetViewModel> answerSheet, CancellationToken cancellationToken);
        /// <summary>
        /// نمایش پاسخ نامه جهت تصحیح سوالات یا مشاهده
        /// </summary>
        /// <param name="examID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<IEnumerable<ExamAnswerSheetViewModel>> GetExamAnswerSheetByUser(int examID, int userID);
        Task<ExamResultViewModel> GetExamResultByUser(int examID, int UserID, CancellationToken cancellationToken);
        Task<ExamResultViewModel> SaveExamAnswersheetAsync(IEnumerable<ExamAnswerSheetViewModel> answerSheet,CancellationToken cancellationToken);

        Task<ExamViewModel> GetExam(int examId);
    }
}
