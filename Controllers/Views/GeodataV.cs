namespace MallMapsApi.Controllers.Views
{
    /// <summary>
    /// GeodataV retrieved from frontend
    /// </summary>
    public class GeodataV
    {
        /// <summary>
        /// Creating an instance of GeoDataV
        /// </summary>
        /// <param name="type">Geometry type</param>
        /// <param name="xInput">array of X positions</param>
        /// <param name="yInput">array of Y positions</param>
        /// <param name="srid">Srid value</param>
        public GeodataV(string type, int[] xInput, int[] yInput, int srid)
        {
            Type = type;
            XInput = xInput;
            YInput = yInput;
            Srid = srid;
        }
        /// <summary>
        /// Geometry type
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Array of X position inputs
        /// </summary>
        public int[] XInput { get; set; }
        /// <summary>
        /// Array of Y positions
        /// </summary>
        public int[] YInput { get; set; }
        /// <summary>
        /// Srid for Spartial data
        /// </summary>
        public int Srid { get; set; }
        /// <summary>
        /// SqlChar builder from X Y Pos
        /// </summary>
        /// <returns></returns>
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
