using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Seventy.Data;
using Seventy.Repository.Core;
using Seventy.Service.EDU.ExamQuestions;
using Seventy.Service.Users;
using Seventy.ViewModel.EDU;
using Seventy.ViewModel.EDU.Exam.ExamAnswerSheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using ExamViewModel = Seventy.ViewModel.EDU.ExamViewModel;

namespace Seventy.Service.EDU.ExamAnswerSheet
{
    public class ExamAnswerSheetService : BaseService.BaseService<DomainClass.EDU.Exam.ExamAnswerSheet>, IExamAnswerSheetService
    {
        private readonly IExamQuestionsService _ExamQuestionsService;
        private readonly IUserManager _UserManager;
        private readonly IMapper _Mapper;

        public ExamAnswerSheetService(IUnitOfWork uow, IExamQuestionsService examQuestionsService, IUserManager userManager, IMapper mapper) : base(uow)
        {
            _ExamQuestionsService = examQuestionsService;
            _UserManager = userManager;
            _Mapper = mapper;
        }

        public override IEnumerable<DomainClass.EDU.Exam.ExamAnswerSheet> Table() => _uow.ExamAnswerSheet.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.Exam.ExamAnswerSheet> TableNoTracking() => _uow.ExamAnswerSheet.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.Exam.ExamAnswerSheet> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.ExamAnswerSheet.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.Exam.ExamAnswerSheet entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.ExamAnswerSheet.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.Exam.ExamAnswerSheet> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.ExamAnswerSheet.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Exam.ExamAnswerSheet> InsertAsync(DomainClass.EDU.Exam.ExamAnswerSheet entity, CancellationToken cancellationToken)
        {
            var exam = await _uow.Exam
                .TableNoTracking
                .SingleAsync(a => a.ID.Equals(entity.ExamID), cancellationToken);

            if (exam.StartDate > DateTime.Now || exam.EndDate < DateTime.Now)
                return null;

            if (entity.AnswerOption != null)
            {
                var questionOption = await _uow.QuestionOptions
                    .TableNoTracking
                    .SingleAsync(a => a.ID.Equals(entity.AnswerOption),
                        cancellationToken);

                var examQuestion = await _uow.ExamQuestions
                    .TableNoTracking
                    .SingleAsync(a => a.ExamID.Equals(entity.ExamID) &&
                                      a.QuestionID.Equals(entity.QuestionID), cancellationToken);

                if (questionOption.IsCorrect)
                    entity.AchievedBarom += examQuestion.Barom;
                else
                    entity.AchievedBarom += examQuestion.Barom;
            }

            var result = await _uow.ExamAnswerSheet.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.Exam.ExamAnswerSheet> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.ExamAnswerSheet.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Exam.ExamAnswerSheet> UpdateAsync(DomainClass.EDU.Exam.ExamAnswerSheet entity, CancellationToken cancellationToken)
        {
            var result = await _uow.ExamAnswerSheet.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.Exam.ExamAnswerSheet> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.ExamAnswerSheet.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public async Task<IEnumerable<ExamUserViewModel>> GetPendingAnswersheetsForTeacher(int teacherUserID)
        {
            var examUser = _uow.ExamUser.TableNoTracking.Where(x => x.Result == null);
            var examList = _uow.Exam.TableNoTracking;
            var userList = _uow.UserProfiles.TableNoTracking;

            var pendingAnswerSheets = from eu in examUser
                                      from e in examList.Where(x => x.ID == eu.ExamID)
                                      from u in userList.Where(x => x.ID == eu.UserID)
                                      select new ExamUserViewModel
                                      {
                                          ExamID = eu.ExamID,
                                          ExamTitle = e.Title,
                                          IsActive = eu.IsActive,
                                          Result = null,
                                          UserID = eu.UserID,
                                          UserNameAndFamily = u.FirstName + " " + u.LastName
                                      };

            //ToDo filter Exams by teacher

            return await pendingAnswerSheets.ToListAsync();
        }

        public async Task<ExamResultViewModel> EvaluateExamByTeacherAsync(IEnumerable<ExamAnswerSheetViewModel> answerSheet, CancellationToken cancellationToken)
        {
            int correctAnswerCount = 0;
            double totalBarom = 0;
            var examID = answerSheet.FirstOrDefault()?.ExamID;

            if (answerSheet == null)
                return new ExamResultViewModel { IsAnswersheetSaved = false, Message = "Null Answersheet" };

            if (examID == null)
                return new ExamResultViewModel { IsAnswersheetSaved = false, Message = "ExamID is null in answersheet" };

            if (answerSheet.Any(x => x.ID == null))
                return new ExamResultViewModel { IsAnswersheetSaved = false, Message = "ID of some rows are null" };

            int userID = answerSheet.First().UserID;

            var examDetails = await _uow.Exam.GetByIDAsync(cancellationToken, examID);

            var listQuestions = await _ExamQuestionsService.GetQuestionsByExamAsync(examID.Value);

            //لیست سوالات تشریحی که تصحیح نشده اند
            var missingQuestionNumbers = from q in listQuestions.Where(x => x.MultiOption == false)
                                         from a in answerSheet.Where(x => x.QuestionID == q.ID && x.AchievedBarom == null)
                                         select new { a.RowNumber };

            if (missingQuestionNumbers != null)
                return new ExamResultViewModel { IsAnswersheetSaved = false, Message = "سوالات " + string.Join(",", missingQuestionNumbers) + " تصحیح نشده اند" };

            //تصحیح سوالات
            foreach (var q in answerSheet)
            {
                var questionDetails = listQuestions.Where(x => x.ID == q.ID).FirstOrDefault();

                if (questionDetails.MultiOption)
                {
                    //correctAnswerCount += (questionDetails.Answer == q.Answer ? 1 : 0);
                    //totalBarom += (questionDetails.Answer == q.Answer ? questionDetails.Barom : 0);
                    //q.AchievedBarom = (questionDetails.Answer == q.Answer ? questionDetails.Barom : 0);
                }
                else
                {
                    correctAnswerCount += (q.AchievedBarom == 0 ? 0 : 1); // اگر بخشی از نمره سوال تشریحی را گرفت به عنوان سوال پاسخ داده شده حساب کن
                    totalBarom += q.AchievedBarom.Value;// بارم بدست آمده برای سوال تشریحی
                }
            }

            using (TransactionScope t = new TransactionScope())
            {
                try
                {
                    #region Update ExamUser
                    var examUser = _uow.ExamUser.Table.Where(x => x.ExamID == examID && x.UserID == userID);

                    if (examUser == null || examUser.Count() > 1)
                    {
                        return new ExamResultViewModel { IsAnswersheetSaved = false, Message = "ExamUser not found to update." };
                    }

                    var foundExamUser = examUser.First();
                    foundExamUser.Result = totalBarom;

                    await _uow.ExamUser.UpdateAsync(foundExamUser, cancellationToken);
                    await _uow.CompleteAsync(cancellationToken);
                    #endregion

                    var answerSheetList = _Mapper.Map<List<DomainClass.EDU.Exam.ExamAnswerSheet>>(answerSheet);
                    await _uow.ExamAnswerSheet.UpdateRangeAsync(answerSheetList, cancellationToken);
                    await _uow.CompleteAsync(cancellationToken);

                    t.Complete();
                    t.Dispose();

                    return new ExamResultViewModel
                    {
                        TotalQuestionCount = listQuestions.Count(),
                        CorrectAnswerCount = correctAnswerCount,
                        IsAnswersheetSaved = true,
                        Message = (totalBarom >= examDetails.PassingGrade ? "شما در آزمون موفق شدید" :
                                    "شما در آزمون نمره قبولی (" + examDetails.PassingGrade + ") را کسب ننموده اید"),
                        Result = totalBarom
                    };
                }
                catch (Exception ex)
                {
                    t.Dispose();
                    return new ExamResultViewModel { IsAnswersheetSaved = false, Message = ex.Message };
                }
            }

        }

        public async Task<IEnumerable<ExamAnswerSheetViewModel>> GetExamAnswerSheetByUser(int examID, int userID)
        {
            var examAnswerSheet = _uow.ExamAnswerSheet.TableNoTracking
                .Where(x => x.ExamID == examID && x.UserID == userID);

            var questions = _uow.Questions.TableNoTracking;
            var examQuestion = _uow.ExamQuestions.TableNoTracking;

            var query =
                from item in examAnswerSheet
                from question in questions.Where(x => x.ID.Equals(item.QuestionID))
                from eq in examQuestion.Where(a => a.ExamID.Equals(examID) && a.QuestionID.Equals(question.ID))
                select new ExamAnswerSheetViewModel
                {
                    ID = item.ID,
                    Description = item.Description,
                    IsActive = item.IsActive,
                    RegDate = item.RegDate,
                    RegUserID = item.RegUserID,
                    UserID = item.UserID,
                    QuestionID = item.QuestionID,
                    ExamID = item.ExamID,
                    Answer = item.Answer,
                    ExamTitle = "",
                    QuestionBarom = eq.Barom,
                    QuestionTitle = question.Title,
                    QuestionType = question.MultiOption
                };

            // var examAnswerSheetViewModel = _Mapper
            //     .Map<IEnumerable<ExamAnswerSheetViewModel>>(examAnswerSheet);

            return query;
        }

        public async Task<ExamResultViewModel> GetExamResultByUser(int examID, int userID, CancellationToken cancellationToken)
        {
            int correctAnswerCount = 0;
            float totalBarom = 0;

            var examAnswerSheet = _uow.ExamAnswerSheet.TableNoTracking.Where(x => x.ExamID == examID && x.UserID == userID);
            var listQuestions = await _ExamQuestionsService.GetQuestionsByExamAsync(examID);
            var examDetails = await _uow.Exam.GetByIDAsync(cancellationToken, examID);

            foreach (var q in examAnswerSheet)
            {
                var questionDetails = listQuestions.Where(x => x.ID == q.ID).FirstOrDefault();

                //correctAnswerCount += (questionDetails.Answer == q.Answer ? 1 : 0);
                //totalBarom += (questionDetails.Answer == q.Answer ? questionDetails.Barom : 0);
            }

            return new ExamResultViewModel
            {
                CorrectAnswerCount = correctAnswerCount,
                TotalQuestionCount = listQuestions.Count(),
                Message = (totalBarom >= examDetails.PassingGrade ? "شما در آزمون موفق شدید" :
                            "شما در آزمون نمره قبولی (" + examDetails.PassingGrade + ") را کسب ننموده اید"),
                Result = totalBarom
            };
        }

        public async Task<ExamResultViewModel> SaveExamAnswersheetAsync(IEnumerable<ExamAnswerSheetViewModel> answerSheet, CancellationToken cancellationToken)
        {
            int correctAnswerCount = 0;
            double totalBarom = 0;
            var currentUserID = _UserManager.GetCurrentUserID();
            var examID = answerSheet.FirstOrDefault()?.ExamID;

            if (answerSheet == null)
                return new ExamResultViewModel { IsAnswersheetSaved = false, Message = "Null Answersheet" };

            if (examID == null)
                return new ExamResultViewModel { IsAnswersheetSaved = false, Message = "ExamID is null in answersheet" };

            var examDetails = await _uow.Exam.GetByIDAsync(cancellationToken, examID);

            var listQuestions = await _ExamQuestionsService.GetQuestionsByExamAsync(examID.Value);

            bool HasDescriptiveQuestion = listQuestions.Any(x => x.MultiOption == false);


            if (!HasDescriptiveQuestion) // اگر سوال تشریحی وجود دارد تصحیح سوالات به مرحله دوم موکول میشود
            {
                //تصحیح سوالات
                foreach (var q in answerSheet)
                {
                    var questionDetails = listQuestions.Where(x => x.ID == q.ID).FirstOrDefault();

                    //correctAnswerCount += (questionDetails.Answer == q.Answer ? 1 : 0);
                    //totalBarom += (questionDetails.Answer == q.Answer ? questionDetails.Barom : 0);
                    //q.AchievedBarom = (questionDetails.Answer == q.Answer ? questionDetails.Barom : 0);
                }
            }

            using (TransactionScope t = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var examUser = new DomainClass.EDU.Exam.ExamUser()
                    {
                        ExamID = examID.Value,
                        UserID = currentUserID.Value,
                        IsActive = true,
                        RegUserID = currentUserID,
                        RegDate = DateTime.Now,
                        Result = (HasDescriptiveQuestion == true ? (double?)null : totalBarom)
                    };

                    await _uow.ExamUser.UpdateAsync(examUser, cancellationToken);
                    await _uow.CompleteAsync(cancellationToken);


                    var answerSheetList = _Mapper.Map<List<DomainClass.EDU.Exam.ExamAnswerSheet>>(answerSheet);
                    await _uow.ExamAnswerSheet.InsertRangeAsync(answerSheetList, cancellationToken);
                    await _uow.CompleteAsync(cancellationToken);


                    t.Complete();
                    t.Dispose();

                    return new ExamResultViewModel
                    {
                        TotalQuestionCount = listQuestions.Count(),
                        CorrectAnswerCount = correctAnswerCount,
                        IsAnswersheetSaved = true,
                        Message = (HasDescriptiveQuestion == true ? "نتیجه آزمون بعد از تصحیح سوالات تشریحی قابل مشاهده است" :
                                    (totalBarom >= examDetails.PassingGrade ? "شما در آزمون موفق شدید" :
                                    "شما در آزمون نمره قبولی (" + examDetails.PassingGrade + ") را کسب ننموده اید")),
                        Result = (HasDescriptiveQuestion == true ? (double?)null : totalBarom)
                    };
                }
                catch (Exception ex)
                {
                    t.Dispose();
                    return new ExamResultViewModel { IsAnswersheetSaved = false, Message = ex.Message };
                }
            }

        }

        public async Task<PagedList<ExamAnswerSheetViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters,
            Expression<Func<ExamAnswerSheetViewModel, bool>> filter = null,
            Func<IQueryable<ExamAnswerSheetViewModel>,
            IOrderedQueryable<ExamAnswerSheetViewModel>> orderBy = null)
        {
            try
            {
                var items = _uow.ExamAnswerSheet.TableNoTracking;
                var exams = _uow.Exam.TableNoTracking;
                var questions = _uow.Questions.TableNoTracking;

                var query =
                    from item in items
                    from exam in exams.Where(x => x.ID.Equals(item.ExamID))
                    from question in questions.Where(x => x.ID.Equals(item.QuestionID))
                    select new ExamAnswerSheetViewModel
                    {
                        ID = item.ID,
                        Description = item.Description,
                        IsActive = item.IsActive,
                        RegDate = item.RegDate,
                        RegUserID = item.RegUserID,
                        UserID = item.UserID,
                    };

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return await PagedList<ExamAnswerSheetViewModel>.ToPagedList(orderBy(query),
                        genericPagingParameters.PageNumber,
                        genericPagingParameters.PageSize);
                }
                else
                {
                    return await PagedList<ExamAnswerSheetViewModel>.ToPagedList(query,
                        genericPagingParameters.PageNumber,
                        genericPagingParameters.PageSize);
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<ExamViewModel> GetExam(int examId)
        {
            try
            {
                var item = await _uow.Exam.TableNoTracking
                    .Include(a => a.ExamQuestions)
                    .Include(a => a.Lesson)
                    .Select(a => new ExamViewModel
                    {
                        ID = a.ID,
                        Title = a.Title,
                        Questions = a.ExamQuestions,
                        IsActive = a.IsActive,
                        RandomQuestionOptionsOrder = a.RandomQuestionOptionsOrder,
                        RegUserID = a.RegUserID,
                        RegDate = a.RegDate,
                        EndDate = a.EndDate.ToString(),
                        Description = a.Description,
                        StartDate = a.StartDate.ToString(),
                        LessonTitle = a.Lesson.Title
                    })
                    .SingleOrDefaultAsync(a => a.ID.Equals(examId));

                return item;
            }
            catch
            {
                return null;
            }
        }

        public override async Task<PagedList<DomainClass.EDU.Exam.ExamAnswerSheet>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.Exam.ExamAnswerSheet, bool>> filter = null, Func<IQueryable<DomainClass.EDU.Exam.ExamAnswerSheet>, IOrderedQueryable<DomainClass.EDU.Exam.ExamAnswerSheet>> orderBy = null)
        {
            return await _uow.ExamAnswerSheet.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
