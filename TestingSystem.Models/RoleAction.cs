namespace TestingSystem.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="RoleAction" />
    /// </summary>
    public class RoleAction
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the RoleId
        /// </summary>
        [Required]
        [ForeignKey("Roles")]
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the ActionId
        /// </summary>
        [Required]
        [ForeignKey("Action")]
        public int ActionId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsTrue
        /// </summary>
        [Required]
        [DefaultValue(false)]
        public bool IsTrue { get; set; }

        /// <summary>
        /// Gets or sets the Roles
        /// </summary>
        public virtual Roles Roles { get; set; }

        /// <summary>
        /// Gets or sets the Action
        /// </summary>
        public virtual Action Action { get; set; }
    }
}
