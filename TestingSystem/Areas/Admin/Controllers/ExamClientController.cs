using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingSystem.BaseController;
using TestingSystem.Sevice;

namespace TestingSystem.Areas.Admin.Controllers
{
    public class ExamClientController : AdminController
    {

        private IExamPaperService examPaperService;
        private IExamService examService;
        private ITestResultService testResultService;
        private TestService TestService;

        public ExamClientController(IUserService userService, TestService TestService, IExamService examService, IExamPaperService examPaperService, ITestResultService testResultService) : base(userService)
        {
            this.examPaperService = examPaperService;
            this.examService = examService;
            this.testResultService = testResultService;
            this.TestService = TestService;
        }
        //GET: Admin/ExamClient
        public ActionResult ListExamClient(int idExam = 0)
        {
            int idUser = int.Parse(Session["Name"].ToString());
            var listExam = examService.GetExamFollow().ToList();
            var listTest = TestService.GetAllTests();
            if (idExam == 0)
            {
                idExam = listExam[0].ExamID;
            }
            if (idExam != 0)
            {
                ViewBag.ListExam = listExam;
                ViewBag.ListExamPaperInExam = examService.GetTestByExamID(idExam, idUser);
            }
            else
            {
                ViewBag.ListExam = listExam;
                ViewBag.ListExamPaperInExam = examService.GetTestByExamID(idExam, idUser);
            }
            ViewBag.IdExam = idExam;
            return View(listExam);
        }
        [ChildActionOnly]
        public ActionResult _CountUser(int TestId)
        {
            var count = testResultService.CountUsed(TestId);
            ViewBag.Count = count;
            return View();
        }
    }
   
}