namespace TrueSnow.Web.Config
{
    using System.Data.Entity;
    using System.Reflection;
    using System.Web.Mvc;

    using Autofac;
    using Autofac.Integration.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Controllers;
    using Data;
    using Data.Common;
    using Data.Models;
    using Infrastructure;
    using Services.Data;
    using Services.Web;
    using Services.Web.Contracts;

    public static class AutofacConfig
    {
        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterSource(new ViewRegistrationSource());
            builder.RegisterFilterProvider();
            RegisterServices(builder);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.Register(x => new TrueSnowDbContext())
                .As<DbContext>()
                .InstancePerRequest();
            builder.Register(x => new HttpCacheService())
                .As<ICacheService>()
                .InstancePerRequest();
            builder.Register(x => new IdentifierProvider())
                .As<IIdentifierProvider>()
                .InstancePerRequest();
            builder.Register(x => new HtmlSanitizerAdapter())
                .As<ISanitizer>()
                .InstancePerRequest();

            builder.RegisterType<UserStore<User>>()
                .As<IUserStore<User>>();
            builder.RegisterType<UserManager<User>>();

            var servicesAssembly = Assembly.GetAssembly(typeof(FilesService));
            builder.RegisterAssemblyTypes(servicesAssembly).AsImplementedInterfaces();

            builder.RegisterGeneric(typeof(DbRepository<>))
                .As(typeof(IDbRepository<>))
                .InstancePerRequest();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AssignableTo<BaseController>().PropertiesAutowired();
        }
    }
}