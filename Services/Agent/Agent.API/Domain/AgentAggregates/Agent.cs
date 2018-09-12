﻿namespace Agent.Domain
{    
    using System;
    using System.Collections.Generic;

    public class Agent : AggregateRoot
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string IntegrationEmail { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Company { get; set; }

        public AgentTypeForm AgentTypeForm { get; set; }

        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public Agent()
        {
            //AgentTypeForms = new List<AgentTypeForm>();
        }
    }
}
