namespace TestingSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="ExamPaper" />
    /// </summary>
    public class ExamPaper
    {
        /// <summary>
        /// Gets or sets the ExamPaperID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExamPaperID { get; set; }

        /// <summary>
        /// Gets or sets the Title
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Time
        /// </summary>
        [Required]
        public int Time { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Status
        /// </summary>
        [Required]
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the NumberOfQuestion
        /// </summary>
        [Required]
        public int NumberOfQuestion { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsActive
        /// </summary>
        [Required]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the CreatedBy
        /// </summary>
        [Required]
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDate
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the ModifiedBy
        /// </summary>
        public int? ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the ModifiedDate
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the CreatedBys
        /// </summary>
        public virtual User CreatedBys { get; set; }

        /// <summary>
        /// Gets or sets the ModifiedBys
        /// </summary>
        public virtual User ModifiedBys { get; set; }

        /// <summary>
        /// Gets or sets the ExamPaperQuesions
        /// </summary>
        public ICollection<ExamPaperQuesion> ExamPaperQuesions { get; set; }
    }
}
