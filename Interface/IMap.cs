using MallMapsApi.Controllers.Views;

namespace MallMapsApi.Interface
{
    public interface IMap
    {
        public void GetMapsByLocation(string location);
        public string CreateMap(MapV map);
    }
}
