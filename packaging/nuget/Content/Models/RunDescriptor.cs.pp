using nTestSwarm.Interface;

namespace $rootnamespace$.Models
{
    public class RunDescriptor : IRunDescriptor
    {
        public RunDescriptor()
        {
        }

        public RunDescriptor(string name, string url)
        {
            Name = name;
            Url = url;
        }

        public string Name
        {
            get;
            set;
        }

        public string Url
        {
            get;
            set;
        }
    }
}