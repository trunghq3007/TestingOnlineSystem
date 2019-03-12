namespace TestingSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="Answer" />
    /// </summary>
    public class Answer
    {
        /// <summary>
        /// Gets or sets the AnswerID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnswerID { get; set; }

        /// <summary>
        /// Gets or sets the AnswerContent
        /// </summary>
        [Required]
        public string AnswerContent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsCorrect
        /// </summary>
        [Required]
        public bool IsCorrect { get; set; }

        /// <summary>
        /// Gets or sets the QuestionID
        /// </summary>
        [Required]

        public int QuestionID { get; set; }

        /// <summary>
        /// Gets or sets the Question
        /// </summary>
        public virtual Question Question { get; set; }
    }
}
