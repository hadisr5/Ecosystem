using DataTables.AspNet.Core;
using Seventy.Data;
using Seventy.Service.BaseService;
using Seventy.ViewModel;
using Seventy.ViewModel.EDU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.Exam
{
    public interface IExamService : BaseService.IBaseService<DomainClass.EDU.Exam.Exam>
    {
        IQueryable<ExamViewModel> GetAvailableExams(Expression<Func<ExamViewModel, bool>> filter = null,
            Func<IQueryable<ExamViewModel>, IOrderedQueryable<ExamViewModel>> orderBy = null);
        /// <summary>
        /// ثبت آزمون به همراه سوالات مربوطه
        /// </summary>
        /// <param name="exam"></param>
        /// <param name="questionList"></param>
        /// <returns></returns>
        Task<bool> InsertExamWithQuestionsAsync(DomainClass.EDU.Exam.Exam exam, List<DomainClass.EDU.Exam.ExamQuestions> questionList, CancellationToken cancellationToken);
        /// <summary>
        /// اصلاح آزمون به همراه سوالات مربوطه
        /// </summary>
        /// <param name="exam"></param>
        /// <param name="questionList"></param>
        /// <returns></returns>
        Task<bool> UpdateExamWithQuestionsAsync(DomainClass.EDU.Exam.Exam exam, List<DomainClass.EDU.Exam.ExamQuestions> questionList, CancellationToken cancellationToken);

        public Task<GridResponseModel> LoadDataAsync(IDataTablesRequest request, CancellationToken cancellationToken = default);
    }
}
