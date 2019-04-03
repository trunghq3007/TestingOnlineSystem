using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using TestingSystem.Data.Infrastructure;
using TestingSystem.DataTranferObject.Question;
using TestingSystem.Models;

namespace TestingSystem.Data.Repositories
{
    public interface IQuestionRepository : IRepository<Question>
    {
        QuestionDto GetQuestionInQuestionDto(int? id, QuestionFilterModel searchModel);
        string GetNameLevelByQuestionID(int id);
        IEnumerable<Level> GetAlLevels();
        IQueryable<QuestionDto> GetAllQuestionDtos(QuestionFilterModel searchModel);
        bool UpdateQuestion(Question question);
        int AddQuestion(Question question);
        int DeleteQuestion(int id);
        Question FindID(int? id);
        bool CheckQuestionInExamPaperQuesion(int id);
        IEnumerable<QuestionDto> SearchByContent(string input, QuestionFilterModel searchModel);
        IQueryable<Question> FilterQuestions(QuestionFilterModel searchModel);

        IEnumerable<QuestionDto> GetQuestionsByExamPaperId(int examPaperId);

        IEnumerable<QuestionDto> GetQuestionsByQuestionCategoryIdAndExamPaperId(int categoryId, int examPaperId);

        IEnumerable<QuestionDto> RandomQuestionsByCategoryIdAndExamPaperIdAndNumber(int categoryId, int examPaperId, int number);
        IEnumerable<Answer> GetAnswersByQuestionId(int? id);
        IEnumerable<Question> GetAllQuestions();
        IEnumerable<Answer> GetAllAnswers();

        

    }
    public class QuestionRepository : RepositoryBase<Question>, IQuestionRepository
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IQuestionCategoryRepository questionCategory;
        private readonly IUserRepository userRepository;
        private readonly IExamPaperQuestionRepository examPaperQuestionRepository;


