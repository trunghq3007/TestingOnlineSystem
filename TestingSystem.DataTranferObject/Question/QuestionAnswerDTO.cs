using System.Collections.Generic;
using TestingSystem.Models;
namespace TestingSystem.DataTranferObject.Question
{
    public class QuestionAnswerDTO
    {
        public Models.Question Question { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
