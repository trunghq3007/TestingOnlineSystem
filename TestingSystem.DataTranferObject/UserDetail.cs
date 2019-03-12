namespace TestingSystem.DataTranferObject
{
    using System;
    using System.Collections.Generic;
    using TestingSystem.Models;

    /// <summary>
    /// Defines the <see cref="UserDetail" />
    /// </summary>
    public class UserDetail
    {
        /// <summary>
        /// Gets or sets the UserId
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the RoleId
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the Password
        /// </summary>
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
        public byte Status { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Phone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the Avatar
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Gets or sets the Note
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Defines the Groups
        /// </summary>
        public List<Group> Groups;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDetail"/> class.
        /// </summary>
        public UserDetail()
        {
            Groups = new List<Group>();
        }
    }
}
