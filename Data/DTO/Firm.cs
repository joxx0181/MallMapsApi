using MallMapsApi.CustomAttributes;

namespace MallMapsApi.Data.DTO
{
    [Table(Name = "Firm")]
    public class Firm
    {
        [Column(Name = "cvrnr")]
        public int Cvr { get; set; }
        [Column(Name="name")]
        public string Name { get; set; }

        public Firm(int cvr, string name)
        {
            Cvr = cvr;
            Name = name;
        }
        
        public Firm()

        {

        }

    }
}
