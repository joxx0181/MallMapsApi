using MallMapsApi.Controllers.Decorators;
using MallMapsApi.Controllers.Views;
using MallMapsApi.Data.DTO;
using MallMapsApi.Interface;

namespace MallMapsApi.Data
{
    public class MapHandler : IMap
    {
        private readonly ICrudAcess _crudAcess;
        public MapHandler(ICrudAcess crudAcess)
        {
            _crudAcess = crudAcess;
        }

        public string CreateMap(MapV map)
        {
            DataMapper mapper = new DataMapper();
            if (map != null)
            {
                var mp = mapper.MapMapper(map);

                var id = _crudAcess.InsertScalar<Map>(mp);
                foreach (var component in mp.Components)
                {
                    component.MapID = id;
                    _crudAcess.Insert<Component>(component);
                }


                return "Completed";
            }
            return "Data was empty";
        }

        public IEnumerable<Map> GetMapsByLocation(int mallid)
        {
            IEnumerable<Map> map = _crudAcess.Get<Map>().Where(x => x.MallId == mallid);
            foreach (var item in map)
            {
                item.Components.AddRange(_crudAcess.Get<Component>().Where(z => z.MapID == item.Id));
            }
            return map;
        }
    }
}