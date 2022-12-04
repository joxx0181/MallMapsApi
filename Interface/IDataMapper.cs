
using MallMapsApi.Controllers.Decorators;
using MallMapsApi.Controllers.Views;
﻿using MallMapsApi.Data.DTO;
using Microsoft.SqlServer.Types;
using System.Data;


namespace MallMapsApi.Interface
{
    public interface IDataMapper
    {
        public FirmUser FirmUserMapper(string username, string password, string role, int cvrnr);
        public Dictionary<string, object> ComponentMapper(List<Component> components);
        public Map MapMapper(MapV map);
        public GeodataV GeoDataMapper(SqlGeometry geo);
        public Mall MallMapper(int firmid, string location);
        public List<MallDecorator> DecoratorMallMapper(IEnumerable<Mall> malls);
        public List<MallMapDecorator> DecoratorMallMapMapper(IEnumerable<Map> maps);
    }
}
