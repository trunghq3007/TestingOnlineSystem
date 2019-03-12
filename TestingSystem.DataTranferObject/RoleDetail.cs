namespace TestingSystem.DataTranferObject
{
    using System.Collections.Generic;
    using Action = TestingSystem.Models.Action;
    using Roles = TestingSystem.Models.Roles;

    /// <summary>
    /// Defines the <see cref="RoleDetail" />
    /// </summary>
    public class RoleDetail
    {
        /// <summary>
        /// Gets or sets the ListAction
        /// </summary>
        public List<Action> ListAction { get; set; }

        /// <summary>
        /// Gets or sets the Role
        /// </summary>
        public Roles Role { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleDetail"/> class.
        /// </summary>
        public RoleDetail()
        {
            ListAction = new List<Action>();
        }
    }
}
