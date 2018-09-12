namespace Contact.Domain
{    
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Events;

    public class Contact : AggregateRoot, IViewModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        private bool _isDraft;

        public static Contact NewDraft()
        {
            var contact = new Contact();
            contact._isDraft = true;

            return contact;
        }

        protected Contact()
        {
            _isDraft = false;
        }

        public Contact(string ownerId, string firstname, string lastname, string email) : this()
        {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;

            CreatedBy = ownerId;
            CreatedDate = DateTime.UtcNow;
            UpdatedDate = DateTime.UtcNow;

            var contactCreatedDomainEvent = new ContactCreatedDomainEvent
            {
                Email = email,
                Firstname = firstname,
                Lastname = lastname,
                CreatedBy = CreatedBy
            };

            this.AddDomainEvent(contactCreatedDomainEvent);
        }
    }
}

