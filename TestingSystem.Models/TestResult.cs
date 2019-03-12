namespace TestingSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="TestResult" />
    /// </summary>
    public class TestResult
    {
        /// <summary>
        /// Gets or sets the TestResultID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TestResultID { get; set; }

        /// <summary>
        /// Gets or sets the CandidatesID
        /// </summary>
        [ForeignKey("Candidates")]
        public int CandidatesID { get; set; }

        /// <summary>
        /// Gets or sets the Candidates
        /// </summary>
        public virtual Candidate Candidates { get; set; }

        /// <summary>
        /// Gets or sets the TestID
        /// </summary>
        [ForeignKey("Tests")]
        public int TestID { get; set; }

        /// <summary>
        /// Gets or sets the Tests
        /// </summary>
        public virtual Test Tests { get; set; }

        /// <summary>
        /// Gets or sets the Score
        /// </summary>
        [Required]
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        [Required]
        public string Description { get; set; }
    }
}
