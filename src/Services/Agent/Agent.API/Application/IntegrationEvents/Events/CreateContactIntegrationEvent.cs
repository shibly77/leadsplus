﻿namespace Agent.IntegrationEvents
{
    using LeadsPlus.BuildingBlocks.EventBus.Events;
    using MediatR;
    using System;

    public class CreateContactIntegrationEvent : IntegrationEvent
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Source { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
