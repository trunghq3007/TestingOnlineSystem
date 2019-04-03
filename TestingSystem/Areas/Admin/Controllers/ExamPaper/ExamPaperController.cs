namespace TestingSystem.Areas.Admin.Controllers.ExamPaper
{
    using Newtonsoft.Json.Linq;
    using OfficeOpenXml;
    using Rotativa.MVC;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using TestingSystem.BaseController;
    using TestingSystem.DataTranferObject.Question;
    using TestingSystem.Models;
    using TestingSystem.Sevice;
    using Excel = Microsoft.Office.Interop.Excel;

    /// <summary>
    /// Defines the <see cref="ExamPaperController" />
    /// </summary>
    public class ExamPaperController : AdminController
    {
        /// <summary>
        /// Defines the examPaperService
        /// </summary>
        private readonly IExamPaperService examPaperService;

        /// <summary>
        /// Defines the questionService
        /// </summary>
        private readonly IQuestionService questionService;

        /// <summary>
        /// Defines the answerService
        /// </summary>
        private readonly IAnswerService answerService;

        /// <summary>
        /// Defines the examPaperQuestionService
        /// </summary>
        private readonly IExamPaperQuestionService examPaperQuestionService;

        /// <summary>
        /// Defines the questionCategorySevice
        /// </summary>
        private readonly IQuestionCategorySevice questionCategorySevice;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExamPaperController"/> class.
        /// </summary>
        /// <param name="examPaperService">The examPaperService<see cref="IExamPaperService"/></param>
        /// <param name="questionService">The questionService<see cref="IQuestionService"/></param>
        /// <param name="answerService">The answerService<see cref="IAnswerService"/></param>
        /// <param name="examPaperQuestionService">The examPaperQuestionService<see cref="IExamPaperQuestionService"/></param>
        /// <param name="questionCategorySevice">The questionCategorySevice<see cref="IQuestionCategorySevice"/></param>

        public ExamPaperController(IUserService a,IExamPaperService examPaperService, IQuestionService questionService, IAnswerService answerService, IExamPaperQuestionService examPaperQuestionService, IQuestionCategorySevice questionCategorySevice) : base(a)
        {
            this.examPaperService = examPaperService;
            this.questionService = questionService;
            this.answerService = answerService;
            this.examPaperQuestionService = examPaperQuestionService;
            this.questionCategorySevice = questionCategorySevice;
        }

        public ActionResult ExamPapers()
        {
            var examPapers = new List<ExamPaper>();
            examPapers = examPaperService.GetAll().ToList();
            ViewBag.CountExamPaper = examPapers.Count();
            foreach (var item in examPapers)
            {
                item.NumberOfQuestion = examPaperService.GetNumberOfQuestionByExamPaperId(item.ExamPaperID);
            }
            return View(examPapers);
        }

        [HttpGet]
        [ActionName("ExamPaper")]
        public ActionResult ExamPaper(int? examPaperId)
        {
            var model = new Models.ExamPaper();
            var questions = new List<QuestionDto>();

            if (examPaperId == null || examPaperId == 0)
            {
                ViewBag.IsUpdate = false;
                ViewBag.Questions = questions;
                return View(model);
            }
            model = examPaperService.GetExamPaperById(examPaperId.Value);
            if (model != null)
            {
                questions = questionService.GetQuestionsByExamPaperId(examPaperId.Value).ToList();
                ViewBag.Questions = questions;

            }
            ViewBag.Status = model.Status;
            ViewBag.IsUpdate = true;
            return View(model);
        }

        /// <summary>
        /// The ExamPapers
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        //public ActionResult ExamPapers()
        //{
        //    return View();
        //}

        /// <summary>
        /// The GetExamPapers
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        [ActionName("GetExamPapers")]
        public ActionResult GetExamPapers()
        {
            var examPapers = new List<TestingSystem.Models.ExamPaper>();
            examPapers = examPaperService.GetAll().ToList();
            foreach (var item in examPapers)
            {
                item.NumberOfQuestion = examPaperService.GetNumberOfQuestionByExamPaperId(item.ExamPaperID);
            }
            return Json(new { data = examPapers }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// The ExamPaper
        /// </summary>
        /// <param name="examPaperId">The examPaperId<see cref="int?"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        //[HttpGet]
        //[ActionName("ExamPaper")]
        //public ActionResult ExamPaper(int? examPaperId)
        //{
        //    var model = new Models.ExamPaper();

        //    if (examPaperId == null || examPaperId == 0)
        //    {
        //        ViewBag.IsUpdate = false;
        //        return View(model);
        //    }
        //    model = examPaperService.GetExamPaperById(examPaperId.Value);
        //    if (model != null)
        //    {

        //    }
        //    ViewBag.Status = model.Status;
        //    ViewBag.IsUpdate = true;
        //    return View(model);
        //}

        /// <summary>
        /// The ExamPaper
        /// </summary>
        /// <param name="examPaper">The examPaper<see cref="Models.ExamPaper"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("ExamPaper")]
        public ActionResult ExamPaper(Models.ExamPaper examPaper)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (examPaper.ExamPaperID == 0)
                    {
                        examPaper.NumberOfQuestion = 0;
                        examPaper.CreatedDate = DateTime.Now;
                        examPaper.CreatedBy = int.Parse(Session["Name"].ToString());
                        examPaper.ModifiedBy = int.Parse(Session["Name"].ToString());
                        if (examPaperService.Create(examPaper) > 0)
                        {
                            Success = "Insert exam paper successfully!";
                            return RedirectToAction("ExamPapers");
                        }
                    }
                    else
                    {
                        examPaper.ModifiedDate = DateTime.Now;
                        examPaper.ModifiedBy = int.Parse(Session["Name"].ToString());
						//lay so cau hoi
						examPaper.NumberOfQuestion=examPaperService.GetNumberOfQuestionByExamPaperId(examPaper.ExamPaperID);
                        if (examPaperService.Edit(examPaper) > 0)
                        {
                            Success = "Update exam paper successfully!";
                            return RedirectToAction("ExamPapers");
                        }
                    }
                }
                Failure = "Something went wrong, please try again!";
                return new JsonResult { Data = new { status = false } };
            }
            catch (Exception exception)
            {
                Failure = exception.Message;
                return View(examPaper);
            }
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
                        if (examPaperService.Delete(id) > 0)
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
        /// The ImportExamPaper
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult ImportExamPaper()
        {
            return View();
        }

        /// <summary>
        /// The ImportExamPaper
        /// </summary>
        /// <param name="excelfile">The excelfile<see cref="HttpPostedFileBase"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        public ActionResult ImportExamPaper(HttpPostedFileBase excelfile)
        {
            if (excelfile == null)
            {
                Failure = "Please choose excel file to import exam paper";
                return RedirectToAction("ImportExamPaper");

            }
            else
            {
                if (excelfile.FileName.EndsWith("xls") || excelfile.FileName.EndsWith("xlsx"))
                {
                    try
                    {
                        string path = Path.Combine(Server.MapPath("~/FileExcel/"), Guid.NewGuid().ToString() + Path.GetExtension(excelfile.FileName));
                        excelfile.SaveAs(path);
                        Excel.Application application = new Excel.Application
                        {
                            Visible = true
                        };
                        Excel.Workbook workbook = application.Workbooks.Open(path);
                        Excel.Worksheet worksheet = workbook.Sheets[@"ExamPaper"];
                        Excel.Range range = worksheet.UsedRange;

                        Models.ExamPaper examPaper = new Models.ExamPaper();
                        examPaper.Title = ((Excel.Range)range.Cells[3, 2]).Text;
                        examPaper.Time = int.Parse(((Excel.Range)range.Cells[4, 2]).Text);
                        if (((Excel.Range)range.Cells[5, 2]).Text == "Public")
                        {
                            examPaper.Status = true;
                        }
                        else if (((Excel.Range)range.Cells[5, 2]).Text == "Draff")
                        {
                            examPaper.Status = false;
                        }
                        else
                        {
                            Failure = "Exam paper status must be select from dropdown list";
                            return RedirectToAction("ImportExamPaper");
                        }
                        examPaper.IsActive = Boolean.Parse(((Excel.Range)range.Cells[6, 2]).Text);
                        examPaper.CreatedBy = int.Parse(Session["Name"].ToString());
                        examPaper.CreatedDate = DateTime.Now;
                        examPaper.ModifiedBy = int.Parse(Session["Name"].ToString());
                        examPaper.ModifiedDate = DateTime.Now;
                        int examPaperId = examPaperService.Create(examPaper);
                        var listQuestionCategory = questionCategorySevice.GetAll();
                        for (int row = 11; row <= range.Rows.Count; row++)
                        {
                            int level = 0;
                            if (((Excel.Range)range.Cells[row, 2]).Text == "Hard")
                            {
                                level = 3;
                            }
                            else if (((Excel.Range)range.Cells[row, 2]).Text == "Normal")
                            {
                                level = 2;
                            }
                            else if (((Excel.Range)range.Cells[row, 2]).Text == "Easy")
                            {
                                level = 1;
                            }
                            else
                            {
                                Failure = "Question level must be select from dropdown list";
                                return RedirectToAction("ImportExamPaper");
                            }
                            int categoryId = 0;
                            int k = 0;
                            foreach (var item in listQuestionCategory)
                            {
                                if (((Excel.Range)range.Cells[row, 3]).Text == item.Name)
                                {
                                    categoryId = item.CategoryID;
                                    k++;
                                }
                            }
                            if (k == 0)
                            {
                                Failure = "Question category must be select from dropdown list";
                                return RedirectToAction("ImportExamPaper");
                            }
                            Models.Question question = new Models.Question
                            {
                                Content = ((Excel.Range)range.Cells[row, 1]).Text,
                                CategoryID = categoryId,
                                Level = level,
                                IsActive = true,
                                CreatedBy = int.Parse(Session["Name"].ToString()),
                                CreatedDate = DateTime.Now,
                                ModifiedBy = int.Parse(Session["Name"].ToString()),
                                ModifiedDate = DateTime.Now
                            };
                            int questionId = questionService.AddQuestion(question);

                            Answer answer = new Answer();
                            int j = 5;
                            for (int i = 4; i <= 13; i += 2)
                            {
                                string content = ((Excel.Range)range.Cells[row, i]).Text;
                                if (content != "")
                                {
                                    answer.AnswerContent = content;
                                    answer.IsCorrect = Boolean.Parse(((Excel.Range)range.Cells[row, j]).Text);
                                    answer.QuestionID = questionId;
                                    answerService.AddAnswer(answer);
                                }
                                else
                                {
                                    continue;
                                }
                                j += 2;
                            }
                            examPaperQuestionService.InsertExamPaperQuestion(examPaperId, questionId);
                        }
                    }
                    catch (Exception ex)
                    {
                        Failure = ex.Message;
                        return RedirectToAction("ImportExamPaper");
                    }
                    Success = "Import exam paper successfully!";
                    return RedirectToAction("ExamPapers");
                }
                else
                {
                    Failure = "Please choose excel file to import exam paper";
                    return RedirectToAction("ImportExamPaper");
                }
            }
        }

        /// <summary>
        /// The ExportToPdf
        /// </summary>
        /// <param name="examPaperId">The examPaperId<see cref="int"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult _ExportToPdf(int examPaperId)
        {
            try
            {
                Models.ExamPaper examPaper = new Models.ExamPaper();
                examPaper = examPaperService.GetExamPaperById(examPaperId);
                List<QuestionDto> questions = new List<QuestionDto>();
                questions = questionService.GetQuestionsByExamPaperId(examPaper.ExamPaperID).ToList();
                List<Answer> answers = new List<Answer>();
                foreach (var item in questions)
                {
                    var answesTemp = questionService.GetAnswersByQuestionId(item.QuestionID);
                    answers.AddRange(answesTemp);
                }
                ViewBag.Answers = answers;
                ViewBag.ExamPaper = examPaper;
                return View(questions);
            }
            catch (Exception e)
            {
                Failure = e.Message;
                return Json(new { status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// The ExportToPdfView
        /// </summary>
        /// <param name="examPaperId">The examPaperId<see cref="int"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult ExportToPdfView(int examPaperId)
        {
            try
            {
                Models.ExamPaper examPaper = new Models.ExamPaper();
                examPaper = examPaperService.GetExamPaperById(examPaperId);
                return new ActionAsPdf("_ExportToPdf", new { examPaperId = examPaperId })
                {
                    FileName = examPaper.Title + ".pdf"
                };
            }
            catch (Exception e)
            {
                Failure = e.Message;
                return Json(new { status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// The ExamPaperTemplateFile
        /// </summary>
        public void ExamPaperTemplateFile()
        {
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("ExamPaper");
            ws.Cells["A1"].Style.Font.Bold = true;
            ws.Cells["A1"].Value = "Exam Paper Infomation";

            ws.Cells["A3"].Style.Font.Bold = true;
            ws.Cells["A3"].Value = "Title";
            ws.Cells["B3"].Value = "Exam paper title";
            ws.Cells["A4"].Style.Font.Bold = true;
            ws.Cells["A4"].Value = "Time";
            ws.Cells["B4"].Value = "120";
            ws.Cells["A5"].Style.Font.Bold = true;
            ws.Cells["A5"].Value = "Status";
            var status = ws.DataValidations.AddListValidation("B5");
            status.Formula.Values.Add("Public");
            status.Formula.Values.Add("Draff");

            ws.Cells["A6"].Style.Font.Bold = true;
            ws.Cells["A6"].Value = "Is Active";
            ws.Cells["B6"].Value = "TRUE";
            ws.Cells["A8"].Style.Font.Bold = true;
            ws.Cells["A8"].Value = "Question Infomation";
            ws.Cells["D8"].Style.Font.Bold = true;
            ws.Cells["D8"].Value = "Answer Infomation";
            ws.Cells["A10"].Style.Font.Bold = true;
            ws.Cells["A10"].Value = "Question Content";
            ws.Cells["B10"].Style.Font.Bold = true;
            ws.Cells["B10"].Value = "Level";
            ws.Cells["C10"].Style.Font.Bold = true;
            ws.Cells["C10"].Value = "Category";
            ws.Cells["D10"].Style.Font.Bold = true;
            ws.Cells["D10"].Value = "Answer1";
            ws.Cells["E10"].Style.Font.Bold = true;
            ws.Cells["E10"].Value = "IsCorrect Answer1";
            ws.Cells["F10"].Style.Font.Bold = true;
            ws.Cells["F10"].Value = "Answer2";
            ws.Cells["G10"].Style.Font.Bold = true;
            ws.Cells["G10"].Value = "IsCorrect Answer2";
            ws.Cells["H10"].Style.Font.Bold = true;
            ws.Cells["H10"].Value = "Answer3";
            ws.Cells["I10"].Style.Font.Bold = true;
            ws.Cells["I10"].Value = "IsCorrect Answer3";
            ws.Cells["J10"].Style.Font.Bold = true;
            ws.Cells["J10"].Value = "Answer4";
            ws.Cells["K10"].Style.Font.Bold = true;
            ws.Cells["K10"].Value = "IsCorrect Answer4";
            ws.Cells["L10"].Style.Font.Bold = true;
            ws.Cells["L10"].Value = "Answer5";
            ws.Cells["M10"].Style.Font.Bold = true;
            ws.Cells["M10"].Value = "IsCorrect Answer5";

            ws.Cells["A11"].Value = "Which of the following is not true about the MAX and MIN functions?";
            var level = ws.DataValidations.AddListValidation("B11");
            level.Formula.Values.Add("Easy");
            level.Formula.Values.Add("Normal");
            level.Formula.Values.Add("Hard");

            var category = ws.DataValidations.AddListValidation("C11");

            ws.Cells["D11"].Value = "Both can be used for any data type";
            ws.Cells["E11"].Value = "FALSE";
            ws.Cells["F11"].Value = "MAX return maximun value";
            ws.Cells["G11"].Value = "FALSE";
            ws.Cells["H11"].Value = "MIN return minium value";
            ws.Cells["I11"].Value = "FALSE";
            ws.Cells["J11"].Value = "All are true";
            ws.Cells["K11"].Value = "TRUE";

            ExcelWorksheet noteSheet = pck.Workbook.Worksheets.Add("Note");

            noteSheet.Cells["A1"].Style.Font.Bold = true;
            noteSheet.Cells["A1"].Value = "(*) Note";
            noteSheet.Cells["A2"].Value = @"To import exam paper correctly, ""Satus"", ""Level"", ""Category"" must select from dropdown list";
            noteSheet.Cells["A3"].Value = @"For each question have maximum 5 answer, if there is no content leave It blank";
            noteSheet.Cells["A:AZ"].AutoFitColumns();



            var listQuestionCategory = questionCategorySevice.GetAllQuestionCategoriesActive();
            foreach (var item in listQuestionCategory)
            {
                category.Formula.Values.Add(item.Name);
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExamPaperTemplate.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }

        public ActionResult GetQuestionCategoriesIsActive()
        {
            var listCategory = new List<QuestionCategory>();
            listCategory = questionCategorySevice.GetAllQuestionCategoriesActive().ToList();
            return Json(new { data = listCategory }, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult _GetCode(int idExamPaper)
        //{
        //    var str = examPaperService.GetCode(idExamPaper);
        //    if (str != "") {
        //        return Json(str, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(null);
        //    }
        //}
        //public JsonResult GetCodeExamPaper()
        //{
        //    GetCode();
        //}
    }
}
