using System.Collections.Generic;
using System.Linq;
using TestingSystem.Data.Infrastructure;
using TestingSystem.DataTranferObject;
using TestingSystem.Models;

namespace TestingSystem.Data.Repositories
{
    public interface IAnswerRepository : IRepository<Answer>
    {
        List<Answer> GetAnswersByQuestionID(int? id);
        bool UpdateAnswer(Answer answer);
        int AddAnswer(Answer answer);
        int DeleteAnswer(int id);
        int DelteAnswerbyQuestionID(int id);
        Answer GetAnswerCorrect(int idAnswer);
        List<int> GetListIdAnswerCorrectByIdQuestion(int idQuestion);
        IEnumerable<ResultCheckIdTrue> GetAllQuestionIdandAnswerIdByExampaperId(int idExamPaper);
    }

    public class AnswerRepository : RepositoryBase<Answer>, IAnswerRepository
    {
        public AnswerRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public int AddAnswer(Answer answer)
        {
            {
                DbContext.Answers.Add(answer);
                this.DbContext.SaveChanges();
                return 1;
            }
        }
        public int DeleteAnswer(int id)
        {
            var answer = DbContext.Answers.Find(id);
            if (answer != null)
            {
                this.DbContext.Answers.Remove(answer);
                return DbContext.SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public int DelteAnswerbyQuestionID(int id)
        {
            var listAnswer = DbContext.Answers.Where(x => x.QuestionID == id);
            if (listAnswer != null)
            {
                foreach (var item in listAnswer)
                {
                    DbContext.Answers.Remove(item);
                }
                return DbContext.SaveChanges();
            }
            else
                return 0;
        }

        public List<Answer> GetAnswersByQuestionID(int? id)
        {
            var listAnswer = DbContext.Answers.Where(x => x.QuestionID == id).ToList();
            return listAnswer;
        }

        public bool UpdateAnswer(Answer answer)
        {
            var objQuestion = this.DbContext.Answers.Find(answer.AnswerID);
            if (objQuestion != null)
            {
                objQuestion.AnswerContent = answer.AnswerContent;
                objQuestion.IsCorrect = answer.IsCorrect;
                objQuestion.QuestionID = answer.QuestionID;
                this.DbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public Answer GetAnswerCorrect(int idAnswer)
        {
            var obj = this.DbContext.Answers.Where(s => s.IsCorrect == true && s.AnswerID == idAnswer).FirstOrDefault();
            return obj;
        }

        public List<int> GetListIdAnswerCorrectByIdQuestion(int idQuestion)
        {
            var listAnswer = this.DbContext.Answers.Where(s => s.QuestionID == idQuestion && s.IsCorrect == true).ToList();
            List<int> listIdAnswer = new List<int>();
            foreach (var item in listAnswer)
            {
                listIdAnswer.Add(item.AnswerID);
            }

            return listIdAnswer;
        }

        public IEnumerable<ResultCheckIdTrue> GetAllQuestionIdandAnswerIdByExampaperId(int idExamPaper)
        {
            var listQuestionByExamPaperId = this.DbContext.ExamPaperQuesions.Where(s => s.ExamPaperID == idExamPaper).ToList();
            var listAnswer = this.DbContext.Answers.ToList();
            var listAnswerByQuestionId = new List<Answer>();
            List<ResultCheckIdTrue> listAnswerQuestion = new List<ResultCheckIdTrue>();
            foreach (var item in listQuestionByExamPaperId)
            {
                foreach (var item2 in listAnswer)
                {
                    if (item.QuestionID == item2.QuestionID)
                    {
                        ResultCheckIdTrue obj = new ResultCheckIdTrue();
                        obj.AnswerId = item2.AnswerID;
                        obj.QuestionId = item2.QuestionID;
                        obj.IsTrue = item2.IsCorrect;
                        listAnswerQuestion.Add(obj);
                    }
                }
            }
            return listAnswerQuestion;
        }
    }
}
