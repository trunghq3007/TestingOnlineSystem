using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingSystem.Sevice;

namespace TestingSystem.Controllers
{
    public class ExamPaperController : Controller
    {
	    public IExamPaperService examPaperService;
		// GET: ExamPaper
		public ExamPaperController(IExamPaperService examPaperService)
		{
			this.examPaperService = examPaperService;
		}
        /// <summary>
        /// PartialExamPaper
        /// </summary>
        /// <returns></returns>
        public ActionResult PartialExamPaper()
        {
	        var model = examPaperService.ListExamPapersTop();
            return PartialView(model);
        }
    }
}