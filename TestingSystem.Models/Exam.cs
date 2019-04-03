namespace TestingSystem.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Exam
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ExamID { get; set; }

		public string ExamCode { get; set; }

		[Required]
		public string ExamName { get; set; }

		public string Description { get; set; }

		[Required]
		public DateTime CreateDate { get; set; }

		[Required]
		public DateTime StartDate { get; set; }

		[Required]
		public DateTime EndDate { get; set; }

		[Required]
		public byte Status { get; set; }
		public ICollection<ExamTest> ExamTests { set; get; }
	}
}
