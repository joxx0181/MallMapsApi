using MallMapsApi.CustomAttributes;

namespace MallMapsApi.Data.DTO
{
    [Table(Name = "Firm")]
    public class Firm
    {
        [Column(Name = "cvrnr")]
        public string Cvr { get; set; }
        [Column(Name="name")]
        public string Name { get; set; }


    }
}
