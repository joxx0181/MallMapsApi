namespace MallMapsApi.CustomAttributes
{
    //Creating custom dataannotion
    [AttributeUsage(AttributeTargets.Class)]
    public class Table : Attribute
    {
        public string Name { get; set; }
    }
}
