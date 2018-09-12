namespace Contact.Infrastructure.AutofacModules
{
    using Autofac;
    using Contact.Commands;
    using Contact.Domain;
    using Repositories;
    using LeadsPlus.BuildingBlocks.EventBus.Abstractions;
    using System.Reflection;
    using Contact.Projection.Query;
    using Microsoft.Extensions.Options;
    using Contact.Config;
    using Microsoft.Extensions.Configuration;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public class ApplicationModule : Autofac.Module
    {
        public IConfiguration setting;

        public ApplicationModule(IConfiguration setting)
        {
            this.setting = setting;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ContactCommandHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

            builder.RegisterType<QueryExecutor>().As<IQueryExecutor>().SingleInstance();

            builder.RegisterAssemblyTypes(typeof(GetContactQueryHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IQueryHandler<,>));

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));

            builder.Register(c => { return ViewModelStoreFactory.Create<Contact>(setting["DatabaseConnectionString"], setting["DatabaseName"]); }).SingleInstance();

            //builder.Register(c => { return ViewModelStoreFactory.Create<Contact>(this.configuration["ProjectionDatabaseConnectionString"], "ContactProjections"); }).SingleInstance();

            //for view models
            //builder.Register(c => { return ViewModelStoreFactory.Create<ContactViewModel>(this.configuration["ProjectionDatabaseConnectionString"], "ContactProjections"); }).SingleInstance();

        }
    }
}
