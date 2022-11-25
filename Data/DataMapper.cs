using MallMapsApi.Controllers.Decorators;
using MallMapsApi.Data.DTO;

namespace MallMapsApi.Data
{
    public class DataMapper
    {
        public FirmUser FirmUserMapper(string username, string password, string role, int cvrnr)
        {
            FirmUser user = new FirmUser(0, username, password, role, null, cvrnr);
            return user;
        }
    }
}
