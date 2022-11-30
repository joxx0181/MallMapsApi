namespace MallMapsApi.CustomAttributes
{

    /// <summary>
    /// Custom attribute for columns
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class Column : Attribute
    {
        private string name = "Unknown";
        //custom attribute default value;
        private bool ignoreSql = false;
        /// <summary>
        /// Db column name
        /// </summary>
        public string Name { get { return name; } set { name = value; } }
        /// <summary>
        /// default false
        /// </summary>
        public bool IgnoreSql { get { return ignoreSql; } set { ignoreSql = value; } }
    }
}
