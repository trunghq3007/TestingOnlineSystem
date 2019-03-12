namespace TestingSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="Exam" />
    /// </summary>
    public class Exam
    {
        /// <summary>
        /// Gets or sets the ExamID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExamID { get; set; }

        /// <summary>
        /// Gets or sets the ExamName
        /// </summary>
        [Required]
        public string ExamName { get; set; }

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
        /// Gets or sets the Status
        /// </summary>
        [Required]
        public byte Status { get; set; }
    }
}
