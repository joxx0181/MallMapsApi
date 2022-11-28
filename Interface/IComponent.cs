using MallMapsApi.Data.DTO;

namespace MallMapsApi.Interface
{
    public interface IComponent
    {
        public Dictionary<string, object> GetById(int mapid);
    }
}
