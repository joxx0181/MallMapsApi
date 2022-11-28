using MallMapsApi.Data.DTO;
using MallMapsApi.Interface;

namespace MallMapsApi.Data
{
    public class ComponentHandler : IComponent
    {
        private DataMapper _mapper;
        private ICrudAcess _crudAcess;

        public ComponentHandler(ICrudAcess crudAcess)
        {
            _crudAcess = crudAcess;
        }

        /// <summary>
        /// Get components based on mapid
        /// </summary>
        /// <param name="mapid"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Dictionary<string, object> GetById(int mapid)
        {
            try
            {
                _mapper = new DataMapper();
                Dictionary<string, object> searchObject = new Dictionary<string, object>();
                searchObject.Add("Mapid", mapid);
                List<Component> components = _crudAcess.Get<Component>(searchObject)?.ToList();
                Dictionary<string, object> data = _mapper.ComponentMapper(components);
                if (data != null)
                    return data;
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
