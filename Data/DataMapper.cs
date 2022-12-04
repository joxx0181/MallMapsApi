using MallMapsApi.Controllers.Decorators;
using MallMapsApi.Controllers.Views;
using MallMapsApi.CustomAttributes;
using MallMapsApi.Data.DTO;
using MallMapsApi.Interface;
using MallMapsApi.Utils;
using Microsoft.SqlServer.Types;
using System.Data;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using Xunit.Sdk;

namespace MallMapsApi.Data
{
    /// <summary>
    /// DataMapper to map input dato into our DTO
    /// </summary>
    public class DataMapper : IDataMapper
    {
        /// <summary>
        /// instance of datamapper
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <param name="cvrnr"></param>
        /// <returns></returns>
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
            //if components is null return null
            if (components == null)
                return null;

            //CReating an dictionary 
            Dictionary<string, object> componentDict = new Dictionary<string, object>();
            //Run through each component
            for (int i = 0; i < components.Count; i++)
            {
               
                //TODO : GeodataV v = new GeodataV();
                //check if image is null
                if (components[i].Img != null) //if not null decorate IconComponent
                    componentDict.Add("IconComponent", new IconComponentDecorator(components[i], null));
                else //create basic component 
                    componentDict.Add("BasicComponent", new BasicComponentDecorator(components[i], null));
            }
            //if any component dic return dictionary
            if (componentDict.Count > 0)
                return componentDict;
            return null;
        }

        /// <summary>
        /// Maps Mapv to DTO mapv
        /// </summary>
        /// <param name="mapV">MapView</param>
        /// <returns>DTO Map</returns>
        public Map MapMapper(MapV mapV)
        {
            //Creating new object of map
            Map map = new Map(mapV.MallID, mapV.Layer);
            //Adding all components to map
            foreach (var item in mapV.Components)
            {
                map.Components.Add(new Component(-1, null, item.Description, item.CreateGeoDat(), item.ZIndex));
            }
            //return map
            return map;
        }

        public GeodataV GeoDataMapper(SqlGeometry geo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a mall object
        /// </summary>
        /// <param name="firmid"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public Mall MallMapper(int firmid, string location)
        {
            Mall mall = new Mall(-1, firmid, location);
            return mall;
        }
        /// <summary>
        /// Maps all the malls in the Enumerable to list of MallDecorators
        /// </summary>
        /// <param name="malls"></param>
        /// <returns></returns>
        public List<MallDecorator> DecoratorMallMapper(IEnumerable<Mall> malls)
        {
            List<MallDecorator> decorators = new List<MallDecorator>();
            foreach (var mall in malls)
            {
                decorators.Add(new MallDecorator(mall));
            }
            return decorators;
        }
    }
}
