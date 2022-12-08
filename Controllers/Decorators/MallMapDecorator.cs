using MallMapsApi.Data;
using MallMapsApi.Data.DTO;

namespace MallMapsApi.Controllers.Decorators
{
    /// <summary>
    /// Decorator for Map
    /// </summary>
    public class MallMapDecorator
    {
        /// <summary>
        /// Create private map.
        /// </summary>
        private Map _Map;
        /// <summary>
        /// Property for DataMapper
        /// </summary>
        private DataMapper DataMapper { get; set; }

        /// <summary>
        /// Create an instance of MapDecorator and decorate values from map
        /// </summary>
        /// <param name="map"></param>
        public MallMapDecorator(Map map)
        {
            _Map = map;
            DataMapper = new DataMapper();
        }
        /// <summary>
        /// The map layer
        /// </summary>
        public int Layer
        {
            get { return _Map.Layer; }
        }
        /// <summary>
        /// Map components
        /// </summary>
        public Dictionary<string,object> Components
        {
            get { return DataMapper.ComponentMapper(_Map.Components); } 
        }

    }
}
