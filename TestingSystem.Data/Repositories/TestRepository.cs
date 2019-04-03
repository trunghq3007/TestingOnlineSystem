using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Data.Infrastructure;
using TestingSystem.Models;

namespace TestingSystem.Data.Repositories
{
	public interface ITestRepository : IRepository<Test>
	{
        /// <summary>
        /// GetAllTest
        /// </summary>
        /// <returns></returns>
		IEnumerable<Test> GetAllTest();
        /// <summary>
        /// UpdateTest
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
		bool UpdateTest(Test entity);
        /// <summary>
        /// GetTestById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		Test GetTestByID(int id);
        /// <summary>
        /// AddTest
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
		int AddTest(Test entity);
        /// <summary>
        /// DeleteTest
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		int DeleteTest(int id);
        /// <summary>
        /// GetAllTestIsActive
        /// </summary>
        /// <returns></returns>
		IEnumerable<Test> GetAllTestIsActive();
        /// <summary>
        /// GetAllTestIsActiveByKeySearch
        /// </summary>
        /// <param name="keySearch"></param>
        /// <returns></returns>
		IEnumerable<Test> GetAllTestIsActiveByKeySearch(string keySearch);
        /// <summary>
        /// GetAllTetByExamCode
        /// </summary>
        /// <param name="examCode"></param>
        /// <returns></returns>
		IEnumerable<Test> GetAllTetByExamCode(string examCode);
        /// <summary>
        /// SearchExams
        /// </summary>
        /// <param name="txtSearch"></param>
        /// <returns></returns>
		IEnumerable<Test> SearchExams(string txtSearch);
        /// <summary>
        /// GetExamPaperIdByTestId
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
		int GetExamPaperIdByTestId(int testId);

	}
	public class TestRepository : RepositoryBase<Test>, ITestRepository
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		public TestRepository(IDbFactory dbFactory) : base(dbFactory)
		{

		}
        /// <summary>
        /// Add Test
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
		public int AddTest(Test entity)
		{
			try
			{
				entity.Description = "good";
				entity.CreateDate = DateTime.Now;
				DbContext.Tests.Add(entity);
				return DbContext.SaveChanges();

			}
			catch (Exception e)
			{
				log.Debug(e.Message);
				return 0;
			}
		}
        /// <summary>
        /// Delete Test
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		public int DeleteTest(int id)
		{
			try
			{
				var test = DbContext.Tests.Find(id);
				var checkTestResult = DbContext.TestResults.FirstOrDefault(x => x.TestID == id);
				if (checkTestResult != null)
				{
					return 0;
				}
				else
				{
					if (test != null)
					{
						this.DbContext.Tests.Remove(test);
						return DbContext.SaveChanges();
					}
					else
					{
						return 0;
					}
				}

			}
			catch (Exception e)
			{
				log.Debug(e.Message);
				return 0;
			}
		}
        /// <summary>
        /// GetALL Test By IsActive
        /// </summary>
        /// <returns></returns>
		public IEnumerable<Test> GetAllTestIsActive()
		{
			var listTestActive = DbContext.Tests.Where(x => x.IsActive == true).ToList();
			return listTestActive.AsEnumerable();
		}
        /// <summary>
        /// Get All Test where KeySeach
        /// </summary>
        /// <param name="keySearch"></param>
        /// <returns></returns>
		public IEnumerable<Test> GetAllTestIsActiveByKeySearch(string keySearch)
		{
			var listTestActiveByKey = DbContext.Tests.Where(x => x.TestName.Contains(keySearch) && x.IsActive == true);
			return listTestActiveByKey;
		}

        /// <summary>
        /// Get all Test by Code
        /// </summary>
        /// <param name="examCode"></param>
        /// <returns></returns>
		public IEnumerable<Test> GetAllTetByExamCode(string examCode)
		{
			var examByCode = DbContext.Exams.SingleOrDefault(x => x.ExamCode == examCode);
			var listExamTestByExamID = DbContext.ExamTests.Where(x => x.ExamID == examByCode.ExamID).ToList();
			List<Test> listTest = new List<Test>();
			foreach (var item in listExamTestByExamID)
			{
				var test = DbContext.Tests.SingleOrDefault(x => x.TestID == item.TestID);
				listTest.Add(test);
			}

			return listTest.AsEnumerable();
		}

        /// <summary>
        /// Get All Test
        /// </summary>
        /// <returns></returns>
		public IEnumerable<Test> GetAllTest()
		{
			try
			{
				var listTest = DbContext.Tests.ToList();
				return listTest;
			}
			catch (Exception e)
			{
				log.Debug(e.Message);
				return null;
			}
		}

        /// <summary>
        /// Get Test by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		public Test GetTestByID(int id)
		{
			var model = DbContext.Tests.Find(id);
			return model;
		}
        /// <summary>
        /// Get List Test by Search
        /// </summary>
        /// <param name="txtSearch"></param>
        /// <returns></returns>
		public IEnumerable<Test> SearchExams(string txtSearch)
		{
			var listTest = DbContext.Tests.Where(x => x.TestName.Contains(txtSearch)).ToList();
			return listTest;
		}
        /// <summary>
        /// Update Test
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
		public bool UpdateTest(Test entity)
		{
			try
			{
				var objTest = this.DbContext.Tests.Find(entity.TestID);
				if (objTest != null)
				{
					objTest.TestName = entity.TestName;
					objTest.Description = entity.Description;
					objTest.IsActive = entity.IsActive;
					objTest.PassingScore = entity.PassingScore;
					objTest.ModifiedDate = DateTime.Now;
					objTest.StartDate = entity.StartDate;
					objTest.EndDate = entity.EndDate;
					objTest.ExamPaperID = entity.ExamPaperID;
					objTest.Status = entity.Status;
					this.DbContext.SaveChanges();
					return true;
				}
				return false;
			}
			catch (Exception e)
			{
				log.Debug(e.Message);
				return false;
			}
		}

        /// <summary>
        /// Get ExamPaper by TestId
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
		public int GetExamPaperIdByTestId(int testId)
		{
			var item = this.DbContext.Tests.Where(s => s.TestID == testId).FirstOrDefault();
			if (item != null)
			{
				return item.ExamPaperID;
			}
			return 0;
		}
	}
}
