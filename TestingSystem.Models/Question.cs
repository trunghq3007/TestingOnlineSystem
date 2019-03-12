namespace TestingSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="Question" />
    /// </summary>
    public class Question
    {
        /// <summary>
        /// Gets or sets the QuestionID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionID { get; set; }

        /// <summary>
        /// Gets or sets the Content
        /// </summary>
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the Image
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the Level
        /// </summary>
        [Required]
        public int Level { get; set; }

        /// <summary>
        /// Gets or sets the CategoryID
        /// </summary>
        [Required]
        public int CategoryID { get; set; }

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
        /// Gets or sets the CreaterUser
        /// </summary>
        public virtual User CreaterUser { get; set; }

        /// <summary>
        /// Gets or sets the ModifiedUser
        /// </summary>
        public virtual User ModifiedUser { get; set; }

        /// <summary>
        /// Gets or sets the Answers
        /// </summary>
        public virtual ICollection<Answer> Answers { get; set; }

        /// <summary>
        /// Gets or sets the QuestionCategory
        /// </summary>
        public virtual QuestionCategory QuestionCategory { get; set; }

        /// <summary>
        /// Gets or sets the ExamPaperQuesions
        /// </summary>
        public ICollection<ExamPaperQuesion> ExamPaperQuesions { get; set; }
    }
}
