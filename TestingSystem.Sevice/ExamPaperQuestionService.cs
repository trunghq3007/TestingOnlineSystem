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
    public interface IExamPaperQuestionService
    {
        int DeleteExamPaperQuestionByExamPaperIdAndQuestionId(int examPaperId, int questionId);

        int DeleteExamPaperQuestion(int examPaperQuestionId);

        int InsertExamPaperQuestion(int examPaperId, int questionId);

        ExamPaperQuesion GetExamPaperQuesionByExamPaperIdAndQuestionId(int examPaperId, int questionId);

        IEnumerable<ExamPaperQuesion> GetExamPaperQuestionsByExamPaperId(int examPaperId);

    }
    public class ExamPaperQuestionService : IExamPaperQuestionService
    {
        private readonly IExamPaperQuestionRepository examPaperQuestionRepository;
        private readonly IUnitOfWork unitOfWork;

        public ExamPaperQuestionService(IExamPaperQuestionRepository examPaperQuestionRepository, IUnitOfWork unitOfWork)
        {
            this.examPaperQuestionRepository = examPaperQuestionRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<ExamPaperQuesion> GetExamPaperQuestionsByExamPaperId(int examPaperId)
        {
            return examPaperQuestionRepository.GetExamPaperQuesionsByExamPaperId(examPaperId);
        }

        public ExamPaperQuesion GetExamPaperQuesionByExamPaperIdAndQuestionId(int examPaperId, int questionId)
        {
            return examPaperQuestionRepository.GetExamPaperQuesionByExamPaperIdAndQuestionId(examPaperId, questionId);
        }

        public int DeleteExamPaperQuestionByExamPaperIdAndQuestionId(int examPaperId, int questionId)
        {
            return examPaperQuestionRepository.DeleteExamPaperQuestionByExamPaperIdAndQuestionId(examPaperId, questionId);
        }

        public int DeleteExamPaperQuestion(int examPaperQuestionId)
        {
            return examPaperQuestionRepository.DeleteExamPaperQuestion(examPaperQuestionId);
        }

        public int InsertExamPaperQuestion(int examPaperId, int questionId)
        {
            return examPaperQuestionRepository.InsertExamPaperQuestion(examPaperId, questionId);
        }
    }
}
