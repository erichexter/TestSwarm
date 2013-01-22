using nTestSwarm.Application.Domain;

namespace nTestSwarm.Application.Services
{
    public interface IAuthenticationService
    {
        bool PasswordMatches(User user, string password);
    }
}