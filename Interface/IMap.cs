using MallMapsApi.Controllers.Decorators;
using MallMapsApi.Controllers.Views;

namespace MallMapsApi.Interface
{
    public interface IMap
    {
        public void GetMapsByLocation(string location);
        public MallMapDecorator CreateMap(MapV map);
    }
}
