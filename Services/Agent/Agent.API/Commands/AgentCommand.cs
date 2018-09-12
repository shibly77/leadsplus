using MediatR;

namespace Agent.Command
{
    public class CreateAgentCommand : Command, IRequest<bool>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
    }

    public class UpdateAgentCommand : Command, IRequest<bool>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
    }

    public class DeleteAgentCommand : Command, IRequest<bool>
    {
        
    }

    public class CreateAgentIntigrationEmailAccountCommand : Command, IRequest<bool>
    {
        public string AgentEmail { get; set; }
    }

    public class CreateAgentTypeFormAccountCommand : Command, IRequest<bool>
    {
        
    }
}
