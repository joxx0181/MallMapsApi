using MallMapsApi.CustomAttributes;
namespace MallMapsApi.Data.DTO
{
    [Table(Name ="Mall")]
    public class Mall
    {

        
        [Column(Name = "id",IgnoreSql = true)]
        public int Id { get; set; }

        [Column(Name = "firmid")]
        public int FirmId { get; set; }

        [Column(Name = "location")]
        public string Location { get; set; }

        public Mall(int id, int firmId, string location)
        {
            Id = id;
            FirmId = firmId;
            Location = location;
        }
        public Mall()
        {

        }
    }
}