        public QuestionRepository(IDbFactory dbFactory, IQuestionCategoryRepository questionCategory, IExamPaperQuestionRepository examPaperQuestionRepository, IUserRepository userRepository) : base(dbFactory)
        {
            this.questionCategory = questionCategory;
            this.examPaperQuestionRepository = examPaperQuestionRepository;
            this.userRepository = userRepository;
        }
        public Question FindID(int? id)
        {
            try
            {
                var question = this.DbContext.Questions.SingleOrDefault(x => x.QuestionID == id);
                return question;
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return null;//
            }
        }
        public int DeleteQuestion(int id)
        {
            try
            {
                if (CheckQuestionInExamPaperQuesion(id) == false)
                {
                    var question = this.DbContext.Questions.Find(id);
                    if (question != null)
                    {
                        this.DbContext.Questions.Remove(question);
                        return DbContext.SaveChanges();
                    }
                    else
                    {
                        return 0;
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

        public IQueryable<Question> FilterQuestions(QuestionFilterModel searchModel)
        {
            var result = this.DbContext.Questions.AsQueryable();
            try
            {
                if (searchModel != null)
                {
                    if (searchModel.QuestionID.HasValue)
                        result = result.Where(x => x.QuestionID == searchModel.QuestionID);

                    if (!string.IsNullOrEmpty(searchModel.Content))
                        result = result.Where(x => x.Content.Contains(searchModel.Content));

                    if (searchModel.Level.HasValue)
                        result = result.Where(x => x.Level == searchModel.Level);

                    if (searchModel.CategoryID.HasValue)
                        result = result.Where(x => x.CategoryID == searchModel.CategoryID);

                    if (!string.IsNullOrEmpty(searchModel.CreatedBy))
                    {
                        var user = DbContext.Users.SingleOrDefault(x => x.Name == searchModel.CreatedBy);
                        if (user != null)
                        {
                            result = result.Where(x => x.CreatedBy == user.UserId);
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    if (searchModel.FromDate.HasValue)
                    {
                        result = result.Where(x => x.CreatedDate > searchModel.FromDate && x.CreatedDate < searchModel.ToDate);
                    }
                }

                return result;
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return result;
            }

        }
        public IEnumerable<QuestionDto> SearchByContent(string input, QuestionFilterModel searchModel)
        {
            try
            {
                var search = GetAllQuestionDtos(searchModel).Where(x => x.Content.Contains(input)).ToList();
                return search.AsEnumerable();
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                throw;
            }

        }

        public int AddQuestion(Question question)
        {
            try
            {
                question.CreatedDate = DateTime.Now;
                DbContext.Questions.Add(question);
                DbContext.SaveChanges();
                return question.QuestionID;
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return 0;//
            }

        }

        public bool UpdateQuestion(Question question)
        {
            try
            {
                var objQuestion = this.DbContext.Questions.Find(question.QuestionID);
                if (objQuestion != null)
                {
                    objQuestion.Content = question.Content;
                    objQuestion.Image = question.Image;
                    objQuestion.Level = question.Level;
                    objQuestion.CategoryID = question.CategoryID;
                    objQuestion.IsActive = question.IsActive;
                    objQuestion.CreatedBy = objQuestion.CreatedBy;
                    objQuestion.CreatedDate = objQuestion.CreatedDate;
                    objQuestion.ModifiedBy = question.ModifiedBy;
                    objQuestion.ModifiedDate = DateTime.Now;
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

        public IQueryable<QuestionDto> GetAllQuestionDtos(QuestionFilterModel searchModel)
        {
            var listQuestionDtos = new List<QuestionDto>();
            try
            {
                if (FilterQuestions(searchModel) != null)
                {
                    foreach (var item in FilterQuestions(searchModel))
                    {
                        listQuestionDtos.Add(new QuestionDto
                        {
                            QuestionID = item.QuestionID,
                            IsActive = item.IsActive,
                            Content = item.Content,
                            Image = item.Image,
                            CreatedBy = item.CreatedBy,
                            CreatedName = userRepository.GetUserById(item.CreatedBy).Name,
                            CreatedDate = item.CreatedDate,
                            ModifiedBy = item.ModifiedBy,
                            ModifiedName = userRepository.GetUserById(item.ModifiedBy.GetValueOrDefault()).Name,
                            ModifiedDate = item.ModifiedDate,
                            CategoryID = item.CategoryID,
                            CategoryName = questionCategory.FindCategoryByID(item.CategoryID).Name,
                            Level = item.Level,
                            LevelName = GetNameLevelByQuestionID(item.QuestionID)
                        });
                    }
                }
                else
                {
                    //listQuestionDtos = null;
                }
                var listQuestion = listQuestionDtos.AsQueryable();
                return listQuestion.OrderByDescending(x => x.CreatedDate);
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return listQuestionDtos.AsQueryable();
            }

        }

        public IEnumerable<QuestionDto> GetQuestionsByExamPaperId(int examPaperId)
        {
            DbContext.Configuration.ProxyCreationEnabled = false;
            var examPaperQuestions = DbContext.ExamPaperQuesions.Where(e => e.ExamPaperID == examPaperId).ToList();
            List<QuestionDto> questionsDto = new List<QuestionDto>();
            foreach (var item in examPaperQuestions)
            {
                var question = new Question();
                var QuestionDto = new QuestionDto();
                question = DbContext.Questions.SingleOrDefault(e => e.QuestionID == item.QuestionID);
                QuestionDto.IsActive = question.IsActive;
                QuestionDto.Content = question.Content;
                QuestionDto.Image = question.Image;
                QuestionDto.QuestionID = question.QuestionID;
                QuestionDto.CreatedBy = question.CreatedBy;
                QuestionDto.CreatedDate = question.CreatedDate;
                QuestionDto.ModifiedBy = question.ModifiedBy;
                QuestionDto.ModifiedDate = question.ModifiedDate;
                QuestionDto.CategoryID = question.CategoryID;
                QuestionDto.CategoryName = DbContext.QuestionCategories.SingleOrDefault(q => q.CategoryID == question.CategoryID).Name;
                QuestionDto.ExamPaperQuestionID = item.ExamPaperQuesionID;
                if (question.Level == 1)
                {
                    QuestionDto.LevelName = "Easy";
                }
                else if (question.Level == 2)
                {
                    QuestionDto.LevelName = "Normal";
                }
                else
                {
                    QuestionDto.LevelName = "Hard";
                }
                questionsDto.Add(QuestionDto);
            }
            return questionsDto;
        }

        public IEnumerable<QuestionDto> GetQuestionsByQuestionCategoryIdAndExamPaperId(int categoryId, int examPaperId)
        {
            DbContext.Configuration.ProxyCreationEnabled = false;

            List<int> temQuestionId = new List<int>();
            List<ExamPaperQuesion> examPaperQuesions = new List<ExamPaperQuesion>();
            examPaperQuesions = examPaperQuestionRepository.GetExamPaperQuesionsByExamPaperId(examPaperId).ToList();
            foreach (var item in examPaperQuesions)
            {
                temQuestionId.Add(item.QuestionID);
            }

            var questions = DbContext.Questions.Where(e => e.CategoryID == categoryId).ToList();
            List<QuestionDto> questionsDto = new List<QuestionDto>();
            foreach (var item in questions)
            {
                int i = 0;
                foreach (var id in temQuestionId)
                {
                    if (item.QuestionID == id)
                    {
                        i++;
                        break;
                    }
                }
                if (i == 0)
                {
                    var QuestionDto = new QuestionDto();
                    QuestionDto.IsActive = item.IsActive;
                    QuestionDto.Content = item.Content;
                    QuestionDto.Image = item.Image;
                    QuestionDto.CreatedBy = item.CreatedBy;
                    QuestionDto.CreatedDate = item.CreatedDate;
                    QuestionDto.ModifiedBy = item.ModifiedBy;
                    QuestionDto.ModifiedDate = item.ModifiedDate;
                    QuestionDto.CategoryID = item.CategoryID;
                    QuestionDto.CategoryName = DbContext.QuestionCategories.SingleOrDefault(q => q.CategoryID == item.CategoryID).Name;
                    QuestionDto.QuestionID = item.QuestionID;
                    if (item.Level == 1)
                    {
                        QuestionDto.LevelName = "Easy";
                    }
                    else if (item.Level == 2)
                    {
                        QuestionDto.LevelName = "Normal";
                    }
                    else
                    {
                        QuestionDto.LevelName = "Hard";
                    }
                    questionsDto.Add(QuestionDto);
                }
            }
            return questionsDto;
        }

        public IEnumerable<QuestionDto> RandomQuestionsByCategoryIdAndExamPaperIdAndNumber(int categoryId, int examPaperId, int number)
        {
            List<QuestionDto> tempQuestionDtos = new List<QuestionDto>();
            tempQuestionDtos = GetQuestionsByQuestionCategoryIdAndExamPaperId(categoryId, examPaperId).ToList();
            if (tempQuestionDtos.Count <= number)
            {
                return tempQuestionDtos;
            }
            else
            {
                List<QuestionDto> QuestionDtos = new List<QuestionDto>();
                int length = tempQuestionDtos.Count();
                List<int> indexs = new List<int>();
                for (int i = 0; i < number; i++)
                {
                    int index = 0;
                    Random rnd = new Random();
                    do
                    {
                        index = rnd.Next(0, length);
                    }
                    while (indexs.Contains(index));
                    indexs.Add(index);
                    QuestionDtos.Add(tempQuestionDtos[index]);
                }
                return QuestionDtos;
            }

        }
        public IEnumerable<Answer> GetAnswersByQuestionId(int? id)
        {
            try
            {
                var listAnswer = DbContext.Answers.Where(x => x.QuestionID == id);
                return listAnswer.ToList();
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return null;//
            }

        }

        public IEnumerable<Question> GetAllQuestions()
        {
            try
            {
                return DbContext.Questions.ToList();
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                throw;
            }

        }

        public IEnumerable<Answer> GetAllAnswers()
        {
            try
            {
                return DbContext.Answers.ToList();
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                throw;
            }

        }

        public IEnumerable<Level> GetAlLevels()
        {
            List<Level> listLevels = new List<Level>();
            listLevels.Add(new Level { LevelId = 1, LevelStep = 1, Name = "Easy" });
            listLevels.Add(new Level { LevelId = 2, LevelStep = 2, Name = "Normal" });
            listLevels.Add(new Level { LevelId = 3, LevelStep = 3, Name = "Hard" });
            return listLevels;
        }

        public string GetNameLevelByQuestionID(int id)
        {
            try
            {
                var name = DbContext.Questions.Find(id);
                if (name.Level == 1)
                {
                    return "Easy";
                }
                if (name.Level == 2)
                {
                    return "Normal";
                }
                if (name.Level == 3)
                {
                    return "Hard";
                }
                else
                {
                    return "None";
                }
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return "None";
            }

        }

        public QuestionDto GetQuestionInQuestionDto(int? id, QuestionFilterModel searchModel)
        {
            var question = GetAllQuestionDtos(searchModel).SingleOrDefault(x => x.QuestionID == id);
            return question;
        }

        public bool CheckQuestionInExamPaperQuesion(int id)
        {
            try
            {
                var question = DbContext.ExamPaperQuesions.SingleOrDefault(x => x.QuestionID == id);
                if (question == null)
                {
                    return false;//Not Exist
                }
                else
                {
                    return true;// Exist
                }
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return true;
            }

        }
    }
}
