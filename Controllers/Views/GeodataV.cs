namespace MallMapsApi.Controllers.Views
{
    public class GeodataV
    {
        public GeodataV(string type, int[] xInput, int[] yInput, int srid)
        {
            Type = type;
            XInput = xInput;
            YInput = yInput;
            Srid = srid;
        }
        public string Type { get; set; }
        public int[] XInput { get; set; }
        public int[] YInput { get; set; }
        public int Srid { get; set; }

        public System.Data.SqlTypes.SqlChars SqlCharBuilder()
        {
            string sqlChars = Type.ToUpper() + "((";
            for (int i = 0; i < XInput.Length; i++)
            {
                if(i == XInput.Length -1)
                    sqlChars += XInput[i].ToString() + " " + YInput[i].ToString();
                else
                    sqlChars += XInput[i].ToString() + " " + YInput[i].ToString() + ",";
            }
            sqlChars += "))";

            return new System.Data.SqlTypes.SqlChars(sqlChars.ToCharArray());
        }
    }
}
