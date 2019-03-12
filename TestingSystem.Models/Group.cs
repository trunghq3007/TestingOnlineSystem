namespace TestingSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="Group" />
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Gets or sets the GroupId
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GroupId { get; set; }

        /// <summary>
        /// Gets or sets the GroupName
        /// </summary>
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9 \.\&\'\-]+)$", ErrorMessage = "Invalid Group Name")]
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDate
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedDate
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the UserGroups
        /// </summary>
        public ICollection<UserGroup> UserGroups { get; set; }
    }
}
