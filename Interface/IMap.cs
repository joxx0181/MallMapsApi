using MallMapsApi.Controllers.Decorators;
using MallMapsApi.Controllers.Views;
using MallMapsApi.Data.DTO;

namespace MallMapsApi.Interface
{
    public interface IMap
    {
        public IEnumerable<Map> GetMapsByLocation(int mallid);
        public string CreateMap(MapV map);
    }
}
