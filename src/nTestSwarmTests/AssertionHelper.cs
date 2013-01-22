using System.Reflection;
using Should;

namespace nTestSwarmTests
{
    public static class AssertionHelper
    {
        public static void ShouldMatchObject(this object obj1, object obj2)
        {
            //obj1.ShouldNotBeSameAs(obj2);
            obj1.ShouldEqual(obj2);

            PropertyInfo[] infos = obj1.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo info in infos)
            {
                object value1 = info.GetValue(obj1, null);
                object value2 = info.GetValue(obj2, null);
                value1.ShouldEqual(value2, string.Format("Property {0} doesn't match", info.Name));
            }
        }
    }
}