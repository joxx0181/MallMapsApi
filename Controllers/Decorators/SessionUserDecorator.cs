

using MallMapsApi.Data.DTO;

namespace MallMapsApi.Controllers.Decorators
{
    public class SessionUserDecorator
    {
        private FirmUser _firmuser;

        public SessionUserDecorator(FirmUser firmUser)
        {
            _firmuser = firmUser;
        }
        public string Role
        {
            get { return _firmuser.Role; }
        }
        
        public string SessionKey
        {
            get { return _firmuser.SessionKey; }
        }
    }
}
