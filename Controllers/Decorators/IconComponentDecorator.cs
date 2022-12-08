using MallMapsApi.Controllers.Views;
using MallMapsApi.Data.DTO;
using Microsoft.SqlServer.Types;
namespace MallMapsApi.Controllers.Decorators
{
    /// <summary>
    /// Decorator for Components with Icons
    /// </summary>
    public class IconComponentDecorator : BaseComponentDecorator
    {
        /// <summary>
        /// Create an Instance and decorate it.
        /// </summary>
        /// <param name="_component"></param>
        /// <param name="geodataV"></param>
        public IconComponentDecorator(Component _component, GeodataV geodataV) : base(_component, geodataV)
        {
        }

        /// <summary>
        /// Img returning value
        /// </summary>
        public byte[] Image
        {
            get { return component.Img; }
        }
        /// <summary>
        /// Description of the component
        /// </summary>
        public string Description
        {
            get { return component.Description; }
        }
   

    }
}
