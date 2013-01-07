namespace nTestSwarm.Application.Services
{
    public interface ICryptographer
    {
        string CreateSalt();
        string ComputeHash(string valueToHash);
        string GetPasswordHash(string password, string salt);
    }
}