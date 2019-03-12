namespace TestingSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="Roles" />
    /// </summary>
    public class Roles
    {
        /// <summary>
        /// Gets or sets the RoleId
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the RoleName
        /// </summary>
        [Required]
        public string RoleName { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the User
        /// </summary>
        public virtual ICollection<User> User { get; set; }

        /// <summary>
        /// Gets or sets the RoleActions
        /// </summary>
        public virtual ICollection<RoleAction> RoleActions { get; set; }
    }
}
