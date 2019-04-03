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
    public interface IExamPaperExamRepository : IRepository<ExamTest>
    {

        List<int> GetIdExamByIdExamPaper(int examPaperId);
        
    }
    public class ExamPaperExamRepository : RepositoryBase<ExamTest>, IExamPaperExamRepository
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ExamPaperExamRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public List<int> GetIdExamByIdExamPaper(int examPaperId)
        {
            try
            {
                // query all usergroup by idGroup
                var listExamPaper = this.DbContext.ExamTests.Where(c => c.TestID == examPaperId).ToList();
                List<int> listIdExam = new List<int>();
                for(int i = 0; i < listExamPaper.Count(); i++)
                {
                    listIdExam.Add(listExamPaper[i].ExamID);
                }
                return listIdExam;
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return null;
            }
        }
    }
}
