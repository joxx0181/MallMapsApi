using MallMapsApi.Controllers.Views;
using MallMapsApi.Data.DTO;
using Microsoft.SqlServer.Types;
namespace MallMapsApi.Controllers.Decorators
{
    public class IconComponentDecorator : BaseComponentDecorator
    {
        public IconComponentDecorator(Component _component, GeodataV geodataV) : base(_component, geodataV)
        {
        }

        public byte[] Image
        {
            get { return component.Img; }
        }
        public string Description
        {
            get { return component.Description; }
        }
   

    }
}
