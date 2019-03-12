namespace TestingSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="Candidate" />
    /// </summary>
    public class Candidate
    {
        /// <summary>
        /// Gets or sets the CandidatesID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CandidatesID { get; set; }

        /// <summary>
        /// Gets or sets the UserId
        /// </summary>
        [ForeignKey("Users")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the Users
        /// </summary>
        public virtual User Users { get; set; }
    }
}
