using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using UseAutofac.Services;

namespace UseAutofac.App_Start
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            ContainerBuilder builder = new ContainerBuilder();
            RegisterDependencies(builder);
            IContainer container = builder.Build();

            AutofacWebApiDependencyResolver resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        private static void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register additional services
            builder.RegisterType<MyService>()
                   .As<IMyService>()
                   .InstancePerRequest();
        }
    }
}