using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TestingSystem.BaseController;
using TestingSystem.DataTranferObject;
using TestingSystem.Models;
using TestingSystem.Sevice;

namespace TestingSystem.Controllers
{
    public class TestNotLoginController : ClientController
    {
        public IExamPaperQuestionService examPaperQuestionService;
        public IExamPaperService examPaperService;
        public IQuestionService questionService;
        public IAnswerService answerService;
        public TestNotLoginController(IUserService userService, IExamPaperQuestionService examPaperQuestionService, IQuestionService questionService, IAnswerService answerService, IExamPaperService examPaperService) : base(userService)
        {
            this.examPaperQuestionService = examPaperQuestionService;
            this.questionService = questionService;
            this.answerService = answerService;
            this.examPaperService = examPaperService;
        }
        /// <summary>
        /// ShowExamPaperById
        /// </summary>
        /// <param name="idExamPaper"></param>
        /// <returns></returns>
        public ActionResult ShowExamPaperById(int idExamPaper)
        {
            var listExamPaperQuesions = questionService.GetQuestionsByExamPaperId(idExamPaper);
            var countQuestion = listExamPaperQuesions.Count();
            ViewBag.CountQuestion = countQuestion;
            ViewBag.Time = examPaperService.GetExamPaperById(idExamPaper).Time;
            List<Answer> listAnswer = new List<Answer>();
            List<Answer> listAnswerInExamPaper = new List<Answer>();
            foreach (var item in listExamPaperQuesions)
            {
                listAnswerInExamPaper.AddRange(answerService.GetAnswersByQuestionID(item.QuestionID));
            }
            List<Answer> listA = new List<Answer>();
            ViewBag.ListQuestion = listExamPaperQuesions;
            ViewBag.ListAnswer = listAnswerInExamPaper;
            ViewBag.IdExamPaper = idExamPaper;
            TempData["idExamPaper"] = idExamPaper;
            return View();
        }
        /// <summary>
        /// RepostTest
        /// </summary>
        /// <param name="fruits"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RepostTest(IEnumerable<ResultTest> fruits)
        {
            var list = fruits;
            List<Answer> listAnswerCorrect = new List<Answer>();
            foreach (var item in list)
            {
                var obj = answerService.GetAnswerCorrect(item.id);
                if (obj != null)
                {
                    listAnswerCorrect.Add(obj);
                }
            }
            int numberOfCorrectAnswer = 0;
            foreach (var item in listAnswerCorrect)
            {
                foreach (var item2 in list)
                {
                    if (item.AnswerID == item2.id)
                    {
                        numberOfCorrectAnswer++;
                        continue;
                    }
                }
            }
            return Json(numberOfCorrectAnswer);
        }
    }
}