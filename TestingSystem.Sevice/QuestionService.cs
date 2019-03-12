using System.Collections.Generic;
using System.Linq;
using TestingSystem.Data.Infrastructure;
using TestingSystem.Data.Repositories;
using TestingSystem.DataTranferObject.Question;
using TestingSystem.Models;

namespace TestingSystem.Sevice
{
    public interface IQuestionService
    {
        QuestionDto GetQuestionInQuestionDTO(int? id, QuestionFilterModel searchModel);
        IEnumerable<Level> GetAlLevels();
        IQueryable<QuestionDto> GetAllQuestionDtos(QuestionFilterModel searchModel);
        bool UpdateQuestion(Question question);
        int AddQuestion(Question question);
        Question FindID(int? id);
        int DeleteQuestion(int id);
        Question GetQuetionById(int id);
        IEnumerable<QuestionDto> SearchByContent(string input, QuestionFilterModel searchModel);
        IQueryable<Question> FilterQuestions(QuestionFilterModel searchModel);
        IEnumerable<Question> GetAllQuestion();
        IEnumerable<QuestionDto> GetQuestionsByExamPaperId(int examPaperId);
        IEnumerable<QuestionDto> GetQuestionsByQuestionCategoryIdAndExamPaperId(int categoryId, int examPaperId);
        IEnumerable<QuestionDto> RandomQuestionsByCategoryIdAndExamPaperIdAndNumber(int categoryId, int examPaperId, int number);
        IEnumerable<Answer> GetAnswersByQuestionId(int? id);
        IEnumerable<Question> GetAllQuestions();
        IEnumerable<Answer> GetAllAnswers();

    }
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository questionRepository;
        private readonly IUnitOfWork unitOfWork;

        public QuestionService(IQuestionRepository questionRepository, IUnitOfWork unitOfWork)
        {
            this.questionRepository = questionRepository;
            this.unitOfWork = unitOfWork;
        }
        public int DeleteQuestion(int id)
        {
            return questionRepository.DeleteQuestion(id);
        }

        public IQueryable<Question> FilterQuestions(QuestionFilterModel searchModel)
        {
            return questionRepository.FilterQuestions(searchModel);
        }

        public Question FindID(int? id)
        {
            return questionRepository.FindID(id);
        }

        public IEnumerable<Question> GetAllQuestion()
        {
            return questionRepository.GetAll();
        }

        public Question GetQuetionById(int id)
        {
            return questionRepository.GetById(id);
        }

        public IEnumerable<QuestionDto> SearchByContent(string input, QuestionFilterModel searchModel)
        {
            return questionRepository.SearchByContent(input, searchModel);
        }

        public bool UpdateQuestion(Question question)
        {
            return questionRepository.UpdateQuestion(question);
        }

        int IQuestionService.AddQuestion(Question question)
        {
            return questionRepository.AddQuestion(question);
        }

        public IEnumerable<QuestionDto> GetQuestionsByExamPaperId(int examPaperId)
        {
            return questionRepository.GetQuestionsByExamPaperId(examPaperId);
        }

        public IEnumerable<QuestionDto> GetQuestionsByQuestionCategoryIdAndExamPaperId(int categoryId, int examPaperId)
        {
            return questionRepository.GetQuestionsByQuestionCategoryIdAndExamPaperId(categoryId, examPaperId);
        }

        public IEnumerable<Level> GetAlLevels()
        {
            return questionRepository.GetAlLevels();
        }

        public IEnumerable<QuestionDto> RandomQuestionsByCategoryIdAndExamPaperIdAndNumber(int categoryId, int examPaperId, int number)
        {
            return questionRepository.RandomQuestionsByCategoryIdAndExamPaperIdAndNumber(categoryId, examPaperId, number);
        }
        public IEnumerable<Answer> GetAnswersByQuestionId(int? id)
        {
            return questionRepository.GetAnswersByQuestionId(id);
        }

        public IEnumerable<Question> GetAllQuestions()
        {
            return questionRepository.GetAllQuestions();
        }

        public IEnumerable<Answer> GetAllAnswers()
        {
            return questionRepository.GetAllAnswers();
        }

        public QuestionDto GetQuestionInQuestionDTO(int? id, QuestionFilterModel searchModel)
        {
            return questionRepository.GetQuestionInQuestionDto(id, searchModel);
        }

        public IQueryable<QuestionDto> GetAllQuestionDtos(QuestionFilterModel searchModel)
        {
            return questionRepository.GetAllQuestionDtos(searchModel);
        }
    }
}
