using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Data.Infrastructure;
using TestingSystem.Models;

namespace TestingSystem.Data.Repositories
{
    public interface IExamPaperQuestionRepository : IRepository<ExamPaperQuesion>
    {
 
        IEnumerable<ExamPaperQuesion> GetExamPaperQuesionsByExamPaperId(int examPaperId);

        int DeleteExamPaperQuestionByExamPaperIdAndQuestionId(int examPaperId, int questionId);

        int DeleteExamPaperQuestion(int examPaperQuestionId);

        int InsertExamPaperQuestion(int examPaperId, int questionId);

        ExamPaperQuesion GetExamPaperQuesionByExamPaperIdAndQuestionId(int examPaperId, int questionId);
    }
    public class ExamPaperQuestionRepository : RepositoryBase<ExamPaperQuesion>, IExamPaperQuestionRepository
    {


        public ExamPaperQuestionRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<ExamPaperQuesion> GetExamPaperQuesionsByExamPaperId(int examPaperId)
        {
            try
            {
                var examPaperQuestions = DbContext.ExamPaperQuesions.Where(e => e.ExamPaperID == examPaperId);
                return examPaperQuestions;
            }
            catch (Exception e )
            {
                //log.Debug(e.Message);
                return null;
                throw;
            }
            
        }

        public ExamPaperQuesion GetExamPaperQuesionByExamPaperIdAndQuestionId(int examPaperId, int questionId)
        {
            try
            {
                ExamPaperQuesion examPaperQuesion = new ExamPaperQuesion();
                examPaperQuesion = DbContext.ExamPaperQuesions.SingleOrDefault(e => e.ExamPaperID == examPaperId && e.QuestionID == questionId);
                return examPaperQuesion;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                throw;
            }

        }

        public int DeleteExamPaperQuestionByExamPaperIdAndQuestionId(int examPaperId,int questionId)
        {
            try
            {
                ExamPaperQuesion examPaperQuesion = new ExamPaperQuesion();
                examPaperQuesion = GetExamPaperQuesionByExamPaperIdAndQuestionId(examPaperId, questionId);
                DbContext.ExamPaperQuesions.Remove(examPaperQuesion);
                return DbContext.SaveChanges();
            }

            catch (Exception e)
            {
                Console.Write(e);
                throw;
            }
        }

        public int DeleteExamPaperQuestion(int examPaperQuestionId)
        {
            try
            {
                ExamPaperQuesion examPaperQuesion = new ExamPaperQuesion();
                examPaperQuesion = DbContext.ExamPaperQuesions.FirstOrDefault(e => e.ExamPaperQuesionID == examPaperQuestionId);
                DbContext.ExamPaperQuesions.Remove(examPaperQuesion);
                return DbContext.SaveChanges();
            }

            catch (Exception e)
            {
                Console.Write(e);
                throw;
            }
        }

        public int InsertExamPaperQuestion(int examPaperId, int questionId)
        {
            try
            {
                ExamPaperQuesion examPaperQuesion = new ExamPaperQuesion();
                examPaperQuesion.ExamPaperID = examPaperId;
                examPaperQuesion.QuestionID = questionId;
                DbContext.ExamPaperQuesions.Add(examPaperQuesion);
                return DbContext.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

    }
}
