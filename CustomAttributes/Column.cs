namespace MallMapsApi.CustomAttributes
{

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class Column : Attribute
    {
        public string Name { get; set; }
    }
}
