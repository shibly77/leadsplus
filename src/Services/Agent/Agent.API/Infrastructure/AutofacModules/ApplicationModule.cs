namespace Agent.Infrastructure.AutofacModules
{
    using Agent.Commands;
    using Autofac;
    using LeadsPlus.BuildingBlocks.EventBus.Abstractions;
    using Microsoft.Extensions.Configuration;
    using System.Reflection;

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
        }
    }
}
