using MallMapsApi.Data.DTO;

namespace MallMapsApi.Interface
{
    public interface IMapper
    {
        public FirmUser FirmUserMapper(string username, string password, string role, int cvrnr);
        public Dictionary<string, object> ComponentMapper(List<Component> components);
    }
}
