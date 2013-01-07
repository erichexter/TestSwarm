using nTestSwarm.Application.Domain;

namespace nTestSwarm.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        readonly ICryptographer _cryptographer;

        public AuthenticationService(ICryptographer cryptographer)
        {
            _cryptographer = cryptographer;
        }

        public bool PasswordMatches(User user, string password)
        {
            var passwordHash = _cryptographer.GetPasswordHash(password, user.PasswordSalt);
            return passwordHash.Equals(user.PasswordHash);
        }
    }
}