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

        public void GetMapsByLocation(string location)
        {
            throw new NotImplementedException();
        }
    }
}
