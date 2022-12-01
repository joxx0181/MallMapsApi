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
            IEnumerable<Map> maps = _crudAcess.Get<Map>().Where(x => x.MallId == mallid);
            if(!maps.Any())
                return Enumerable.Empty<Map>();

            foreach (var map in maps)
            {
                var it = _crudAcess.GetByProcedure<Component>("GetComponents").Where(o => o.MapID == map.Id);
                map.Components = new List<Component>();
                map.Components.AddRange(it.ToList());
                map.MallRef = _crudAcess.Get<Mall>().FirstOrDefault(x => x.Id == mallid) ?? default(Mall);
            }
            return maps;
        }
    }
}