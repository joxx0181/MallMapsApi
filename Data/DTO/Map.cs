using MallMapsApi.CustomAttributes;
using System.Text.Json.Serialization;

namespace MallMapsApi.Data.DTO
{
    /// <summary>
    /// DTO model of Mall
    /// </summary>
    [Table(Name = "Map")]
    public class Map
    {
        /// <summary>
        /// AutoIncrement id.
        /// </summary>
        [JsonIgnore]
        [Column(Name = "id",IgnoreSql = true)]
        public int Id { get; set; }
        /// <summary>
        /// MallID for db reference 
        /// </summary>
        [ForeignKey(Key = "id")]
        [Column(Name = "mallid")]
        public int MallId { get; set; }
        /// <summary>
        /// List of map components
        /// </summary>
        public List<Component> Components { get; set; }

        /// <summary>
        /// Drawing layer
        /// </summary>
        [Column(Name = "layer")]
        public int Layer { get; set; }
        /// <summary>
        /// Reference to master object
        /// </summary>
        public Mall? MallRef { get; set; }
        /// <summary>
        /// Create map from Mallid and layer
        /// </summary>
        /// <param name="mallId"></param>
        /// <param name="layer"></param>
        public Map(int mallId, int layer)
        {
            MallId = mallId;
            Components = new List<Component>();
            Layer = layer;
        }
        //TODO : Replace constructor and use Activator with Arguments (this message can be seen under View -> TaskList)
        /// <summary>
        /// Constructor used for Reflection Activator
        /// </summary>
        public Map()
        {
            Components = new List<Component>();
        }
    }
}
