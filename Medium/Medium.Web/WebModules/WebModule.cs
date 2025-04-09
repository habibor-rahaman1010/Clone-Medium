using Autofac;
using Medium.Domain;
using Medium.Domain.RepositoriesInterface;
using Medium.Domain.UnitOfWorkInterface;
using Medium.Infrastructure;
using Medium.Infrastructure.Repositories;
using Medium.Infrastructure.UnitOfWork;

namespace Medium.Web.WebModules
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {     
            builder.RegisterType<MediumUnitOfWork>().As<IMediumUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<ApplicationTime>().As<IApplicationTime>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
