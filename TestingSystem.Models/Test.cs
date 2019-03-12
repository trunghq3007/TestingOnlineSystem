namespace TestingSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="Test" />
    /// </summary>
    public class Test
    {
        /// <summary>
        /// Gets or sets the TestID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TestID { get; set; }

        /// <summary>
        /// Gets or sets the ExamPaperID
        /// </summary>
        [ForeignKey("ExamPapers")]
        public int ExamPaperID { get; set; }

        /// <summary>
        /// Gets or sets the ExamPapers
        /// </summary>
        public virtual ExamPaper ExamPapers { get; set; }

        /// <summary>
        /// Gets or sets the ExamID
        /// </summary>
        [ForeignKey("Exams")]
        public int ExamID { get; set; }

        /// <summary>
        /// Gets or sets the Exams
        /// </summary>
        public virtual Exam Exams { get; set; }

        /// <summary>
        /// Gets or sets the TestName
        /// </summary>
        [Required]
        public string TestName { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the CreateDate
        /// </summary>
        [Required]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the StartDate
        /// </summary>
        [Required]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the EndDate
        /// </summary>
        [Required]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the PassingScore
        /// </summary>
        [Required]
        public string PassingScore { get; set; }

        /// <summary>
        /// Gets or sets the Status
        /// </summary>
        [Required]
        public byte Status { get; set; }
    }
}
