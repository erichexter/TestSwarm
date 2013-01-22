using System;

namespace nTestSwarm.Application.Domain
{
    public class User : Entity
    {
        public string Username { get; set; }
        public virtual string Name { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string PasswordSalt { get; set; }

        public Guid ApiKey { get; set; }
    }
}