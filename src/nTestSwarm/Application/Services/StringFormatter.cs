namespace nTestSwarm.Application.Services
{
    public interface IStringFormatter
    {
        string Format(string format, params object[] args);
    }

    public class StringFormatter : IStringFormatter
    {
        public string Format(string format, params object[] args)
        {
            return format;
        }
    }
}