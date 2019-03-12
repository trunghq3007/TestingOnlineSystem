namespace TestingSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="ExamPaperQuesion" />
    /// </summary>
    public class ExamPaperQuesion
    {
        /// <summary>
        /// Gets or sets the ExamPaperQuesionID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExamPaperQuesionID { get; set; }

        /// <summary>
        /// Gets or sets the QuestionID
        /// </summary>
        [Required]
        public int QuestionID { get; set; }

        /// <summary>
        /// Gets or sets the ExamPaperID
        /// </summary>
        [Required]
        public int ExamPaperID { get; set; }

        /// <summary>
        /// Gets or sets the Question
        /// </summary>
        public virtual Question Question { get; set; }

        /// <summary>
        /// Gets or sets the ExamPaper
        /// </summary>
        public virtual ExamPaper ExamPaper { get; set; }
    }
}
