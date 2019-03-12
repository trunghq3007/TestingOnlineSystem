namespace TestingSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="UserGroup" />
    /// </summary>
    public class UserGroup
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the UserId
        /// </summary>
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the GroupId
        /// </summary>
        [Required]
        [ForeignKey("Group")]
        public int GroupId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsManager
        /// </summary>
        [Required]
        public bool IsManager { get; set; }

        /// <summary>
        /// Gets or sets the Group
        /// </summary>
        public virtual Group Group { get; set; }

        /// <summary>
        /// Gets or sets the User
        /// </summary>
        public virtual User User { get; set; }
    }
}
