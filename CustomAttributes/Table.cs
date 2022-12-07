namespace MallMapsApi.CustomAttributes
{
    /// <summary>
    /// Creating custom dataannotion
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class Table : Attribute
    {
        //attribute string 
        private string name = string.Empty;
        /// <summary>
        /// Name of database Table 1-1
        /// </summary>
        public string Name
        {
            get
            { 
                return name; 
            }
            set
            {
                name = value;
            }
        }
    }
}
