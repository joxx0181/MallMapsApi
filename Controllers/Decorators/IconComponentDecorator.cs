using MallMapsApi.Data.DTO;
using Microsoft.SqlServer.Types;
namespace MallMapsApi.Controllers.Decorators
{
    public class IconComponentDecorator
    {
        private Component _component;

        public IconComponentDecorator(Component component)
        {
            _component = component;
        }
        public int MapID 
        {
           get { return _component.MapID; }
        }
        public SqlGeometry GeoData
        {
            get { return _component.Geodata; }
        }
        public byte[] Image
        {
            get { return _component.Img; }
        }
        public string Description
        {
            get { return _component.Description; }
        }
        public int Zindex
        {
            get { return _component.Zindex; }
        }

    }
}
