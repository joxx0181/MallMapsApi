namespace MallMapsApi.Data.DTO
{
    public class Firm
    {
        public Firm(string cvr, string name)
        {
            Cvr = cvr;
            Name = name;
        }
        public string Cvr { get; set; }
        public string Name { get; set; }
    }
}
