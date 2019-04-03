using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Data.Repositories;
using TestingSystem.Models;

namespace TestingSystem.Sevice
{
	public interface ITestService
	{
		IEnumerable<Test> GetAllTests();
		bool UpdateTest(Test test);
		int AddTest(Test test);
		int DeleteTest(int id);
		Test GetTestByID(int id);
		IEnumerable<Test> GetAllTestIsActive();
		IEnumerable<Test> GetAllTestIsActiveByKeySearch(string keySearch);
		IEnumerable<Test> GetAllTetByExamCode(string examCode);
		IEnumerable<Test> SearchExams(string txtSearch);
        int GetExamPaperIdByTestId(int testId);

    }
	public class TestService : ITestService
	{
		private readonly ITestRepository testRepository;
		public TestService(ITestRepository TestRepository)
		{
			this.testRepository = TestRepository;
		}
        /// <summary>
        /// AddTest
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
		public int AddTest(Test test)
		{
			return testRepository.AddTest(test);
		}
        /// <summary>
        /// DeleteTest
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		public int DeleteTest(int id)
		{
			return testRepository.DeleteTest(id);
		}
        /// <summary>
        /// GetAllTests
        /// </summary>
        /// <returns></returns>
		public IEnumerable<Test> GetAllTests()
		{
			return testRepository.GetAllTest();
		}
        /// <summary>
        /// GetTestByID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		public Test GetTestByID(int id)
		{
			return testRepository.GetTestByID(id);
		}
        /// <summary>
        /// GetAllTestIsActive
        /// </summary>
        /// <returns></returns>
		public IEnumerable<Test> GetAllTestIsActive()
		{
			return testRepository.GetAllTestIsActive();
		}
        /// <summary>
        /// GetAllTestIsActiveByKeySearch
        /// </summary>
        /// <param name="keySearch"></param>
        /// <returns></returns>
		public IEnumerable<Test> GetAllTestIsActiveByKeySearch(string keySearch)
		{
			return testRepository.GetAllTestIsActiveByKeySearch(keySearch);
		}
        /// <summary>
        /// GetAllTetByExamCode
        /// </summary>
        /// <param name="examCode"></param>
        /// <returns></returns>
		public IEnumerable<Test> GetAllTetByExamCode(string examCode)
		{
			return testRepository.GetAllTetByExamCode(examCode);
		}
        /// <summary>
        /// UpdateTest
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
		public bool UpdateTest(Test test)
		{
			return testRepository.UpdateTest(test);
		}
        /// <summary>
        /// SearchExams
        /// </summary>
        /// <param name="txtSearch"></param>
        /// <returns></returns>
        public IEnumerable<Test> SearchExams(string txtSearch)
        {
            return testRepository.SearchExams(txtSearch);
        }
        /// <summary>
        /// GetExamPaperIdByTestId
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        public int GetExamPaperIdByTestId(int testId)
        {
            return testRepository.GetExamPaperIdByTestId(testId);
        }
    }
}
