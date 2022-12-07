using MallMapsApi.Controllers.Views;
using MallMapsApi.Data.DTO;
using Microsoft.SqlServer.Types;
namespace MallMapsApi.Controllers.Decorators
{
    /// <summary>
    /// BasicComponent for basic components
    /// </summary>
    public class BasicComponentDecorator : BaseComponentDecorator
    {
        
        /// <summary>
        /// Create and instance of component and decorate it.
        /// </summary>
        /// <param name="_component"></param>
        /// <param name="geodataV"></param>
        public BasicComponentDecorator(Component _component, GeodataV geodataV) : base(_component, geodataV)
        {

        }

    }
}
