using System;

namespace LeadsPlus.BuildingBlocks.EventBus.Events
{
    public class IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public Guid Id  { get; }
        public DateTime CreationDate { get; }
        public string AggregateId { get; set; }
    }
}
