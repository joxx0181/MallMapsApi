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
            if(map != null)
               return _crudAcess.InsertMap(mapper.MapMapper(map));

            return "Data was empty";
        }

        public void GetMapsByLocation(string location)
        {
            throw new NotImplementedException();
        }
    }
}
