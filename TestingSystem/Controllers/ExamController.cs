using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingSystem.Data.Repositories;

namespace TestingSystem.Controllers
{
	public class ExamController : Controller
	{
		private readonly IExamPaperRepository examPaperRepository;
		public ExamController(IExamPaperRepository ExamPaperRepository)
		{
			this.examPaperRepository = ExamPaperRepository;
		}
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        // GET: Exam
        public ActionResult Index()
		{
			var model = examPaperRepository.ListExamPapersTop();
			return PartialView(model);
		}

	}
}