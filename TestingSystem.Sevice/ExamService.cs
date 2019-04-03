using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Data.Infrastructure;
using TestingSystem.Data.Repositories;
using TestingSystem.Models;

namespace TestingSystem.Sevice
{
	public interface IExamService
	{
		IEnumerable<Exam> GetAllExams();
		bool UpdateExam(Exam exam);
		int AddExam(Exam exam);
		int DeleteExam(int id);
		Exam GetExamsByID(int id);
		IEnumerable<Exam> SearchExams(string txtSearch);
		IEnumerable<Test> GetTestByExamID(int examID, int idUser);
		int RemoveTestInExams(int testID, int examID);
		string GetNameExamByID(int examID);
		int AddTestIntoExams(int testID, int examID);
		Exam GetExamByCode(string examCode);
		IEnumerable<Test> GetTestByExamIDAdmin(int examID);
		bool CheckTestExistInExam(int testID, int examID);

		IEnumerable<Exam> GetExamFollow();
    }

	public class ExamService : IExamService
	{
		private readonly IExamRepository examRepository;
		private readonly IUnitOfWork unitOfWork;

		public ExamService(IExamRepository examRepository, IUnitOfWork unitOfWork)
		{
			this.examRepository = examRepository;
			this.unitOfWork = unitOfWork;
		}
		public int AddExam(Exam exam)
		{
			return examRepository.AddExam(exam);
		}

		public int DeleteExam(int id)
		{
			return examRepository.DeleteExam(id);
		}

		public IEnumerable<Exam> GetAllExams()
		{
			return examRepository.GetAllExams();
		}

		public Exam GetExamsByID(int id)
		{
			return examRepository.GetExamsByID(id);
		}

		public IEnumerable<Exam> SearchExams(string txtSearch)
		{
			return examRepository.SearchExams(txtSearch);
		}

		public IEnumerable<Test> GetTestByExamID(int examID, int idUser)
		{
			return examRepository.GetTestByExamID(examID, idUser);
		}

		public int RemoveTestInExams(int testID, int examID)
		{
			return examRepository.RemoveTestInExams(testID,examID);
		}

		public string GetNameExamByID(int examID)
		{
			return examRepository.GetNameExamByID(examID);
		}

		public int AddTestIntoExams(int testID, int examID)
		{
			return examRepository.AddTestIntoExams(testID, examID);
		}

		public Exam GetExamByCode(string examCode)
		{
			return examRepository.GetExamByCode(examCode);
		}

		public IEnumerable<Test> GetTestByExamIDAdmin(int examID)
		{
			return examRepository.GetTestByExamIDAdmin(examID);
		}

		public bool CheckTestExistInExam(int testID, int examID)
		{
			return examRepository.CheckTestExistInExam(testID, examID);
		}


		public bool UpdateExam(Exam exam)
		{
			return examRepository.UpdateExam(exam);
		}

        public IEnumerable<Exam> GetExamFollow()
        {
            return examRepository.GetAllFollow();
        }
    }
}
