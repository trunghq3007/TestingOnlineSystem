using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingSystem.DataTranferObject.Question
{
    public class QuestionDto
    {
        public int QuestionID { get; set; }

        public string Content { get; set; }

        public string Image { get; set; }

        public int Level { get; set; }
        public string LevelName { get; set; }

        public int CategoryID { get; set; }
  
        public string CategoryName { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }
        public string CreatedName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ModifiedBy { get; set; }
        public string ModifiedName { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ExamPaperQuestionID { get; set; }
    }
}
