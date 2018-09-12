﻿namespace Agent.IntegrationEvents
{
    using LeadsPlus.BuildingBlocks.EventBus.Events;

    public class EmailNeedsToBeSentIntegrationEvent : IntegrationEvent
    {
        public string[] To { get; set; }
        public string[] Cc { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }
        public string ReplyTo { get; set; }

        public EmailNeedsToBeSentIntegrationEvent()
        {

        }
    }
}

