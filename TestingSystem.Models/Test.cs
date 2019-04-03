using System.Collections;

namespace TestingSystem.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Test
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int TestID { get; set; }

		[ForeignKey("ExamPapers")]
		public int ExamPaperID { get; set; }

		[Required]
		public string TestName { get; set; }

		public bool IsActive { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		public string Description { get; set; }

		public int CreatedBy { get; set; }
		public int? ModifiedBy { get; set; }

		[Required]
		public DateTime CreateDate { get; set; }

		public DateTime? ModifiedDate { get; set; }

		[Required]
		public string PassingScore { get; set; }

		[Required]
		public bool Status { get; set; }

		public virtual ICollection<TestResult> TestResults { get; set; }
		public virtual ICollection<ExamTest> ExamTests { get; set; }
		public virtual ExamPaper ExamPapers { get; set; }
		public virtual ICollection<CandidatesTest> CandidatesTests { get; set; }
	}
}
