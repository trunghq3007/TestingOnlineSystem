namespace TestingSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="QuestionCategory" />
    /// </summary>
    public class QuestionCategory
    {
        /// <summary>
        /// Gets or sets the CategoryID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        [Required]
        public string Name { get; set; }

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

        //[ForeignKey("ModifiedBys"), Column(Order = 1)]
        /// <summary>
        /// Gets or sets the ModifiedBys
        /// </summary>
        public virtual User ModifiedBys { get; set; }

        /// <summary>
        /// Gets or sets the CreatedBys
        /// </summary>
        public virtual User CreatedBys { get; set; }

        /// <summary>
        /// Gets or sets the Questions
        /// </summary>
        public virtual ICollection<Question> Questions { get; set; }
    }
}
