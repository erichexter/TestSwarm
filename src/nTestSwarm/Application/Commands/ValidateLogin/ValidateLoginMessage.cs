using nTestSwarm.Application.Infrastructure.BusInfrastructure;

namespace nTestSwarm.Application.Commands.ValidateLogin
{
    public class ValidateLoginMessage:IRequest<bool>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}