namespace TestingSystem.DataTranferObject
{
    using System.Collections.Generic;
    using TestingSystem.Models;

    /// <summary>
    /// Defines the <see cref="MatrixRoleAction" />
    /// </summary>
    public class MatrixRoleAction
    {
        /// <summary>
        /// Defines the ListAction
        /// </summary>
        public IEnumerable<Models.Action> ListAction;

        /// <summary>
        /// Defines the ListRoles
        /// </summary>
        public List<Roles> ListRoles;

        /// <summary>
        /// Defines the ListRoleActions
        /// </summary>
        public List<RoleAction> ListRoleActions;

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixRoleAction"/> class.
        /// </summary>
        public MatrixRoleAction()
        {
            ListAction = new List<Models.Action>();
            ListRoles = new List<Roles>();
            ListRoleActions = new List<RoleAction>();
        }
    }
}
