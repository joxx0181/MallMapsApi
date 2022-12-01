using MallMapsApi.Data;
using MallMapsApi.Data.DTO;

namespace MallMapsApi.Controllers.Decorators
{
    public class MallMapDecorator
    {
        private Map _Map;
        DataMapper DataMapper { get; set; } = new DataMapper();

        public MallMapDecorator(Map map)
        {
            _Map = map;
        }

        public int Etage
        {
            get { return _Map.Layer; }
        }

        public Dictionary<string,object> Components { get { return DataMapper.ComponentMapper(_Map.Components); } }


    }
}
