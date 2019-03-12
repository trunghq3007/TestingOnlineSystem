namespace TestingSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;

    /// <summary>
    /// Defines the <see cref="User" />
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the UserId
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the RoleId
        /// </summary>
        [Required]
        [ForeignKey("Roles")]
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the UserName
        /// </summary>
        [Required]
        [MaxLength(20)]
        [Remote("_IsAvailableName", "User", AdditionalFields = "OldUserName", HttpMethod = "POST", ErrorMessage = "Đã tồn tại trong dữ liệu")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the Password
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDate
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedDate
        /// </summary>
        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the Status
        /// </summary>
        [Required]
        public byte Status { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Phone
        /// </summary>
        [MaxLength(50)]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the Email
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Remote("_IsAvailableEmail", "User", AdditionalFields = "OldEmail", HttpMethod = "POST", ErrorMessage = "Đã tồn tại trong dữ liệu")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the Address
        /// </summary>
        [MaxLength(200)]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the Avatar
        /// </summary>
        [MaxLength(200)]
        public string Avatar { get; set; }

        /// <summary>
        /// Gets or sets the Note
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets the Roles
        /// </summary>
        public virtual Roles Roles { get; set; }

        /// <summary>
        /// Gets or sets the UserGroups
        /// </summary>
        public virtual ICollection<UserGroup> UserGroups { get; set; }

        /// <summary>
        /// Gets or sets the Candidates
        /// </summary>
        public virtual ICollection<Candidate> Candidates { get; set; }

        /// <summary>
        /// Gets or sets the QuestionCategoriesCreateUser
        /// </summary>
        public virtual ICollection<QuestionCategory> QuestionCategoriesCreateUser { get; set; }

        /// <summary>
        /// Gets or sets the QuestionCategoriesModifiedUser
        /// </summary>
        public virtual ICollection<QuestionCategory> QuestionCategoriesModifiedUser { get; set; }

        /// <summary>
        /// Gets or sets the QuestionCreateUser
        /// </summary>
        public virtual ICollection<Question> QuestionCreateUser { get; set; }

        /// <summary>
        /// Gets or sets the QuestionModifiedUser
        /// </summary>
        public virtual ICollection<Question> QuestionModifiedUser { get; set; }

        /// <summary>
        /// Gets or sets the ExamPapersCreateUser
        /// </summary>
        public virtual ICollection<ExamPaper> ExamPapersCreateUser { get; set; }

        /// <summary>
        /// Gets or sets the ExamPapersModifiedUser
        /// </summary>
        public virtual ICollection<ExamPaper> ExamPapersModifiedUser { get; set; }
    }
}
