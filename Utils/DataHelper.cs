using Microsoft.SqlServer.Types;
using System.Reflection;

namespace MallMapsApi.Utils
{
    public static class DataHelper
    {
        public static bool IsStringNullOrWhiteSpace(string str)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
                return true;
            return false;
        }

        public static bool CVRNRIsValid(int cvrnr)
        {
            if (cvrnr < 1)
                return true;

            char[] chars = cvrnr.ToString().ToCharArray();
            if (chars == null || chars.Length == 0)
                return true;

            if (chars.Length > 8)
                return true;

            return false;
        }

        public static bool IsEnumerableType(PropertyInfo prop)
        {
            Type type = prop.GetType();
            if (typeof(System.Collections.IList).IsAssignableFrom(type))
                return true;

            foreach (var it in type.GetInterfaces())
                if (it.IsGenericType && typeof(IList<>) == it.GetGenericTypeDefinition())
                    return true;

            return false;

        }
    }
}
