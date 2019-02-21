using System;
using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Models
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }

        [Required(ErrorMessage = "Is Required")]
        [MaxLength(50,ErrorMessage = ">50")]
        public string GroupName { get; set; }

        [Required(ErrorMessage = "Is Required")]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Is Required")]
        public DateTime UpdatedDate { get; set; }

        public string Description { get; set; }
    }
}
