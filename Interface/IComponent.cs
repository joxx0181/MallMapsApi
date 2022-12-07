using MallMapsApi.Data.DTO;

namespace MallMapsApi.Interface
{
    public interface IComponent
    {
        /// <summary>
        /// Get command for component
        /// </summary>
        /// <param name="mapid">map id to get component from</param>
        /// <returns>Dictionary string and object</returns>
        public Dictionary<string, object> GetById(int mapid);
    }
}
