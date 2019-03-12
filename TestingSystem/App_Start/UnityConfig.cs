using System;
using System.Linq;
using TestingSystem.Data.Infrastructure;
using TestingSystem.Data.Repositories;
using TestingSystem.Models;
using TestingSystem.Sevice;
using Unity;

namespace TestingSystem
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IDbFactory, DbFactory>();



            // Team02_mapping
            container.RegisterType<IActionRepository, ActionRepository>();
            container.RegisterType<IActionService, ActionService>();

            container.RegisterType<IGroupRepository, GroupRepository>();
            container.RegisterType<IGroupService, GroupService>();

            container.RegisterType<IRoleActionRepository, RoleActionRepository>();
            container.RegisterType<IRoleActionService, RoleActionService>();

            container.RegisterType<IRoleRepository, RoleRepository>();
            container.RegisterType<IRoleService, RoleService>();

            container.RegisterType<IUserGroupRepository, UserGroupRepository>();
            container.RegisterType<IUserGroupService, UserGroupService>();

            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IUserService, UserService>();

            ///
            ///
            ///
            ///
            ///
            ///
            container.RegisterType<IQuestionRepository, QuestionRepository>();
            container.RegisterType<IQuestionService, QuestionService>();
            container.RegisterType<IQuestionCategoryRepository, QuestionCategoryRepository>();
            container.RegisterType<IQuestionCategorySevice, QuestionCategorySevice>();

            container.RegisterType<IAnswerRepository, AnswerRepository>();
            container.RegisterType<IAnswerService, AnswerService>();
            container.RegisterType<IExamPaperRepository, ExamPaperRepository>();
            container.RegisterType<IExamPaperService, ExamPaperService>();
            container.RegisterType<IExamPaperQuestionService, ExamPaperQuestionService>();
            container.RegisterType<IExamPaperQuestionRepository, ExamPaperQuestionRepository>();
        }
    }
}