using MallMapsApi.Controllers.Decorators;
using MallMapsApi.Controllers.Views;
using MallMapsApi.CustomAttributes;
using MallMapsApi.Data.DTO;
using MallMapsApi.Interface;
using MallMapsApi.Utils;
using Microsoft.SqlServer.Types;
using System.Data;
using System.Reflection;

namespace MallMapsApi.Data
{
    public class DataMapper : IDataMapper
    {
        public FirmUser FirmUserMapper(string username, string password, string role, int cvrnr)
        {
            FirmUser user = new FirmUser(0, username, password, role, null, cvrnr);
            return user;
        }

        /// <summary>
        /// Maps the compoents and returns a dictonary of Basic and icon ComponentDecorators.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> ComponentMapper(List<Component> components)
        {
            Dictionary<string, object> componentDict = new Dictionary<string, object>();
            if (components == null)
                return null;
            for (int i = 0; i < components.Count; i++)
            {
                //TODO : GeodataV v = new GeodataV();
                if (components[i].Img != null) 
                    componentDict.Add("IconComponent", new IconComponentDecorator(components[i],  null));
                else
                    componentDict.Add("BasicComponent", new BasicComponentDecorator(components[i], null));
            }
            if (componentDict.Count > 0)
                return componentDict;

            return null;
        }
        public Map MapMapper(MapV mapV)
        {
            Map map = new Map(mapV.MallID, mapV.Layer);
            foreach (var item in mapV.Components)
            {
                map.Components.Add(new Component(-1,null, item.Description, item.CreateGeoDat(), item.ZIndex));
            }
            return map;
        }

        public GeodataV MapMapper(SqlGeometry geo)
        {
            //geo.ToString()
            //new GeodataV( ,srid: geo.STSrid);
        }
    }
}
