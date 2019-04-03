using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingSystem.BaseController;
using TestingSystem.DataTranferObject;
using TestingSystem.Models;
using TestingSystem.Sevice;

namespace TestingSystem.Areas.Admin.Controllers
{
    public class ReviewTestResultController : AdminController
    {
        public ICandidateService candidateService;
        public IExamPaperQuestionService examPaperQuestionService;
        public IExamPaperService examPaperService;
        public ITestService testService;
        public ITestResultService testResultService;
        public IQuestionService questionService;
        public IAnswerService answerService;
        public ReviewTestResultController(ICandidateService candidateService, ITestResultService testResultService, ITestService testService, IUserService userService, IExamPaperQuestionService examPaperQuestionService, IQuestionService questionService, IAnswerService answerService, IExamPaperService examPaperService) : base(userService)
        {
            this.examPaperQuestionService = examPaperQuestionService;
            this.questionService = questionService;
            this.answerService = answerService;
            this.examPaperService = examPaperService;
            this.testService = testService;
            this.testResultService = testResultService;
            this.candidateService = candidateService;
        }
        public ActionResult ShowAllTestByDedicateId()
        {
            int idUser = int.Parse(Session["Name"].ToString());
            List<ReviewTestResult> listReviewTestResult = new List<ReviewTestResult>();
            ViewBag.ListReviewTestResult = testResultService.ListAllTestByDedicateId(idUser).ToList();
            ViewBag.NameDedicate = userService.GetUserById(idUser).Name;
            return View();
        }
        public ActionResult ShowTestResultById(int idTest, DateTime dateTest, int turnTest)
        {
            int idExamPaper = testService.GetExamPaperIdByTestId(idTest);
            /// danh sach cau hoi trong de thi
            var listExamPaperQuesions = questionService.GetQuestionsByExamPaperId(idExamPaper);



            // so luong cau hoi
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

            List<QuestionCheckMulti> listQuestionCheckMulti = new List<QuestionCheckMulti>();
            List<Answer> listAnswerByQuestionId = new List<Answer>();

            // get multi choice in all question
            foreach (var item in listExamPaperQuesions)
            {
                var checkcount = 0;
                bool multichoice = false;
                listAnswerByQuestionId = answerService.GetAnswersByQuestionID(item.QuestionID);
                foreach (var item2 in listAnswerByQuestionId)
                {
                    if (answerService.GetAnswerCorrect(item2.AnswerID) != null)
                    {
                        checkcount++;
                    }
                }
                if (checkcount > 1)
                {
                    multichoice = true;
                }
                listQuestionCheckMulti.Add(new QuestionCheckMulti() { QuestionID = item.QuestionID, CheckMulti = multichoice });
                multichoice = false;
                checkcount = 0;
            }
            ViewBag.ListQuestionCheckMulti = listQuestionCheckMulti;
            ViewBag.IdExamPaper = idExamPaper;
            TempData["idExamPaper"] = idExamPaper;
            ViewBag.TitleTest = examPaperService.GetExamPaperById(idExamPaper).Title;
            // id bai thi
            ViewBag.IdTest = idTest;
            // lay bai thi theo id
            Test test = new Test();
            test = testService.GetTestByID(idTest);
            //pass score bai thi
            ViewBag.PassScore = test.PassingScore;

            List<ResultCheckId> listResultCheckIds = new List<ResultCheckId>();
            // lay id cau tra loi va id cau hoi da check
            listResultCheckIds = testResultService.ListAllQuestionIdAndAnswerIdByTestIdChecked(idTest, turnTest).ToList();
            ViewBag.ListResultCheckIds = listResultCheckIds;
            List<ResultCheckIdTrue> listAnswerQuestion = new List<ResultCheckIdTrue>();
            listAnswerQuestion = answerService.GetAllQuestionIdandAnswerIdByExampaperId(idExamPaper).ToList();
            ViewBag.ListAnswerQuestion = listAnswerQuestion;
            List<QuestionCheck> listIdQuestionToCheckTrue = new List<QuestionCheck>();



            int idalive = 0;
            int i = 0;
            foreach (var item in listResultCheckIds)
            {
                // id: answer id, name: question id
                // lay answer dung trong fruits
                if (idalive != item.QuestionId)
                {
                    if (listQuestionCheckMulti[i].CheckMulti == false)
                    {
                        var obj = answerService.GetAnswerCorrect(item.AnswerId);
                        if (obj != null)
                        {
                            listIdQuestionToCheckTrue.Add(new QuestionCheck() { QuestionID = item.QuestionId, IsTrue = true });
                        }
                        else
                        {
                            listIdQuestionToCheckTrue.Add(new QuestionCheck() { QuestionID = item.QuestionId, IsTrue = false });
                        }
                    }
                    else
                    {
                        List<int> listIdAnswerCorrectByIdQuestion = new List<int>();
                        // lay tat ca id answer dung theo id question
                        listIdAnswerCorrectByIdQuestion = answerService.GetListIdAnswerCorrectByIdQuestion(item.QuestionId);
                        List<int> listIdAnswerCheckById = new List<int>();
                        foreach (var obj in listResultCheckIds)
                        {
                            if (obj.QuestionId == item.QuestionId)
                            {
                                listIdAnswerCheckById.Add(obj.AnswerId);
                            }
                        }
                        if (UnorderedEqual(listIdAnswerCorrectByIdQuestion, listIdAnswerCheckById) == true)
                        {
                            listIdQuestionToCheckTrue.Add(new QuestionCheck() { QuestionID = item.QuestionId, IsTrue = true });
                        }
                        else
                        {
                            listIdQuestionToCheckTrue.Add(new QuestionCheck() { QuestionID = item.QuestionId, IsTrue = false });
                        }
                        idalive = item.QuestionId;
                    }
                }
            }
            ViewBag.ListIdQuestionToCheckTrue = listIdQuestionToCheckTrue;
            ViewBag.CountListIdQuestionToCheckTrue = listIdQuestionToCheckTrue.Count();
            return View();
        }
        static bool UnorderedEqual<T>(ICollection<T> a, ICollection<T> b)
        {
            // 1
            // Require that the counts are equal
            if (a.Count != b.Count)
            {
                return false;
            }
            // 2
            // Initialize new Dictionary of the type
            Dictionary<T, int> d = new Dictionary<T, int>();
            // 3
            // Add each key's frequency from collection A to the Dictionary
            foreach (T item in a)
            {
                int c;
                if (d.TryGetValue(item, out c))
                {
                    d[item] = c + 1;
                }
                else
                {
                    d.Add(item, 1);
                }
            }
            // 4
            // Add each key's frequency from collection B to the Dictionary
            // Return early if we detect a mismatch
            foreach (T item in b)
            {
                int c;
                if (d.TryGetValue(item, out c))
                {
                    if (c == 0)
                    {
                        return false;
                    }
                    else
                    {
                        d[item] = c - 1;
                    }
                }
                else
                {
                    // Not in dictionary
                    return false;
                }
            }
            // 5
            // Verify that all frequencies are zero
            foreach (int v in d.Values)
            {
                if (v != 0)
                {
                    return false;
                }
            }
            // 6
            // We know the collections are equal
            return true;
        }
    }
}