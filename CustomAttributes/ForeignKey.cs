namespace MallMapsApi.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ForeignKey : Attribute
    {
        private string key;

        public string Key { get { return key; } set { key = value; } }
    }
}
