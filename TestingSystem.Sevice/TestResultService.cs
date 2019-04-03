using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Data.Repositories;
using TestingSystem.DataTranferObject;
using TestingSystem.Models;

namespace TestingSystem.Sevice
{
    public interface ITestResultService
    {
        /// <summary>
        /// AddTestResult
        /// </summary>
        /// <param name="testResult"></param>
        /// <returns></returns>
        int AddTestResult(TestResult testResult);
        /// <summary>
        /// GetALl
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        IEnumerable<TestResult> GetALl(int Id);
        /// <summary>
        /// GetQuestionByCount
        /// </summary>
        /// <param name="countQ"></param>
        /// <returns></returns>
        IEnumerable<TestResult> GetQuestionByCount(int countQ);
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
    public class TestResultService : ITestResultService
    {
        private readonly ITestResultRepository testResultRepository;
        /// <summary>
        /// TestResultService
        /// </summary>
        /// <param name="testResultRepository"></param>
        public TestResultService(ITestResultRepository testResultRepository)
        {
            this.testResultRepository = testResultRepository;
        }
        /// <summary>
        /// AddTestResult
        /// </summary>
        /// <param name="testResult"></param>
        /// <returns></returns>
        public int AddTestResult(TestResult testResult)
        {
            return testResultRepository.AddTestResult(testResult);
        }
        /// <summary>
        /// CountUsed
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int CountUsed(int Id)
        {
            return testResultRepository.CountUsed(Id);
        }
        /// <summary>
        /// GetALl
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IEnumerable<TestResult> GetALl(int Id)
        {
            return testResultRepository.GetALl(Id);
        }
        /// <summary>
        /// GetQuestionByCount
        /// </summary>
        /// <param name="countQ"></param>
        /// <returns></returns>
        public IEnumerable<TestResult> GetQuestionByCount(int countQ)
        {
            return testResultRepository.GetQuestionByCount(countQ);
        }
        /// <summary>
        /// ListAllQuestionIdAndAnswerIdByTestIdChecked
        /// </summary>
        /// <param name="testId"></param>
        /// <param name="turn"></param>
        /// <returns></returns>
        public IEnumerable<ResultCheckId> ListAllQuestionIdAndAnswerIdByTestIdChecked(int testId, int turn)
        {
            return testResultRepository.ListAllQuestionIdAndAnswerIdByTestIdChecked(testId, turn);
        }
        /// <summary>
        /// ListAllTestByDedicateId
        /// </summary>
        /// <param name="dedicateId"></param>
        /// <returns></returns>
        public IEnumerable<ReviewTestResult> ListAllTestByDedicateId(int dedicateId)
        {
            return testResultRepository.ListAllTestByDedicateId(dedicateId);
        }
        /// <summary>
        /// ReturnTurn
        /// </summary>
        /// <param name="testId"></param>
        /// <param name="dateTest"></param>
        /// <returns></returns>
        public int ReturnTurn(int testId, DateTime dateTest)
        {
            return testResultRepository.ReturnTurn(testId, dateTest);
        }
    }
}
