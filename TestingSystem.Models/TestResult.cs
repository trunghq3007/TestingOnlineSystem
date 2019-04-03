using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestingSystem.Models
{
	public class TestResult
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int TestResultID { get; set; }
		[Required]
		public int TestID { get; set; }
		[Required]
		public int CandidateID { get; set; }
		[Required]
		public string TestName { get; set; }
		public string Description { get; set; }
		[Required]
		public DateTime CreatedDate { get; set; }
		[Required]
		public int Score { get; set; }

		public int Turns { get; set; }
		public int QuestionID { get; set; }
		public int AnswerID { get; set; }

		public virtual Candidate Candidate { get; set; }
		public virtual Test Test { get; set; }
	}
}
