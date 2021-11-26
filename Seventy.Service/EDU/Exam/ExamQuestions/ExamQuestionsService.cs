using Microsoft.EntityFrameworkCore;
using Seventy.Data;
using Seventy.Repository.Core;
using Seventy.ViewModel.EDU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.ExamQuestions
{
    public class ExamQuestionsService : BaseService.BaseService<DomainClass.EDU.Exam.ExamQuestions>, IExamQuestionsService
    {
        public ExamQuestionsService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.Exam.ExamQuestions> Table() => _uow.ExamQuestions.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.Exam.ExamQuestions> TableNoTracking() => _uow.ExamQuestions.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.Exam.ExamQuestions> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.ExamQuestions.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.Exam.ExamQuestions entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.ExamQuestions.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.Exam.ExamQuestions> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.ExamQuestions.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Exam.ExamQuestions> InsertAsync(DomainClass.EDU.Exam.ExamQuestions entity, CancellationToken cancellationToken)
        {
            var checkNotExist = await _uow.ExamQuestions.TableNoTracking
                .AnyAsync(a => a.QuestionID.Equals(entity.QuestionID) &&
                               a.ExamID.Equals(entity.ExamID), cancellationToken);

            if (checkNotExist)
                return null;

            var result = await _uow.ExamQuestions.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.Exam.ExamQuestions> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.ExamQuestions.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Exam.ExamQuestions> UpdateAsync(DomainClass.EDU.Exam.ExamQuestions entity, CancellationToken cancellationToken)
        {
            var result = await _uow.ExamQuestions.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.Exam.ExamQuestions> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.ExamQuestions.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public async Task<bool> DeleteQuestionsByExamAsync(CancellationToken cancellationToken, int? examID)
        {
            try
            {
                var q = await _uow.ExamQuestions.Table.Where(x => x.ExamID == examID).ToListAsync();
                await _uow.ExamQuestions.DeleteRangeAsync(q, cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<QuestionsViewModel>> GetQuestionsByExamAsync(int examID)
        {
            try
            {
                var examQuestions = _uow.ExamQuestions.TableNoTracking.Where(x => x.ExamID == examID);
                var questions = _uow.Questions.TableNoTracking;
                var questionOptions = _uow.QuestionOptions.TableNoTracking;
                var lessons = _uow.Lesson.TableNoTracking;

                var questionList = from eq in examQuestions
                                   join q in questions on eq.QuestionID equals q.ID
                                   join l in lessons on q.LessonID equals l.ID
                                   select new QuestionsViewModel
                                   {
                                       ID = q.ID,
                                       LessonTitle = l.Title,
                                       Title = q.Title,
                                       Barom = eq.Barom,
                                       Description = q.Description,
                                       IsActive = q.IsActive,
                                       MultiOption = q.MultiOption,
                                       //AnswerOptions = q.a
                                       QuestionLevel = q.QuestionLevel,
                                       RegDate = q.RegDate,
                                       RegUserID = q.RegUserID
                                   };

                return await questionList.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public override async Task<PagedList<DomainClass.EDU.Exam.ExamQuestions>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.Exam.ExamQuestions, bool>> filter = null, Func<IQueryable<DomainClass.EDU.Exam.ExamQuestions>, IOrderedQueryable<DomainClass.EDU.Exam.ExamQuestions>> orderBy = null)
        {
            return await _uow.ExamQuestions.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }

        public async Task<Seventy.DomainClass.EDU.Exam.ExamQuestions> GetQuestionByExamAsync(int id,int examID)
        {
            return await _uow.ExamQuestions.Table.Where(x => x.QuestionID == id && x.ExamID == examID).FirstOrDefaultAsync();
        }
    }
}
