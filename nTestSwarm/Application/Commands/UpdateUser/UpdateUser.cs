﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;

namespace nTestSwarm.Application.Commands.UpdateUser
{
    public class UpdateUser:IRequest<User>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }
    }
}