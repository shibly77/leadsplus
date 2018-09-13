namespace Agent.Infrastructure.AutofacModules
{
    using Agent.Commands;
    using Autofac;
    using LeadsPlus.BuildingBlocks.EventBus.Abstractions;
    using LeadsPlus.Core.Query;
    using Microsoft.Extensions.Configuration;
    using System.Reflection;
    using Agent.Domain.Query;
    using LeadsPlus.Core.Repositories;
    using LeadsPlus.Core;
    using System;

    public class ApplicationModule : Autofac.Module
    {
        public IConfiguration setting;

        public ApplicationModule(IConfiguration setting)
        {
            this.setting = setting;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(AgentCommandHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));

            builder.RegisterAssemblyTypes(typeof(GetAgentQueryHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IQueryHandler<,>));

            //builder.Register(c =>
            //{
            //    var context = c.Resolve<IComponentContext>();

            //    Func<IQueryExecutor> factory = () =>
            //    {
            //        return new QueryExecutor(context);
            //    };
            //    return factory;
            //}).As<Func<IQueryExecutor>>().SingleInstance();

            builder.RegisterType<QueryExecutor>()
                .As<IQueryExecutor>()
                .InstancePerLifetimeScope();

            builder.Register(c => { return ViewModelStoreFactory.Create<Domain.Agent>(setting["DatabaseConnectionString"], setting["DatabaseName"]); }).SingleInstance();
            
        }
    }
}
