using MallMapsApi.Data;
using MallMapsApi.Data.DTO;

namespace MallMapsApi.Controllers.Decorators
{
    public class MallMapDecorator
    {
        private Map _Map;
        private DataMapper DataMapper { get; set; }

        public MallMapDecorator(Map map)
        {
            _Map = map;
            DataMapper = new DataMapper();
        }

        public int Layer
        {
            get { return _Map.Layer; }
        }

        public Dictionary<string,object> Components
        {
            get { return DataMapper.ComponentMapper(_Map.Components); } 
        }


    }
}
