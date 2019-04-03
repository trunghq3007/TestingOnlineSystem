using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TestingSystem.BaseController;
using TestingSystem.DataTranferObject;
using TestingSystem.DataTranferObject.Question;
using TestingSystem.Models;
using TestingSystem.Sevice;

namespace TestingSystem.Areas.Admin.Controllers
{
    public class TestLoginController : AdminController
    {
        public ICandidateService candidateService;
        public IExamPaperQuestionService examPaperQuestionService;
        public IExamPaperService examPaperService;
        public ITestService testService;
        public ITestResultService testResultService;
        public IQuestionService questionService;
        public IAnswerService answerService;
        public TestLoginController(ICandidateService candidateService, ITestResultService testResultService, ITestService testService, IUserService userService, IExamPaperQuestionService examPaperQuestionService, IQuestionService questionService, IAnswerService answerService, IExamPaperService examPaperService) : base(userService)
        {
            this.examPaperQuestionService = examPaperQuestionService;
            this.questionService = questionService;
            this.answerService = answerService;
            this.examPaperService = examPaperService;
            this.testService = testService;
            this.testResultService = testResultService;
            this.candidateService = candidateService;
        }

        // hien thi thong tin bai thi
        public ActionResult ShowInfoTest(int idExamPaper, int idExam, int idTest)
        {
            Test item = new Test();
            item = testService.GetTestByID(idTest);
            ViewBag.IdExam = idExam;
            return View(item);
        }

        // hien thi bai thi
        // hieu la lay id bai test theo idExamPaper va idTest
        [HttpGet]
        public ActionResult ShowExamPaperById(int idExamPaper, int idExam, int idTest)
        {
            
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
                    if(answerService.GetAnswerCorrect(item2.AnswerID) != null)
                    {
                        checkcount++;
                    }
                }
                if(checkcount > 1)
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
            ViewBag.IdExam = idExam;
            
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
        [HttpPost]
        public JsonResult _RepostTest(int exampaperid,int examid, int passscore, int idtest, IEnumerable<ResultTest> fruits = null)
        {

            /// danh sach cau hoi trong de thi
            var listExamPaperQuesions = questionService.GetQuestionsByExamPaperId(exampaperid);
            List<Answer> listAnswerByQuestionId = new List<Answer>();
            List<QuestionCheckMulti> listQuestionCheckMulti = new List<QuestionCheckMulti>();

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






            int idUser = int.Parse(Session["Name"].ToString());
            // fruits : chua danh sach tat ca id question va id answer da check
            var list = fruits;

            Models.ExamPaper examPaper = new Models.ExamPaper();
            examPaper = examPaperService.GetExamPaperById(exampaperid);

            // check turn of test result trong hom thi
            int turn = testResultService.ReturnTurn(idtest, DateTime.Now);

            // get all question include not check
            List<QuestionDto> listQuestion = new List<QuestionDto>();
            listQuestion = questionService.GetQuestionsByExamPaperId(exampaperid).ToList();

            if (fruits.Count() > 0 && fruits != null)
            {
                int countAnswer = fruits.Count();
                int numberOfCorrectAnswer = 0;
                int i = 0;
                int idalive = 0;
                foreach (var item in list)
                {
                    // id: answer id, name: question id
                    // lay answer dung trong fruits
                    if (idalive != item.name)
                    {
                        if (listQuestionCheckMulti[i].CheckMulti == false)
                        {
                            var obj = answerService.GetAnswerCorrect(item.id);
                            if (obj != null)
                            {
                                numberOfCorrectAnswer++;
                            }
                        }
                        else
                        {
                            List<int> listIdAnswerCorrectByIdQuestion = new List<int>();
                            // lay tat ca id answer dung theo id question
                            listIdAnswerCorrectByIdQuestion = answerService.GetListIdAnswerCorrectByIdQuestion(item.name);
                            List<int> listIdAnswerCheckById = new List<int>();
                            foreach (var obj in list)
                            {
                                if (obj.name == item.name)
                                {
                                    listIdAnswerCheckById.Add(obj.id);
                                }
                            }
                            if (UnorderedEqual(listIdAnswerCorrectByIdQuestion, listIdAnswerCheckById) == true)
                            {
                                numberOfCorrectAnswer++;
                            }
                            idalive = item.name;
                        }
                    }
                }

                // lay exampaper theo id
                // listquestion: so cau hoi trong de

                double percent = numberOfCorrectAnswer / countAnswer;


                // lay exampaper theo id
                // listquestion: so cau hoi trong de
                
                var testName = testService.GetTestByID(idtest).TestName;

                foreach (var item in listQuestion)
                {
                    TestResult testResult = new TestResult();
                    testResult.TestID = idtest;
                    testResult.CandidateID = idUser;
                    testResult.TestName = testName;
                    testResult.Description = "description note";
                    testResult.CreatedDate = DateTime.Now;
                    testResult.Score = numberOfCorrectAnswer;
                    testResult.Turns = turn + 1;

                    // list: so cau hoi da check
                    bool checkAvailable = false;
                    foreach (var item2 in list)
                    {
                        // item2 co 2 attribute: id(answerid) va name(questionid)
                        if (item2.name == item.QuestionID)
                        {
                            testResult.QuestionID = item2.name;
                            testResult.AnswerID = item2.id;
                            checkAvailable = true;
                            testResultService.AddTestResult(testResult);
                        }
                    }
                    if (checkAvailable == false)
                    {
                        testResult.QuestionID = item.QuestionID;
                        testResult.AnswerID = -1;
                        testResultService.AddTestResult(testResult);
                    }
                }
                return Json(listQuestion.Count());
            }
            else
            {
                foreach (var item in listQuestion)
                {
                    TestResult testResult = new TestResult();
                    testResult.TestID = idtest;
                    testResult.CandidateID = idUser;
                    testResult.TestName = examPaper.Title;
                    testResult.Description = "description note";
                    testResult.CreatedDate = DateTime.Now;
                    testResult.Score = 0;
                    testResult.Turns = turn + 1;
                    testResult.QuestionID = item.QuestionID;
                    testResult.AnswerID = -1;
                    testResultService.AddTestResult(testResult);
                }
                return Json(0);
            }
        }

        public ActionResult _ShowResult(int countQ, int passscore, string title)
        {
            int idUser = int.Parse(Session["Name"].ToString());
            List<Models.TestResult> listQ= new List<Models.TestResult>();
            listQ = testResultService.GetQuestionByCount(countQ).ToList();
            ViewBag.DedicateName = userService.GetUserById(idUser).Name;
            ViewBag.DedicateEmail = userService.GetUserById(idUser).Email;
            ViewBag.TestTitle = title;
            int score = 0;
            bool checkPass = false; 
            foreach(var item in listQ)
            {
                score = item.Score;
                break;
            }
            double percent = (score / countQ) * 100;
            percent = Math.Round(percent);
            if (percent >= passscore)
            {
                checkPass = true;
            }
            ViewBag.Score = score;
            ViewBag.CheckPass = checkPass;
            ViewBag.CountQ = countQ;
            return View();
        }
    }
}