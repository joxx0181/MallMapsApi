using MallMapsApi.CustomAttributes;

namespace MallMapsApi.Data.DTO
{
    [Table(Name = "Firm")]
    public class Firm
    {
        public Firm(string cvr, string name)
        {
            Cvr = cvr;
            Name = name;
        }
        
        [Column(Name = "cvrnr")]
        public string Cvr { get; set; }
        [Column(Name="name")]
        public string Name { get; set; }
    }
}
