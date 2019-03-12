namespace TestingSystem.Areas.Admin.Controllers.ExamPaperQuestion
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using TestingSystem.BaseController;
    using TestingSystem.DataTranferObject.Question;
    using TestingSystem.Sevice;

    /// <summary>
    /// Defines the <see cref="ExamPaperQuestionController" />
    /// </summary>
    public class ExamPaperQuestionController : AdminController
    {
        /// <summary>
        /// Defines the examPaperQuestionService
        /// </summary>
        private readonly IExamPaperQuestionService examPaperQuestionService;

        /// <summary>
        /// Defines the questionService
        /// </summary>
        private readonly IQuestionService questionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExamPaperQuestionController"/> class.
        /// </summary>
        /// <param name="examPaperQuestionService">The examPaperQuestionService<see cref="IExamPaperQuestionService"/></param>
        /// <param name="questionService">The questionService<see cref="IQuestionService"/></param>
        public ExamPaperQuestionController(IUserService a,IExamPaperQuestionService examPaperQuestionService, IQuestionService questionService):base(a)
        {
            this.examPaperQuestionService = examPaperQuestionService;
            this.questionService = questionService;
        }

        /// <summary>
        /// The GetExamPaperQuestionsByExamPaperId
        /// </summary>
        /// <param name="examPaperId">The examPaperId<see cref="int"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult GetExamPaperQuestionsByExamPaperId(int examPaperId)
        {
            var examPaperQuestions = new List<TestingSystem.Models.ExamPaperQuesion>();
            examPaperQuestions = examPaperQuestionService.GetExamPaperQuestionsByExamPaperId(examPaperId).ToList();
            return Json(new { data = examPaperQuestions }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="ids">The ids<see cref="List{int}"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Delete(List<int> ids)
        {
            try
            {
                if (ids.Count > 0)
                {
                    int i = 0;
                    foreach (var id in ids)
                    {
                        if (examPaperQuestionService.DeleteExamPaperQuestion(id) > 0)
                        {
                            i++;
                            continue;
                        }
                        else
                        {
                            //!!!!!!!!!!! break nhưng mà những cái record trc đó vẫn đã bị xóa
                            break;
                        }

                    }
                    if (i > 0)
                    {
                        Success = "Delete exam paper successfully!";
                        return Json(new { status = true }, JsonRequestBehavior.AllowGet);
                    }
                }
                Failure = "Something went wrong, please try again!";
                return Json(new { status = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                Failure = exception.Message;
                return Json(new { status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// The Insert
        /// </summary>
        /// <param name="examPaperId">The examPaperId<see cref="int"/></param>
        /// <param name="questionId">The questionId<see cref="int"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        public ActionResult Insert(int examPaperId, int questionId)
        {
            try
            {
                if (examPaperQuestionService.InsertExamPaperQuestion(examPaperId, questionId) > 0)
                {
                    Success = "Add question into exam paper successfully!";
                    return Json(new { status = true }, JsonRequestBehavior.AllowGet);
                }

                Failure = "Something went wrong, please try again!";
                return Json(new { status = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                Failure = exception.Message;
                return Json(new { status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// The RandomQuestionsByCategoryIdAndExamPaperIdAndNumber
        /// </summary>
        /// <param name="categoryId">The categoryId<see cref="int"/></param>
        /// <param name="examPaperId">The examPaperId<see cref="int"/></param>
        /// <param name="number">The number<see cref="int"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult RandomQuestionsByCategoryIdAndExamPaperIdAndNumber(int categoryId, int examPaperId, int number)
        {
            try
            {
                List<QuestionDto> questionDtos = new List<QuestionDto>();
                questionDtos = questionService.RandomQuestionsByCategoryIdAndExamPaperIdAndNumber(categoryId, examPaperId, number).ToList();
                foreach (var item in questionDtos)
                {
                    examPaperQuestionService.InsertExamPaperQuestion(examPaperId, item.QuestionID);
                }
                Success = "Add question into exam paper successfully!";
                return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                Failure = exception.Message;
                return Json(new { status = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
