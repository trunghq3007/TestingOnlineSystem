using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using TestingSystem.Data.Infrastructure;
using TestingSystem.Mappings;
using TestingSystem.Models;
using TestingSystem.Sevice;

namespace TestingSystem.App_Start
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
            //Configure AutoMapper
            AutoMapperConfiguration.Configure();
        }

        private static void SetAutofacContainer()
        {
            //var builder = new ContainerBuilder();
            //builder.RegisterControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            //builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            //// Repositories
            //builder.RegisterAssemblyTypes(typeof(QuestionCategory).Assembly)
            //    .Where(t => t.Name.EndsWith("Repository"))
            //    .AsImplementedInterfaces().InstancePerRequest();
            //// Services
            //builder.RegisterAssemblyTypes(typeof(QuestionService).Assembly)
            //   .Where(t => t.Name.EndsWith("Service"))
            //   .AsImplementedInterfaces().InstancePerRequest();






            //IContainer container = builder.Build();
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}