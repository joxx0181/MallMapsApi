using MallMapsApi.Controllers.Views;
using MallMapsApi.Data.DTO;
using Microsoft.SqlServer.Types;

namespace MallMapsApi.Controllers.Decorators
{
    /// <summary>
    /// Base component for decorators
    /// </summary>
    public abstract class BaseComponentDecorator
    {
        /// <summary>
        /// Component for decorator
        /// </summary>
        protected Component component;
        /// <summary>
        /// GeodataV for Geometry data
        /// </summary>
        protected GeodataV geodataV;
        /// <summary>
        /// The Z index of the component
        /// </summary>
        public int Zindex
        {
            get { return component.Zindex; }
        }
        /// <summary>
        /// The map id of the component
        /// </summary>
        public virtual int MapId
        {
            get { return component.MapID; }
        }
        /// <summary>
        /// Create an instance and decorate it, to prevent wrong data to reach frontend
        /// </summary>
        /// <param name="_component">Component</param>
        /// <param name="_geodataV">GeoDataV</param>
        public BaseComponentDecorator(Component _component, GeodataV _geodataV)
        {
            component = _component;
            geodataV = _geodataV;
        }
        public GeodataV GeoData
        {
            get { return geodataV; }
        }

    }
}
