using MallMapsApi.Controllers.Views;
using MallMapsApi.Data.DTO;
using Microsoft.SqlServer.Types;

namespace MallMapsApi.Controllers.Decorators
{
    public abstract class BaseComponentDecorator
    {
        protected Component component;
        protected GeodataV geodataV;

        public BaseComponentDecorator(Component _component, GeodataV _geodataV)
        {
            component = _component;
            geodataV = _geodataV;
        }
        public int Zindex
        {
            get { return component.Zindex; }
        }

        public virtual int MapId
        {
            get { return component.MapID; }
        }

        public GeodataV GeoData
        {
            get { return geodataV; }
        }

    }
}
