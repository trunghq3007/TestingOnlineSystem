using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestingSystem.Models
{
	public class CandidatesTest
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CandidatesTestID { get; set; }
		public int  TestID { get; set; }
		public int CandidateID { get; set; }

		public Test Test { get; set; }
		public Candidate Candidate { get; set; }
	}
}