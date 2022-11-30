using MallMapsApi.Controllers.Views;
using MallMapsApi.Data.DTO;

namespace MallMapsApi.Interface
{
    public interface IDataMapper
    {
        public FirmUser FirmUserMapper(string username, string password, string role, int cvrnr);
        public Dictionary<string, object> ComponentMapper(List<Component> components);
        public Map MapMapper(MapV map);
    }
}
