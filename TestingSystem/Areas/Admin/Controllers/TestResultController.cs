using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingSystem.BaseController;
using TestingSystem.Sevice;

namespace TestingSystem.Areas.Admin.Controllers
{
    public class TestResultController : AdminController
    {
        // GET: Admin/TestResult
         private readonly ITestResultService testResultService;
        public TestResultController(ITestResultService testResult, IUserService userService):base(userService)
        {
            this.testResultService = testResult;
        }
        public ActionResult Index()
        {
            return View();
        }
     
    }
}