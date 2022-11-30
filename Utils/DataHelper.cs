﻿using Microsoft.SqlServer.Types;

namespace MallMapsApi.Utils
{
    public static class DataHelper
    {
        public static bool IsStringNullOrWhiteSpace(this string str)
        {
            if(string.IsNullOrWhiteSpace(str))
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



    }
}
