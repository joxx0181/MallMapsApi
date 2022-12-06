

using MallMapsApi.Data.DTO;

namespace MallMapsApi.Controllers.Decorators
{
    public class SessionUserDecorator
    {
        private FirmUser _firmuser;

        public string Role
        {
            get { return _firmuser.Role; }
        }
        
        public string SessionKey
        {
            get { return _firmuser.SessionKey; }
        }

        public int FirmID
        {
            get { return _firmuser.Firmid; }
        }
        public SessionUserDecorator(FirmUser firmUser)
        {
            _firmuser = firmUser;
        }
    }
}
