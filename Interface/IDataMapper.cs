
using MallMapsApi.Controllers.Decorators;
using MallMapsApi.Controllers.Views;
﻿using MallMapsApi.Data.DTO;
using Microsoft.SqlServer.Types;
using System.Data;


namespace MallMapsApi.Interface
{
    public interface IDataMapper
    {
        /// <summary>
        /// Create new object of FirmUser
        /// </summary>
        /// <param name="username">FirmUser username</param>
        /// <param name="password">Password for the FirmUSer</param>
        /// <param name="role">The FirmUser role</param>
        /// <param name="cvrnr">Cvr number</param>
        /// <returns>New FirmUser</returns>
        public FirmUser FirmUserMapper(string username, string password, string role, int cvrnr);
        /// <summary>
        /// Convert list of components to Dictionary
        /// </summary>
        /// <param name="components"></param>
        /// <returns> Dictionary Key = string, value = object</returns>
        public Dictionary<string, object> ComponentMapper(List<Component> components);
        /// <summary>
        /// Convert MapV to map
        /// </summary>
        /// <param name="map">Mapv object</param>
        /// <returns>Map</returns>
        public Map MapMapper(MapV map);
        /// <summary>
        /// Convert SqlGeometry to GeoDataV object
        /// </summary>
        /// <param name="geo"></param>
        /// <returns>GeoDataV containing Geometry</returns>
        public GeodataV GeoDataMapper(SqlGeometry geo);
        /// <summary>
        /// Map m all from firmid
        /// </summary>
        /// <param name="firmid">Firm id</param>
        /// <param name="location">Loaction</param>
        /// <returns>Mall</returns>
        public Mall MallMapper(int firmid, string location);
        /// <summary>
        /// Decorate Mall From collection of Malls
        /// </summary>
        /// <param name="malls">Collection of malls</param>
        /// <returns>collection of MallDecorator</returns>
        public IEnumerable<MallDecorator> DecoratorMallMapper(IEnumerable<Mall> malls);
        /// <summary>
        /// Convert collection of Map to collection of MallMapDecorator
        /// </summary>
        /// <param name="maps"></param>
        /// <returns>Collection of MallMapDecorator</returns>
        public IEnumerable<MallMapDecorator> DecoratorMallMapMapper(IEnumerable<Map> maps);

        // Create new object of Firm for returning new Firm
        public Firm FirmMapper(int cvr, string name);
    }
}
