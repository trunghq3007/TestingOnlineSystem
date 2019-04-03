using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingSystem.Models
{
    public class ExamTest
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ExamTestID { set; get; }
		
        public int ExamID { set; get; }

        public int TestID { set; get; }

        public virtual Test Test { set; get; }

        public virtual Exam Exam { set; get; }
             
    }
}
