using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Data.Infrastructure;
using TestingSystem.Data.Repositories;

namespace TestingSystem.Sevice
{
    public interface IExamPaperExamService
    {
        List<int> GetIdExamByIdExamPaper(int examPaperId);

    }
    public class ExamPaperExamService : IExamPaperExamService
    {
        private readonly IExamPaperExamRepository examPaperExamRepository;
        private readonly IUnitOfWork unitOfWork;

        public ExamPaperExamService(IExamPaperExamRepository examPaperExamRepository, IUnitOfWork unitOfWork)
        {
            this.examPaperExamRepository = examPaperExamRepository;
            this.unitOfWork = unitOfWork;
        }
        public List<int> GetIdExamByIdExamPaper(int examPaperId)
        {
            return this.examPaperExamRepository.GetIdExamByIdExamPaper(examPaperId);
        }
    }
}
