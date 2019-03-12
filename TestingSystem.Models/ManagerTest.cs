namespace TestingSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="ManagerTest" />
    /// </summary>
    public class ManagerTest
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the TestID
        /// </summary>
        [ForeignKey("Tests")]
        public int TestID { get; set; }

        /// <summary>
        /// Gets or sets the Tests
        /// </summary>
        public virtual Test Tests { get; set; }

        /// <summary>
        /// Gets or sets the UserId
        /// </summary>
        [ForeignKey("Users")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the Users
        /// </summary>
        public virtual User Users { get; set; }

        /// <summary>
        /// Gets or sets the Type
        /// </summary>
        [Required]
        public byte Type { get; set; }
    }
}
