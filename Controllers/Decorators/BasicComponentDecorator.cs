using MallMapsApi.Controllers.Views;
using MallMapsApi.Data.DTO;
using Microsoft.SqlServer.Types;
namespace MallMapsApi.Controllers.Decorators
{
    public class BasicComponentDecorator : BaseComponentDecorator
    {
        
        public BasicComponentDecorator(Component _component, GeodataV geodataV) : base(_component, geodataV)
        {

        }

    }
}
