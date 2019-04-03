using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using TestingSystem.DataTranferObject;
using TestingSystem.DataTranferObject.Question;
using TestingSystem.Models;
using TestingSystem.Sevice;

namespace TestingSystem.Controllers
{
	public class AuditionsController : Controller
	{
		//public string Success { set { TempData["Success"] = ViewData["Success"] = value; } }
		//public string Failure { set { TempData["Failure"] = ViewData["Failure"] = value; } }

		private readonly IExamPaperService examPaperService;
		private readonly IQuestionService questionService;
		private readonly IAnswerService answerService;
		private readonly IExamService examService;
		private readonly IExamPaperQuestionService examPaperQuestionService;
		private readonly ITestService testService;
		private readonly IQuestionCategorySevice questionCategorySevice;

		public AuditionsController(
			IExamPaperService examPaperService,
			IQuestionService questionService,
			IAnswerService answerService,
			IExamService examService,
			IExamPaperQuestionService examPaperQuestionService,
			IQuestionCategorySevice questionCategorySevice,
			ITestService testService)
		{
			this.examPaperService = examPaperService;
			this.questionService = questionService;
			this.answerService = answerService;
			this.examService = examService;
			this.examPaperQuestionService = examPaperQuestionService;
			this.questionCategorySevice = questionCategorySevice;
			this.testService = testService;
		}
        /// <summary>
        /// _ShowInfoTest
        /// </summary>
        /// <param name="idExamPaper"></param>
        /// <param name="idExam"></param>
        /// <param name="idTest"></param>
        /// <returns></returns>
		public ActionResult _ShowInfoTest(int idExamPaper, int idExam, int idTest)
		{
			Test item = new Test();
			item = testService.GetTestByID(idTest);
			ViewBag.IdExam = idExam;
			return View(item);
		}
		public ActionResult AuditionsTest()
		{
			return View();
		}

		public ActionResult MyAuditionsTest()
		{

			return View();
		}

        /// <summary>
        /// MyAuditionsTest
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
		[HttpPost]
		public ActionResult MyAuditionsTest(string code)
		{

			try
			{
				var model = testService.GetAllTetByExamCode(code);
				if (model != null)
				{
					ViewBag.IdExam = examService.GetExamByCode(code).ExamID;
					//Success = "Success";
					return View(model);
				}
			}
			catch (Exception e)
			{
				///*Fa*/ilure = "Code not exist!";
				return RedirectToAction("AuditionsTest");

			}
			ViewBag.IdExam = examService.GetExamByCode(code).ExamID;
			return View();

		}

        // hieu la lay id bai test theo idExamPaper va idTest
        /// <summary>
        /// ShowExamPaperById
        /// </summary>
        /// <param name="idExamPaper"></param>
        /// <param name="idExam"></param>
        /// <param name="idTest"></param>
        /// <returns></returns>
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
			ViewBag.IdExam = idExam;
			TempData["countTrue"] = 0;
			return View();
		}
        /// <summary>
        /// UnorderedEqual
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
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
        /// <summary>
        /// _RepostTest
        /// </summary>
        /// <param name="fruits"></param>
        /// <param name="exampaperid"></param>
        /// <param name="examid"></param>
        /// <param name="passscore"></param>
        /// <param name="idtest"></param>
        /// <returns></returns>
		[HttpPost]
		public JsonResult _RepostTest(IEnumerable<ResultTest> fruits, int exampaperid, int examid, int passscore, int idtest)
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
			// fruits : chua danh sach tat ca id question va id answer da check
			var list = fruits;
			int countAnswer = fruits.Count();
			int numberOfCorrectAnswer = 0;
			int i = 0;
			bool checkcontinue = true;
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
			// get all question include not check
			List<QuestionDto> listQuestion = new List<QuestionDto>();
			listQuestion = questionService.GetQuestionsByExamPaperId(exampaperid).ToList();


			double percent = numberOfCorrectAnswer / countAnswer;

			Models.ExamPaper examPaper = new Models.ExamPaper();
			examPaper = examPaperService.GetExamPaperById(exampaperid);


			var resultJson = new ResultJson
			{
				countQ = listQuestion.Count(),
				countTrue = numberOfCorrectAnswer
			};
			TempData["countTrue"] = numberOfCorrectAnswer;
			TempData.Keep();
			return Json(resultJson);
		}

		public ActionResult _ShowResult(int countQ, int passscore, string title)
		{
			List<Models.TestResult> listQ = new List<Models.TestResult>();
			ViewBag.TestTitle = title;
			int score = 0;
			bool checkPass = false;
			double percent = ((int)TempData["countTrue"] / countQ) * 100;
			percent = Math.Round(percent);
			if (percent >= passscore)
			{
				checkPass = true;
			}
			ViewBag.Score = TempData["countTrue"];
			ViewBag.CheckPass = checkPass;
			ViewBag.CountQ = countQ;
			return View();
		}
	}
}