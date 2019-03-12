using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TestingSystem.Data.Infrastructure;
using TestingSystem.Data.Repositories;
using TestingSystem.DataTranferObject.Question;
using TestingSystem.Models;
using TestingSystem.Sevice;

namespace UnitTestProject1
{
    [TestClass]
    public class QuestionTest
    {
        private Mock<IQuestionRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IQuestionService _questionService;
        private List<Question> _listQuestion;
        private List<ExamPaper> _listExamPaper;
        private List<ExamPaperQuesion> _listExamPaperQuestion;
        private List<Answer> _listAnswer;
        private List<Level> _listLevels;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IQuestionRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _questionService = new QuestionService(_mockRepository.Object, _mockUnitOfWork.Object);

            _listQuestion = new List<Question>()
            {

                new Question()
                {
                    QuestionID = 1, Content = "1", Image = null, Level = 1, CategoryID = 1, IsActive = true,
                    CreatedBy = 1, ModifiedBy = 1, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now
                },
                new Question()
                {
                    QuestionID = 2, Content = "2", Image = null, Level = 1, CategoryID = 1, IsActive = true,
                    CreatedBy = 1, ModifiedBy = 1, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now
                },
                new Question()
                {
                    QuestionID = 3, Content = "3", Image = null, Level = 1, CategoryID = 1, IsActive = true,
                    CreatedBy = 1, ModifiedBy = 1, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now
                },
            };
            //
            _listExamPaper = new List<ExamPaper>()
            {
                new ExamPaper()
                {
                    ExamPaperID = 1, Title = "Title", Time = 60, Status = true, NumberOfQuestion = 30, IsActive = true,
                    CreatedBy = 1, ModifiedBy = 1, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now
                },
            };
            //
            _listExamPaperQuestion = new List<ExamPaperQuesion>()
            {
                new ExamPaperQuesion() {ExamPaperQuesionID = 1, QuestionID = 1, ExamPaperID = 1},
            };
            //
            _listAnswer = new List<Answer>()
            {
                new Answer() {AnswerID = 1,QuestionID = 1,AnswerContent = "Answer1", IsCorrect = true},
                new Answer() {AnswerID = 2,QuestionID = 1,AnswerContent = "Answer2", IsCorrect = false},
            };
            _listLevels = new List<Level>()
            {
                new Level() {LevelId = 1,LevelStep = 1,Name = "Easy"},
                new Level() {LevelId = 2,LevelStep = 2,Name = "Normal"},
                new Level() {LevelId = 3,LevelStep = 3,Name = "Hard"},
            };
        }
        // GET ALL
        [TestMethod]
        public void Service_GetAll()
        {
            _mockRepository.Setup(m => m.GetAllQuestions()).Returns(_listQuestion);
            var result = _questionService.GetAllQuestions();
            Assert.AreEqual(3, result.Count());
        }

        // CREATE QUESTION
        [TestMethod]
        public void Service_Create()
        {
            Question question = new Question();
            question.QuestionID = 10;
            question.Content = "1";
            question.Image = null;
            question.Level = 1;
            question.CategoryID = 1;
            question.IsActive = true;
            question.CreatedBy = 1;
            question.ModifiedBy = 1;
            question.CreatedDate = DateTime.Now;
            question.ModifiedDate = DateTime.Now;

            _mockRepository.Setup(m => m.AddQuestion(question)).Returns((Question p) =>
            {
                return p.QuestionID;
            });
            var result = _questionService.AddQuestion(question);
            Assert.IsNotNull(result);
            Assert.AreEqual(10, result);
        }

        //Delete
        [TestMethod]
        public void Service_Delete()
        {
            _mockRepository.Setup(m => m.DeleteQuestion(_listQuestion[1].QuestionID)).Returns(1);
            var result = _questionService.DeleteQuestion(_listQuestion[1].QuestionID);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Service_Delete_Valite()
        {
            int id = -1;
            _mockRepository.Setup(m => m.DeleteQuestion(id)).Returns(0);
            var result = _questionService.DeleteQuestion(id);
            Assert.AreEqual(0, result);
        }

        // UpDate
        [TestMethod]
        public void Service_Update()
        {
            Question question = new Question();
            question.QuestionID = 2;

            question.Content = "1";
            question.Image = null;
            question.Level = 1;
            question.CategoryID = 1;
            question.IsActive = true;
            question.CreatedBy = 1;
            question.ModifiedBy = 1;
            question.CreatedDate = DateTime.Now;
            question.ModifiedDate = DateTime.Now;

            _mockRepository.Setup(m => m.UpdateQuestion(question)).Returns(true);
            var result = _questionService.UpdateQuestion(question);
            Assert.AreEqual(true, result);
        }

        // GET BY ID
        [TestMethod]
        public void Service_GetByID()
        {
            int id = 2;
            _mockRepository.Setup(m => m.FindID(id)).Returns(_listQuestion[1]);
            var result = _questionService.FindID(id);
            Assert.AreEqual(_listQuestion[1], result);
            Assert.AreEqual(_listQuestion[1].QuestionID, result.QuestionID);
        }
        // Get Answers By QuestionId         
        [TestMethod]
        public void Service_GetAnswerByQuestionID()
        {
            int questionID = 1;
            _mockRepository.Setup(m => m.GetAnswersByQuestionId(questionID)).Returns(_listAnswer);
            var result = _questionService.GetAnswersByQuestionId(questionID);
            Assert.AreEqual(2, result.Count());
        }
        // Get All Level        
        [TestMethod]
        public void Service_GetAllLevel()
        {
            _mockRepository.Setup(m => m.GetAlLevels()).Returns(_listLevels);
            var result = _questionService.GetAlLevels();
            Assert.AreEqual(3, result.Count());
        }
    }
}
