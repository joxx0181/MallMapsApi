using MallMapsApi.Controllers.Views;
using MallMapsApi.Data.DTO;
using MallMapsApi.Interface;
using MallMapsApi.Utils;

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
                _crudAcess.Insert<Map>(mapper.MapMapper(map));
                return "Completed";
            }
            return "Data was empty";
        }

        public void GetMapsByLocation(string location)
        {
            if (location.IsStringNullOrWhiteSpace())
        }
    }
}
