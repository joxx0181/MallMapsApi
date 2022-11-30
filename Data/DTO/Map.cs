using MallMapsApi.CustomAttributes;
using Newtonsoft.Json;

namespace MallMapsApi.Data.DTO
{
    [Table(Name = "Map")]
    public class Map
    {
        public Map(int mallId, int layer)
        {
            MallId = mallId;
            Components = new List<Component>();
            Layer = layer;
        }

        [Column(Name = "mallid")]
        public int MallId { get; set; }
        public List<Component> Components { get; set; }
        [Column(Name = "layer")]
        public int Layer { get; set; }
    }
}
