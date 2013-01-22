using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;

namespace nTestSwarm.Application.Commands.UpdateUser
{
    public class UpdateUserHandler:IHandler<UpdateUser,User>

    {
        private readonly IDataBase _dataBase;
        private readonly ICryptographer _cryptographer;

        public UpdateUserHandler(IDataBase dataBase,ICryptographer cryptographer)
        {
            _dataBase = dataBase;
            _cryptographer = cryptographer;
        }

        public User Handle(UpdateUser message)
        {
            
            var user = _dataBase.Find<User>(message.Id) ?? new User();
            user.Name = message.Name;
            user.EmailAddress = message.EmailAddress;
            user.PasswordSalt = _cryptographer.CreateSalt();
            user.PasswordHash = _cryptographer.GetPasswordHash(message.Password,
                                                               user.PasswordSalt);
            user.Username = message.Username;

            if(user.Id==default(long))
            {
                user.ApiKey = Guid.NewGuid();
            }
            _dataBase.Add(user);
            _dataBase.SaveChanges();

            return user;// new ReturnValue { Type = typeof(User), Value = user };

        }
    }
}