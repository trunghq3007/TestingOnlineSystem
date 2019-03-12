using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestingSystem.Data;
using TestingSystem.Data.Infrastructure;
using TestingSystem.Data.Repositories;
using TestingSystem.Models;
using TestingSystem.Sevice;

namespace UnitTestProject1
{
    [TestClass]
    public class QuestionCategoryTest
    {
        private Mock<IQuestionCategoryRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IQuestionCategorySevice _questionCategoryService;
        private List<TestingSystem.Models.QuestionCategory> _listCategory;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IQuestionCategoryRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _questionCategoryService = new QuestionCategorySevice(_mockRepository.Object, _mockUnitOfWork.Object);
            _listCategory = new List<TestingSystem.Models.QuestionCategory>()
            {
                new TestingSystem.Models.QuestionCategory()
                {
                    CategoryID = 1, Name = "1", IsActive = true, CreatedBy = 1, ModifiedBy = 1,
                    CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now
                },
                new TestingSystem.Models.QuestionCategory()
                {
                    CategoryID = 2, Name = "2", IsActive = true, CreatedBy = 1, ModifiedBy = 1,
                    CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now
                },
                new TestingSystem.Models.QuestionCategory()
                {
                    CategoryID = 3, Name = "3", IsActive = true, CreatedBy = 1, ModifiedBy = 1,
                    CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now
                },

            };
        }



        [TestMethod]
        public void Service_GetAll()
        {
            _mockRepository.Setup(m => m.GetAllQuestionCategories()).Returns(_listCategory);
            var result = _questionCategoryService.GetAllQuestionCategories();
            Assert.AreEqual(3, result.Count());
        }



        //[TestMethod]
        //public void Service_GetAll()
        //{
        //    ////setup method
        //    //_mockRepository.Setup(m => m.GetAllQuestions().ToList().Returns(_listCategory);

        //    //call action
        //    var result = _questionService.GetAllQuestion() as List<Models.Question>;

        //    //compare
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(3, result.Count);
        //}

        [TestMethod]
        public void Service_Create()
        {
            TestingSystem.Models.QuestionCategory question = new TestingSystem.Models.QuestionCategory();
            question.CategoryID = 10;
            question.Name = "1";
            question.IsActive = true;
            question.CreatedBy = 1;
            question.ModifiedBy = 1;
            question.CreatedDate = DateTime.Now;
            question.ModifiedDate = DateTime.Now;

            _mockRepository.Setup(m => m.AddCategoryQuestion(question))
                .Returns((TestingSystem.Models.QuestionCategory p) => { return p.CategoryID; });

            var result = _questionCategoryService.AddCategoryQuestion(question);

            Assert.IsNotNull(result);
            Assert.AreEqual(10, result);


        }
        [TestMethod]
        public void Service_Delete()
        {


            TestingSystem.Models.QuestionCategory question = new TestingSystem.Models.QuestionCategory();
            //int questionID = 1;           
            _mockRepository.Setup(m => m.Delete(_listCategory[1].CategoryID)).Returns(1);
            var result = _questionCategoryService.Delete(_listCategory[1].CategoryID);
            Assert.AreEqual(1, result);



        }
        [TestMethod]
        public void Service_Update()
        {
            TestingSystem.Models.QuestionCategory question = new TestingSystem.Models.QuestionCategory();
            question.CategoryID = 1;
            question.Name = "2";
            question.IsActive = true;
            question.CreatedBy = 1;
            question.ModifiedBy = 1;
            question.CreatedDate = DateTime.Now;
            question.ModifiedDate = DateTime.Now;

            _mockRepository.Setup(m => m.UpdateCategoryQuestion(question))
                .Returns((TestingSystem.Models.QuestionCategory p) => { return p.CategoryID; });

            var result = _questionCategoryService.UpdateCategoryQuestion(question);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);


        }
        [TestMethod]
        public void Service_FindCategoryByID()
        {
            int id = 2;
            _mockRepository.Setup(m => m.FindCategoryByID(id)).Returns(_listCategory[1]);
            var result = _questionCategoryService.FindCategoryByID(id);
            Assert.AreEqual(_listCategory[1], result);
        }
        [TestMethod]
        public void Service_QuestionCategoryID()
        {
            int id = 4;
            _mockRepository.Setup(m => m.QuestionCategoryID(id)).Returns(false);
            var result = _questionCategoryService.QuestionCategoryID(id);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Service_Search()
        {

            _mockRepository.Setup(m => m.SearchCategories("1")).Returns(_listCategory);
            var result = _questionCategoryService.SearchCategories("1");
            Assert.AreEqual(3, result.Count());
        }
    }

}
