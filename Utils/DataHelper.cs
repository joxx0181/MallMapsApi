using Microsoft.SqlServer.Types;
using System.Drawing;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MallMapsApi.Utils
{
    /// <summary>
    /// DataHelper is made towards making validation check easier and prevent duplicated code.
    /// </summary>
    public static class DataHelper
    {
        /// <summary>
        /// Check is string is null empty or whitespace 
        /// </summary>
        /// <param name="str">str to check</param>
        /// <returns>bool</returns>
        internal static bool IsStringNullOrWhiteSpace(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return true;
            return false;
        }

        /// <summary>
        /// Check if cvr is valid 
        /// </summary>
        /// <param name="cvrnr"></param>
        /// <returns></returns>
        internal static bool CVRIsNotValid(int cvrnr)
        {
            //if less then 1 return true
            if (cvrnr < 1)
                return true;
            //convert cvr to char array
            char[] chars = cvrnr.ToString().ToCharArray();
            //check if null or 0
            if (chars == null || chars.Length == 0)
                return true;
            //check if its
            if (chars.Length > 8)
                return true;

            return false;
        }
        /// <summary>
        /// Check is property is Enumerable
        /// </summary>
        /// <param name="prop">proprty you want to check</param>
        /// <returns>true is list or false if not</returns>
        internal static bool IsEnumerableType(PropertyInfo prop)
        {
            //if the prop is null return false
            if (prop == null)
                return false;
            //Get the type of the property
            Type type = prop.GetType();
            //Check if type can be assigned as collection
            if (typeof(System.Collections.IList).IsAssignableFrom(type))
                return true;
            //Check each interface in type 
            foreach (var interfaceTypes in type.GetInterfaces())
                //Check if the generic type defination is equal to IList
                if (interfaceTypes.IsGenericType && typeof(IList<>) == interfaceTypes.GetGenericTypeDefinition())
                    return true;
            //Return false if none of the above is true
            return false;

        }


        internal static string GetGeoType(SqlGeometry sqlGeo)
        {
            return Regex.Match(sqlGeo.ToString(), @"[A-z]+").Value;
        }
        internal static Dictionary<string, int[]> GetXYFromGeo(SqlGeometry sqlGeo)
        {
            Dictionary<string, int[]> _XYvalues = new Dictionary<string, int[]>();

            List<int> x = new List<int>();
            List<int> y = new List<int>();
            for (int i = 1; i < sqlGeo.STNumPoints(); i++)
            {
                var matchCollection = Regex.Matches(sqlGeo.STPointN(i).ToString(), @"(\d+)");
                x.Add(int.Parse(matchCollection[0].Value));
                y.Add(int.Parse(matchCollection[1].Value));
            }

            _XYvalues.Add("XPos", x.ToArray());
            _XYvalues.Add("YPos", y.ToArray());

            return _XYvalues;
        }
    }
}
