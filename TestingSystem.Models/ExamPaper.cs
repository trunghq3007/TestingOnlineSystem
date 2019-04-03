namespace TestingSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ExamPaper
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExamPaperID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int Time { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        public int NumberOfQuestion { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public virtual User CreatedBys { get; set; }

        public virtual User ModifiedBys { get; set; }


        public ICollection<ExamPaperQuesion> ExamPaperQuesions { get; set; }

        public ICollection<Test> Tests { get; set; }

	}
}
