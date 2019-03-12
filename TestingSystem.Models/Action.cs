namespace TestingSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;

    /// <summary>
    /// Defines the <see cref="Action" />
    /// </summary>
    public class Action
    {
        /// <summary>
        /// Gets or sets the ActionId
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActionId { get; set; }

        /// <summary>
        /// Gets or sets the ActionName
        /// </summary>
        [Required]
        [Remote("_IsAvailable", "Action", AdditionalFields = "oldName", HttpMethod = "POST", ErrorMessage = "Đã tồn tại trong Cơ sở dữ liệu!")]
        public string ActionName { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the RoleActions
        /// </summary>
        public virtual ICollection<RoleAction> RoleActions { get; set; }
    }
}
