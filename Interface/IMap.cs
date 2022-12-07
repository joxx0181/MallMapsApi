using MallMapsApi.Controllers.Decorators;
using MallMapsApi.Controllers.Views;
using MallMapsApi.Data.DTO;

namespace MallMapsApi.Interface
{
    public interface IMap
    {
        /// <summary>
        /// Get get return model of map
        /// </summary>
        /// <param name="mallid">map mall id</param>
        /// <returns>IEnumerable of maps</returns>
        public IEnumerable<MallMapDecorator> GetMapsByLocation(int mallid);
        /// <summary>
        /// Create an map from MapV
        /// </summary>
        /// <param name="map"></param>
        /// <returns>string</returns>
        public string CreateMap(MapV map);
    }
}
