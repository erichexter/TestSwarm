using System.Linq;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Services;

namespace nTestSwarm.Application.Commands.ValidateLogin
{
    public class ValidateLoginHandler:IHandler<ValidateLoginMessage,bool>
    {
        private readonly IDataBase _dataBase;
        private readonly ICryptographer _cryptographer;

        public ValidateLoginHandler(IDataBase dataBase,ICryptographer cryptographer)
        {
            _dataBase = dataBase;
            _cryptographer = cryptographer;
        }

        public bool Handle(ValidateLoginMessage request)
        {
            var user = _dataBase.All<User>().FirstOrDefault(u => u.Username == request.Username);
            if (user == null)
                return false;
            return user.PasswordHash == _cryptographer.GetPasswordHash(request.Password, user.PasswordSalt);

        }
    }
}