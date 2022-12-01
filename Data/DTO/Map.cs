using MallMapsApi.CustomAttributes;
using System.Text.Json.Serialization;

namespace MallMapsApi.Data.DTO
{
    [Table(Name = "Map")]
    public class Map
    {

        [JsonIgnore]
        [Column(Name = "id")]
        public int Id { get; set; }

        [ForeignKey(Key = "id")]
        [Column(Name = "mallid")]
        public int MallId { get; set; }

        public List<Component> Components { get; set; }

        [Column(Name = "layer")]
        public int Layer { get; set; }
        public Mall MallRef { get; set; }

        public Map(int mallId, int layer)
        {
            MallId = mallId;
            Components = new List<Component>();
            Layer = layer;
        }

        public Map()
        {

        }
    }
}
