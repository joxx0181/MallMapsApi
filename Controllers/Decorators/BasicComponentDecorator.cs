using MallMapsApi.Data.DTO;
using Microsoft.SqlServer.Types;
namespace MallMapsApi.Controllers.Decorators
{
    public class BasicComponentDecorator
    {
        private Component _component;

        public BasicComponentDecorator(Component component)
        {
            _component = component;
        }
        public int MapId
        {
            get { return _component.MapID; }
        }
        public SqlGeometry GeoData
        {
            get { return _component.Geodata; }
        }
        public int Zindex
        {
            get { return _component.Zindex; }
        }
    }
}
