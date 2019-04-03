using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Data.Infrastructure;
using TestingSystem.DataTranferObject;
using TestingSystem.Models;

namespace TestingSystem.Data.Repositories
{
    public interface ITestResultRepository : IRepository<TestResult>
    {
        /// <summary>
        /// AddTestResult
        /// </summary>
        /// <param name="testResult"></param>
        /// <returns></returns>
        int AddTestResult(TestResult testResult);
        /// <summary>
        /// GetQuestionByCount
        /// </summary>
        /// <param name="countQ"></param>
        /// <returns></returns>
        IEnumerable<TestResult> GetQuestionByCount(int countQ);
        /// <summary>
        /// GetALl
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        IEnumerable<TestResult> GetALl(int Id);
        /// <summary>
        /// ReturnTurn
        /// </summary>
        /// <param name="testId"></param>
        /// <param name="dateTest"></param>
        /// <returns></returns>
        int ReturnTurn(int testId, DateTime dateTest);
        /// <summary>
        /// ListAllTestByDedicateId
        /// </summary>
        /// <param name="dedicateId"></param>
        /// <returns></returns>
        IEnumerable<ReviewTestResult> ListAllTestByDedicateId(int dedicateId);
        /// <summary>
        /// ListAllQuestionIdAndAnswerIdByTestIdChecked
        /// </summary>
        /// <param name="testId"></param>
        /// <param name="turn"></param>
        /// <returns></returns>
        IEnumerable<ResultCheckId> ListAllQuestionIdAndAnswerIdByTestIdChecked(int testId, int turn);
        /// <summary>
        /// CountUsed
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        int CountUsed(int Id);
    }
    public class TestResultRepository : RepositoryBase<TestResult>, ITestResultRepository
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public TestResultRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
        /// <summary>
        /// AddTestResult
        /// </summary>
        /// <param name="testResult"></param>
        /// <returns></returns>
        public int AddTestResult(TestResult testResult)
        {
            try
            {
                DbContext.TestResults.Add(testResult);
                DbContext.SaveChanges();
                return testResult.TestResultID;
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return 0;
            }
        }
        /// <summary>
        /// GetQuestionByCount
        /// </summary>
        /// <param name="countQ"></param>
        /// <returns></returns>
        public IEnumerable<TestResult> GetQuestionByCount(int countQ)
        {
            var listQ = DbContext.TestResults.OrderByDescending(s => s.TestResultID).Take(countQ).ToList();
            return listQ;
        }
        /// <summary>
        /// GetALl
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IEnumerable<TestResult> GetALl(int Id)
        {
            var model = DbContext.TestResults.Where(x => x.TestID == Id).GroupBy(x => new { x.TestID, x.Turns }).Select(x => x.FirstOrDefault()).ToList();
            return model.Distinct().AsEnumerable();
        }
        /// <summary>
        /// ListAllTestByDedicateId
        /// </summary>
        /// <param name="dedicateId"></param>
        /// <returns></returns>
        public IEnumerable<ReviewTestResult> ListAllTestByDedicateId(int dedicateId)
        {
            var listAllTestResult = this.DbContext.TestResults.ToList();
            var listAllTestResultDTO = new List<ReviewTestResult>();
            int i = 0;
            foreach (var item in listAllTestResult)
            {
                if (i != item.Turns)
                {
                    ReviewTestResult obj = new ReviewTestResult();
                    obj.TestId = item.TestID;
                    obj.TestName = item.TestName;
                    obj.numRank = item.Turns;
                    obj.dateTest = item.CreatedDate;
                    i = item.Turns;
                    listAllTestResultDTO.Add(obj);
                }
            }
            return listAllTestResultDTO;
        }
        /// <summary>
        /// ReturnTurn
        /// </summary>
        /// <param name="testId"></param>
        /// <param name="dateTest"></param>
        /// <returns></returns>
        public int ReturnTurn(int testId, DateTime dateTest)
        {
            var item = this.DbContext.TestResults.Where(s => s.TestID == testId && s.CreatedDate.Month == dateTest.Month && s.CreatedDate.Year == dateTest.Year && s.CreatedDate.Day == dateTest.Day).OrderByDescending(s => s.CreatedDate).Take(1).FirstOrDefault();
            if (item != null)
                return item.Turns;
            else return 0;
        }

        /// <summary>
        /// ListAllQuestionIdAndAnswerIdByTestIdChecked
        /// </summary>
        /// <param name="testId"></param>
        /// <param name="turn"></param>
        /// <returns></returns>
        public IEnumerable<ResultCheckId> ListAllQuestionIdAndAnswerIdByTestIdChecked(int testId, int turn)
        {
            var list = this.DbContext.TestResults.Where(s => s.TestID == testId && s.Turns == turn).ToList();
            List<ResultCheckId> listResultCheckId = new List<ResultCheckId>();
            foreach (var item in list)
            {
                ResultCheckId obj = new ResultCheckId();
                obj.QuestionId = item.QuestionID;
                obj.AnswerId = item.AnswerID;
                listResultCheckId.Add(obj);
            }
            return listResultCheckId;
        }

        /// <summary>
        /// CountUsed
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int CountUsed(int Id)
        {
            var count = DbContext.TestResults.Where(x => x.TestID == Id).GroupBy(x => new { x.CandidateID}).ToList().Distinct().Count();
            return count;
        }
    }
}
