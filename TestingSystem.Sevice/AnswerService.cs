using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Data.Infrastructure;
using TestingSystem.Data.Repositories;
using TestingSystem.DataTranferObject;
using TestingSystem.Models;

namespace TestingSystem.Sevice
{
    public interface IAnswerService
    {
        int DeleteAnswer(int id);
        int DelteAnswerbyQuestionID(int id);
        List<Answer> GetAnswersByQuestionID(int? id);
        int AddAnswer(Answer answer);
        bool UpdateAnswer(Answer answer);
        void DeleteAnswer(Answer answer);
        Answer GetAnswerCorrect(int idAnswer);
        List<int> GetListIdAnswerCorrectByIdQuestion(int idQuestion);
        IEnumerable<ResultCheckIdTrue> GetAllQuestionIdandAnswerIdByExampaperId(int idExamPaper);
    }
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository answerRepository;
        private readonly IUnitOfWork unitOfWork;

        public AnswerService(IAnswerRepository answerRepository, IUnitOfWork unitOfWork)
        {
            this.answerRepository = answerRepository;
            this.unitOfWork = unitOfWork;
        }

        public int AddAnswer(Answer answer)
        {
            return answerRepository.AddAnswer(answer);
        }
        public int DeleteAnswer(int id)
        {
            return answerRepository.DeleteAnswer(id);
        }

        public int DelteAnswerbyQuestionID(int id)
        {
            return answerRepository.DelteAnswerbyQuestionID(id);
        }
        public void DeleteAnswer(Answer answer)
        {
            answerRepository.Delete(answer);
        }

        public List<Answer> GetAnswersByQuestionID(int? id)
        {
            return answerRepository.GetAnswersByQuestionID(id);
        }

        public bool UpdateAnswer(Answer answer)
        {
            return answerRepository.UpdateAnswer(answer);
        }

        public Answer GetAnswerCorrect(int idAnswer)
        {
            return answerRepository.GetAnswerCorrect(idAnswer);
        }

        public List<int> GetListIdAnswerCorrectByIdQuestion(int idQuestion)
        {
            return answerRepository.GetListIdAnswerCorrectByIdQuestion(idQuestion);
        }

        public IEnumerable<ResultCheckIdTrue> GetAllQuestionIdandAnswerIdByExampaperId(int idExamPaper)
        {
            return answerRepository.GetAllQuestionIdandAnswerIdByExampaperId(idExamPaper);
        }
    }
}
