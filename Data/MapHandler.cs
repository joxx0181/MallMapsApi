using MallMapsApi.Controllers.Views;
using MallMapsApi.Data.DTO;
using MallMapsApi.Interface;

namespace MallMapsApi.Data
{
    /// <summary>
    /// MapHandler handle all map request and decortations
    /// </summary>
    public class MapHandler : IMap
    {
        private readonly ICrudAcess _crudAcess;
        /// <summary>
        /// Create instance of mapHandler.
        /// </summary>
        /// <param name="crudAcess">CrudAccess for database</param>
        public MapHandler(ICrudAcess crudAcess)
        {
            _crudAcess = crudAcess;
        }

        /// <summary>
        /// Create and insert an map into database
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public string CreateMap(MapV map)
        {
            //Check if map is null
            if (map != null)
            {
                //Creating instance of DataMapper
                DataMapper mapper = new DataMapper();
                //Map the MapV inputs into Dto map
                var mp = mapper.MapMapper(map);
                //Insert Dto into database and get the iD
                var id = _crudAcess.InsertScalar<Map>(mp);
                //Run through all components
                foreach (var component in mp.Components)
                {
                    //Set component ID 
                    component.MapID = id;
                    //Insert each component
                    _crudAcess.Insert<Component>(component);
                }
                //Return success
                return "Completed";
            }
            //Return empty data 
            return "Data was empty";
        }
        /// <summary>
        /// Get an map from mall id 
        /// </summary>
        /// <param name="mallid">id for the mall</param>
        /// <returns></returns>
        public IEnumerable<Map> GetMapsByLocation(int mallid)
        {
            ///Get maps from databases where mallID match
            IEnumerable<Map> maps = _crudAcess.Get<Map>().Where(x => x.MallId == mallid);
            //if not there is any maps return empty list
            if (!maps.Any())
                return Enumerable.Empty<Map>();

            //loop through each map 
            foreach (var map in maps)
            {
                //Get components by storedProcedure and where mapId = mallID
                var it = _crudAcess.GetByProcedure<Component>("GetComponents").Where(o => o.MapID == map.Id);
                //Create a new list for map object components
                map.Components = new List<Component>();
                //add components to the list 
                map.Components.AddRange(it.ToList());
                //Map mall reference to the map
                map.MallRef = _crudAcess.Get<Mall>().FirstOrDefault(x => x.Id == mallid) ?? default(Mall);
            }
            //return map
            return maps;
        }
    }
}