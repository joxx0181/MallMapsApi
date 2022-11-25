namespace MallMapsApi.Utils
{
    public static class DataHelper
    {
        public static bool IsStringNullOrWhiteSpace(string str)
        {
            if(string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
                return false;
            return true;
        }

        public static bool CVRNRIsValid(int cvrnr)
        {
            if (cvrnr < 1)
                return false;

            char[] chars = cvrnr.ToString().ToCharArray();
            if (chars == null || chars.Length == 0)
                return false;

            if (chars.Length > 8)
                return false;

            return true;
        }
    }
}
