using DataTables.AspNet.Core;
using Seventy.Data;
using Seventy.Repository.Core;
using Seventy.Service.EDU.ExamQuestions;
using Seventy.Service.Users;
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
    public class ExamService : BaseService.BaseService<DomainClass.EDU.Exam.Exam>, IExamService
    {
        private readonly IExamQuestionsService _ExamQuestions;
        private readonly IUserManager _UserManager;

        public ExamService(IUnitOfWork uow, IExamQuestionsService examQuestionsService, IUserManager userManager) : base(uow)
        {
            _ExamQuestions = examQuestionsService;
            _UserManager = userManager;
        }

        public override IEnumerable<DomainClass.EDU.Exam.Exam> Table() => _uow.Exam.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.Exam.Exam> TableNoTracking() => _uow.Exam.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.Exam.Exam> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.Exam.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.Exam.Exam entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Exam.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.Exam.Exam> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Exam.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Exam.Exam> InsertAsync(DomainClass.EDU.Exam.Exam entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Exam.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.Exam.Exam> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Exam.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Exam.Exam> UpdateAsync(DomainClass.EDU.Exam.Exam entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Exam.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.Exam.Exam> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Exam.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public IQueryable<ExamViewModel> GetAvailableExams(Expression<Func<ExamViewModel, bool>> filter = null,
            Func<IQueryable<ExamViewModel>, IOrderedQueryable<ExamViewModel>> orderBy = null)
        {
            var currentUserID = _UserManager.GetCurrentUserID();
            var lessonList = _uow.Lesson.TableNoTracking;
            var examList = _uow.Exam.TableNoTracking;
            //جدول تخصیص آزمون به کاربر
            var userExams = _uow.ExamUser.TableNoTracking.Where(x => x.UserID == currentUserID);
            var examAnswerSheets = _uow.ExamAnswerSheet.TableNoTracking.Where(x => x.UserID == currentUserID);

            var availableExams =
                from l in lessonList
                from e in examList.Where(x => x.LessonID == l.ID)
                from ue in userExams.Where(x => x.ExamID == e.ID)
                let HasAnwersheet = examAnswerSheets.Any(x => x.ExamID == e.ID)
                select new ExamViewModel
                {
                    ID = e.ID,
                    Title = e.Title,
                    LessonID = e.LessonID,
                    LessonTitle = l.Title,
                    IsActive = e.IsActive,
                    Description = e.Description,
                    StartDate = e.StartDate.ToString(),
                    EndDate = e.EndDate.ToString(),
                    PassingGrade = e.PassingGrade,
                    RandomQuestionOptionsOrder = e.RandomQuestionOptionsOrder,
                    RandomQuestionsOrder = e.RandomQuestionsOrder,
                    RegDate = e.RegDate,
                    RegUserID = e.RegUserID,
                    FileID = e.FileID,
                    Type = e.Type,
                    ExamState = (ue.Result != null ? ExamEnums.ExamState.ResultReady :
                                  HasAnwersheet ? ExamEnums.ExamState.Done : ExamEnums.ExamState.NotDone)
                };

            if (filter != null)
            {
                availableExams = availableExams.Where(filter);
            }

            if (orderBy != null)
            {
                availableExams = orderBy(availableExams);
            }

            return availableExams;
        }

        public async Task<bool> InsertExamWithQuestionsAsync(DomainClass.EDU.Exam.Exam exam, List<DomainClass.EDU.Exam.ExamQuestions> questionList, CancellationToken cancellationToken)
        {
            int? insertedExmaID = 0;

            try
            {
                var insertedExam = await _uow.Exam.InsertAsync(exam, cancellationToken);
                await _uow.CompleteAsync(cancellationToken);

                if (insertedExam != null)
                {
                    insertedExmaID = insertedExam.ID;

                    foreach (var q in questionList)
                    {
                        var result = await _uow.ExamQuestions.InsertAsync(new DomainClass.EDU.Exam.ExamQuestions()
                        {
                            ExamID = insertedExmaID.Value,
                            QuestionID = q.QuestionID,
                            Barom = q.Barom,
                            Description = q.Description,
                            IsActive = q.IsActive,
                            RegDate = DateTime.Now,
                            RegUserID = _UserManager.GetCurrentUserID()
                        }, cancellationToken);
                        await _uow.CompleteAsync(cancellationToken);

                        if (result == null)
                            throw new Exception("ExamQuestions Insert Error");

                    }
                    return true; // Exam and questions inserted successfully
                }

                return false;// Exam or questions not inserted
            }
            catch (Exception ex)
            {
                if (insertedExmaID != 0)
                {
                    await _ExamQuestions.DeleteQuestionsByExamAsync(cancellationToken, insertedExmaID.Value);

                    var examToDelete = await _uow.Exam.GetByIDAsync(cancellationToken, insertedExmaID);

                    if (examToDelete != null)
                    {
                        await _uow.Exam.DeleteAsync(examToDelete, cancellationToken);
                        await _uow.CompleteAsync(cancellationToken);
                    }
                }
                return false;
            }
        }

        public async Task<bool> UpdateExamWithQuestionsAsync(DomainClass.EDU.Exam.Exam exam, List<DomainClass.EDU.Exam.ExamQuestions> questionList, CancellationToken cancellationToken)
        {
            try
            {
                if (exam is null || exam.ID == null)
                    return false;

                var res = await _ExamQuestions.DeleteQuestionsByExamAsync(cancellationToken, exam.ID);

                if (res == false)
                    return false; // old questions not deleted

                await _uow.Exam.UpdateAsync(exam, cancellationToken);
                await _uow.CompleteAsync(cancellationToken);

                foreach (var q in questionList)
                {
                    await _uow.ExamQuestions.InsertAsync(new DomainClass.EDU.Exam.ExamQuestions()
                    {
                        ExamID = (int)exam.ID,
                        QuestionID = (int)q.ID,
                        Barom = q.Barom,
                        Description = q.Description,
                        IsActive = q.IsActive,
                        RegDate = DateTime.Now,
                        RegUserID = _UserManager.GetCurrentUserID()
                    }, cancellationToken);
                }
                await _uow.CompleteAsync(cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public override async Task<PagedList<DomainClass.EDU.Exam.Exam>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.Exam.Exam, bool>> filter = null, Func<IQueryable<DomainClass.EDU.Exam.Exam>, IOrderedQueryable<DomainClass.EDU.Exam.Exam>> orderBy = null)
        {
            return await _uow.Exam.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }

        public async Task<GridResponseModel> LoadDataAsync(IDataTablesRequest request, CancellationToken cancellationToken = default)
        {
            var data = await _uow.Exam.LoadDataAsync(request,cancellationToken);
            return data;
        }

        //public async  Task<bool> UserCanStartExam(CancellationToken cancellationToken, int ExamId, int UserId)
        //{
        //    if (ExamId == 0 || UserId == 0)
        //        return false;
        //    var exam = await _uow.Exam.GetByIDAsync(cancellationToken,ExamId);

        //}
    }
}
