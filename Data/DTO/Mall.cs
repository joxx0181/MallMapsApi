using MallMapsApi.CustomAttributes;
namespace MallMapsApi.Data.DTO
{
    public class Mall
    {
        public Mall(int id, int firmId, string location)
        {
            Id = id;
            FirmId = firmId;
            Location = location;
        }

        [Column(Name = "id")]
        public int Id { get; set; }

        [Column(Name = "firmid")]
        public int FirmId { get; set; }

        [Column(Name = "location")]
        public string Location { get; set; }
    }
}
