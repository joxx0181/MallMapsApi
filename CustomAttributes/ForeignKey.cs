namespace MallMapsApi.CustomAttributes
{
    /// <summary>
    /// Custom attribute foreignkey is for mapping object
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ForeignKey : Attribute
    {
        /// <summary>
        /// The key name
        /// </summary>
        private string key = string.Empty; 
        /// <summary>
        /// Key name lookup
        /// </summary>
        public string Key { get { return key; } set { key = value; } }
    }
}
