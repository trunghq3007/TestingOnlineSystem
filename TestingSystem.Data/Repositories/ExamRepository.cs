using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Data.Infrastructure;
using TestingSystem.Models;

namespace TestingSystem.Data.Repositories
{
	public interface IExamRepository : IRepository<Exam>
	{
		//Get all exams to view list exam
		IEnumerable<Exam> GetAllExams();
		IEnumerable<Exam> GetAllFollow();
		// Update exam
		bool UpdateExam(Exam exam);
		// Get exam by exam id
		Exam GetExamsByID(int id);
		// Create new exam
		int AddExam(Exam exam);
		// Delete exam
		int DeleteExam(int id);
		// Search exam by name
		IEnumerable<Exam> SearchExams(string txtSearch);
		// Get all test by exam id(use for client).
		IEnumerable<Test> GetTestByExamID(int examID, int idUser);
		// Delete test in list exam
		int RemoveTestInExams(int testID, int examID);
		// Add test into exam
		int AddTestIntoExams(int testID, int examID);
		// Get Name exam by exam id
		string GetNameExamByID(int examID);
		// Get test by exam test id (use for admin)
		IEnumerable<Test> GetTestByExamIDAdmin(int examID);
		// Get code exam
		Exam GetExamByCode(string examCode);
		//Check test exist in exam
		bool CheckTestExistInExam(int testID, int examID);
	}
	public class ExamRepository : RepositoryBase<Exam>, IExamRepository
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public ExamRepository(IDbFactory dbFactory) : base(dbFactory)
		{

		}

		public IEnumerable<Exam> GetAllExams()
		{
			try
			{
				var listExam = DbContext.Exams.ToList();
				return listExam;
			}
			catch (Exception e)
			{
				log.Debug(e.Message);
				return null;
			}

		}

		public bool UpdateExam(Exam exam)
		{
			try
			{
				var objExam = this.DbContext.Exams.Find(exam.ExamID);
				if (objExam != null)
				{
					objExam.ExamName = exam.ExamName;
					objExam.Description = exam.Description;
					objExam.StartDate = exam.StartDate;
					objExam.EndDate = exam.EndDate;
					objExam.CreateDate = objExam.CreateDate;
					objExam.Status = exam.Status;

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

		public Exam GetExamsByID(int id)
		{
			try
			{
				var exam = DbContext.Exams.SingleOrDefault(x => x.ExamID == id);
				return exam;
			}
			catch (Exception e)
			{
				log.Debug(e.Message);
				return null;
			}

		}

		public int AddExam(Exam exam)
		{
			try
			{
				exam.ExamCode = Guid.NewGuid().ToString();
				exam.CreateDate = DateTime.Now;
				DbContext.Exams.Add(exam);
				DbContext.SaveChanges();
				return exam.ExamID;
			}
			catch (Exception e)
			{
				log.Debug(e.Message);
				return 0;
			}

		}

		public int DeleteExam(int id)
		{
			try
			{
				var exam = DbContext.Exams.Find(id);
				if (exam != null)
				{
					//kiem tra xem co test result cua bai test hay khong neu co thi khong duoc xoa ky thi
					var checkTestHaveTestResult = DbContext.TestResults.Where(x => x.TestID == id).ToList().Count;
					if (checkTestHaveTestResult > 0)
					{
						return 0;
					}
					else
					{
						this.DbContext.Exams.Remove(exam);
						return DbContext.SaveChanges();
					}
				}
				else
				{
					return 0;
				}
			}
			catch (Exception e)
			{
				log.Debug(e.Message);
				return 0;
			}

		}

		public IEnumerable<Exam> SearchExams(string txtSearch)
		{
			var listExams = DbContext.Exams.Where(x => x.ExamName.Contains(txtSearch)).ToList();
			return listExams;
		}

		public IEnumerable<Test> GetTestByExamID(int examID, int idUser)
		{
			// lay tat ca examtest theo examid
			var listExamTestByExamID = DbContext.ExamTests.Where(x => x.ExamID == examID).ToList();
			List<Test> listTests = new List<Test>();
			// lay tat ca test theo testid trong examtest
			foreach (var item in listExamTestByExamID)
			{
				var examTest = DbContext.Tests.SingleOrDefault(x => x.TestID == item.TestID);
				listTests.Add(examTest);
			}

			// list can tra ve
			List<Test> listTestReturn = new List<Test>();

			// list candidate id trong bang CandidatesTest
			List<int> listTestIdByCandidateID = new List<int>();
			// lay tat ca CandidatesTest theo candidateid
			var listTestByCandidateId = this.DbContext.CandidatesTests.Where(s => s.CandidateID == idUser).ToList();
			foreach (var item in listTestByCandidateId)
			{
				listTestIdByCandidateID.Add(item.TestID);
			}

			foreach (var item2 in listTests)
			{
				foreach (var item3 in listTestIdByCandidateID)
				{
					if (item2.TestID == item3)
					{
						listTestReturn.Add(item2);
					}
				}
			}

			return listTestReturn.AsEnumerable();
		}

		public string GetNameExamByID(int examID)
		{
			var examName = DbContext.Exams.SingleOrDefault(x => x.ExamID == examID).ExamName;
			return examName;
		}

		public IEnumerable<Test> GetTestByExamIDAdmin(int examID)
		{
			// lay tat ca examtest theo examid
			var listExamTestByExamID = DbContext.ExamTests.Where(x => x.ExamID == examID).ToList();
			var listTest = DbContext.Tests.ToList();
			var listReturn = new List<Test>();
			; foreach (var item in listExamTestByExamID)
			{
				foreach (var item2 in listTest)
				{
					if (item2.TestID == item.TestID)
					{
						listReturn.Add(item2);
					}
				}
			}
			return listReturn.AsEnumerable();
		}		

		public int AddTestIntoExams(int testID, int examID)
		{
			ExamTest examTest = new ExamTest();
			examTest.TestID = testID;
			examTest.ExamID = examID;
			DbContext.ExamTests.Add(examTest);
			DbContext.SaveChanges();
			return examTest.ExamID;
		}

		public Exam GetExamByCode(string examCode)
		{
			var exam = DbContext.Exams.SingleOrDefault(x => x.ExamCode == examCode);
			return exam;
		}

		public bool CheckTestExistInExam(int testID, int examID)
		{
			var checkExists = DbContext.ExamTests.Where(x => x.TestID == testID && x.ExamID == examID).ToList();
			if (checkExists != null)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public IEnumerable<Exam> GetAllFollow()
		{
			var exam = DbContext.Exams.Where(x => x.Status == 1).ToList();
			return exam;
		}

		public int RemoveTestInExams(int testID, int examID)
		{
			try
			{
				var test = DbContext.ExamTests.FirstOrDefault(x => x.TestID == testID && x.ExamID == examID);
				if (test != null)
				{
					this.DbContext.ExamTests.Remove(test);
					return DbContext.SaveChanges();
				}
				else
				{
					return 0;
				}
			}
			catch (Exception e)
			{
				log.Debug(e.Message);
				return 0;
			}
		}
	}
}
