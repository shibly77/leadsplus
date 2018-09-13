namespace LeadsPlus.Core.Extension
{
    using MediatR;
    using System.Linq;
    using System.Threading.Tasks;

    public static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, AggregateRoot entity)
        {
            var domainEvents = entity.DomainEvents
                .ToList();

            entity.ClearDomainEvents();

            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await mediator.Publish(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
