namespace UnitTestProject1
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingSystem.Data.Infrastructure;
    using TestingSystem.Data.Repositories;
    using TestingSystem.Models;
    using TestingSystem.Sevice;

    /// <summary>
    /// Defines the <see cref="ExamPaperTest" />
    /// </summary>
    [TestClass]
    public class ExamPaperTest
    {
        /// <summary>
        /// Defines the objExamPaper
        /// </summary>
        private ExamPaper objExamPaper;

        /// <summary>
        /// Defines the _mockRepository
        /// </summary>
        private Mock<IExamPaperRepository> _mockRepository;

        /// <summary>
        /// Defines the _mockUnitOfWork
        /// </summary>
        private Mock<IUnitOfWork> _mockUnitOfWork;

        /// <summary>
        /// Defines the _examPaperService
        /// </summary>
        private IExamPaperService _examPaperService;

        /// <summary>
        /// Defines the _listeExamPapers
        /// </summary>
        private List<ExamPaper> _listeExamPapers;

        /// <summary>
        /// The Initialize
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            //use mock
            _mockRepository = new Mock<IExamPaperRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            //usr mock virtual object transmission to service
            _examPaperService = new ExamPaperService(_mockRepository.Object, _mockUnitOfWork.Object);
            //create list ExamPaper
            _listeExamPapers = new List<ExamPaper>()
            {
                new ExamPaper()
                {
                    ExamPaperID = 1,
                    Title = "Exam1",
                    Time = 20,
                    NumberOfQuestion = 20,
                    IsActive = true,
                    CreatedBy = 1,
                    ModifiedBy = 1,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new ExamPaper()
                {
                    ExamPaperID = 2,
                    Title = "Exam2",
                    Time = 30,
                    NumberOfQuestion = 30,
                    IsActive = true,
                    CreatedBy = 1,
                    ModifiedBy = 1,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new ExamPaper()
                {
                    ExamPaperID = 3,
                    Title = "Exam3",
                    Time = 40,
                    NumberOfQuestion = 40,
                    IsActive = true,
                    CreatedBy = 1,
                    ModifiedBy = 1,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
            };
        }

        /// <summary>
        /// The ExamPaper_GetAll
        /// </summary>
        [TestMethod]
        public void ExamPaper_GetAll()
        {
            //Atc
            _mockRepository.Setup(m => m.GetAll()).Returns(_listeExamPapers);
            var result = _examPaperService.GetAll();
            //Assert
            Assert.AreEqual(3, result.Count());
        }

        /// <summary>
        /// The ExamPaper_Delete
        /// </summary>
        [TestMethod]
        public void ExamPaper_Delete_1()
        {
            // Arrange
            int id = -1;
            //Atc
            _mockRepository.Setup(m => m.Delete(id)).Returns(0);
            var result = _examPaperService.Delete(id);
            //Assert
            Assert.AreEqual(0, result);
        }

        /// <summary>
        /// The ExamPaper_Delete_2
        /// </summary>
        [TestMethod]
        public void ExamPaper_Delete_2()
        {
            // Arrange
            int id = 1;
            //Atc
            _mockRepository.Setup(m => m.Delete(id)).Returns(1);
            var result = _examPaperService.Delete(id);
            //Assert
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// The ExamPaper_Delete_3
        /// </summary>
        [TestMethod]
        public void ExamPaper_Delete_3()
        {
            // Arrange
            int id = 0;
            //Atc
            _mockRepository.Setup(m => m.Delete(id)).Returns(0);
            var result = _examPaperService.Delete(id);
            //Assert
            Assert.AreEqual(0, result);
        }

        /// <summary>
        /// The ExamPaper_Create
        /// </summary>
        [TestMethod]
        public void ExamPaper_Create_1()
        {
            // Arrange
            objExamPaper = new ExamPaper();
            objExamPaper.ExamPaperID = 4;
            objExamPaper.CreatedBy = 1;
            objExamPaper.CreatedDate = DateTime.Now;
            objExamPaper.IsActive = true;
            objExamPaper.ModifiedDate = DateTime.Now;
            objExamPaper.NumberOfQuestion = 20;
            objExamPaper.Status = true;
            objExamPaper.Time = 20;
            objExamPaper.Title = "Ruby";

            //Atc
            _mockRepository.Setup(m => m.Create(objExamPaper)).Returns(1);
            var result = _examPaperService.Create(objExamPaper);
            // Assert
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// The ExamPaper_Create_2
        /// </summary>
        [TestMethod]
        public void ExamPaper_Create_2()
        {
            // Arrange
            objExamPaper = new ExamPaper();
            objExamPaper.ExamPaperID = 4;
            objExamPaper.CreatedBy = 1;
            objExamPaper.CreatedDate = DateTime.Now;
            objExamPaper.IsActive = true;
            objExamPaper.ModifiedDate = DateTime.Now;
            objExamPaper.NumberOfQuestion = 20;
            objExamPaper.Status = true;
            objExamPaper.Time = 20;
            objExamPaper.Title = "";

            //Atc
            _mockRepository.Setup(m => m.Create(objExamPaper)).Returns(0);
            var result = _examPaperService.Create(objExamPaper);
            // Assert
            Assert.AreEqual(0, result);
        }

        /// <summary>
        /// The ExamPaper_Create_3
        /// </summary>
        [TestMethod]
        public void ExamPaper_Create_3()
        {
            // Arrange
            objExamPaper = new ExamPaper();
            objExamPaper.ExamPaperID = 4;
            objExamPaper.CreatedBy = 1;
            objExamPaper.CreatedDate = DateTime.Now;
            objExamPaper.IsActive = true;
            objExamPaper.ModifiedDate = DateTime.Now;
            objExamPaper.NumberOfQuestion = 20;
            objExamPaper.Status = true;
            objExamPaper.Time = 0;
            objExamPaper.Title = "";

            //Atc
            _mockRepository.Setup(m => m.Create(objExamPaper)).Returns(0);
            var result = _examPaperService.Create(objExamPaper);
            // Assert
            Assert.AreEqual(0, result);
        }

        /// <summary>
        /// The ExamPaper_Create_4
        /// </summary>
        [TestMethod]
        public void ExamPaper_Create_4()
        {
            // Arrange
            objExamPaper = new ExamPaper();
            objExamPaper.ExamPaperID = 4;
            objExamPaper.CreatedBy = 1;
            objExamPaper.CreatedDate = DateTime.Now;
            objExamPaper.IsActive = true;
            objExamPaper.ModifiedDate = DateTime.Now;
            objExamPaper.NumberOfQuestion = 0;
            objExamPaper.Status = true;
            objExamPaper.Time = 0;
            objExamPaper.Title = "PHP";

            //Atc
            _mockRepository.Setup(m => m.Create(objExamPaper)).Returns(0);
            var result = _examPaperService.Create(objExamPaper);
            // Assert
            Assert.AreEqual(0, result);
        }

        /// <summary>
        /// The ExamPaper_Edit
        /// </summary>
        [TestMethod]
        public void ExamPaper_Edit_1()
        {
            // Arrange
            int id = 1;
            _listeExamPapers[0].Title = "PHP";
            //Atc
            _mockRepository.Setup(m => m.FindById(id)).Returns(_listeExamPapers[0]);
            var result = _examPaperService.GetExamPaperById(id);
            _mockRepository.Setup(m => m.Edit(_listeExamPapers[0])).Returns(1);
            var resultEdit = _examPaperService.Edit(_listeExamPapers[0]);
            // Assert
            Assert.AreEqual(_listeExamPapers[0], result);
            Assert.AreEqual(1, resultEdit);
        }

        /// <summary>
        /// The ExamPaper_Edit_2
        /// </summary>
        [TestMethod]
        public void ExamPaper_Edit_2()
        {
            // Arrange
            int id = 1;
            _listeExamPapers[0].Title = "";

            //Atc
            _mockRepository.Setup(m => m.FindById(id)).Returns(_listeExamPapers[0]);
            var result = _examPaperService.GetExamPaperById(id);
            _mockRepository.Setup(m => m.Edit(_listeExamPapers[0])).Returns(0);
            var resultEdit = _examPaperService.Edit(_listeExamPapers[0]);
            // Assert
            Assert.AreEqual(_listeExamPapers[0], result);
            Assert.AreEqual(0, resultEdit);
        }

        [TestMethod]
        public void ExamPaper_Edit_3()
        {
            // Arrange
            int id = 0;
            _listeExamPapers[0].Title = "";

            //Atc
            _mockRepository.Setup(m => m.FindById(id)).Returns(_listeExamPapers[0]);
            var result = _examPaperService.GetExamPaperById(id);
            _mockRepository.Setup(m => m.Edit(_listeExamPapers[0])).Returns(0);
            var resultEdit = _examPaperService.Edit(_listeExamPapers[0]);
            // Assert
            Assert.AreEqual(_listeExamPapers[0], result);
            Assert.AreEqual(0, resultEdit);
        }

        /// <summary>
        /// The ExamPaper_
        /// </summary>
        [TestMethod]

        public void ExamPaper_GetNumberOfQuestionByExamPaperId_1()
        {
            // Arrange
            int id = 1;
            //Atc
            _mockRepository.Setup(m => m.GetNumberOfQuestionByExamPaperId(id)).Returns(1);
            var result = _examPaperService.GetNumberOfQuestionByExamPaperId(id);
            //Assert
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// The ExamPaper_GetNumberOfQuestionByExamPaperId_2
        /// </summary>
        [TestMethod]
        public void ExamPaper_GetNumberOfQuestionByExamPaperId_2()
        {
            // Arrange
            int id = -1;
            //Atc
            _mockRepository.Setup(m => m.GetNumberOfQuestionByExamPaperId(id)).Returns(0);
            var result = _examPaperService.GetNumberOfQuestionByExamPaperId(id);
            //Assert
            Assert.AreEqual(0, result);
        }

        /// <summary>
        /// The ExamPaper_GetNumberOfQuestionByExamPaperId_3
        /// </summary>
        [TestMethod]
        public void ExamPaper_GetNumberOfQuestionByExamPaperId_3()
        {
            // Arrange
            int id = 0;
            //Atc
            _mockRepository.Setup(m => m.GetNumberOfQuestionByExamPaperId(id)).Returns(0);
            var result = _examPaperService.GetNumberOfQuestionByExamPaperId(id);
            //Assert
            Assert.AreEqual(0, result);
        }
    }
}
