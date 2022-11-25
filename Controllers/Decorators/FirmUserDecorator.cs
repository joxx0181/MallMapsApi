using MallMapsApi.Data.DTO;

namespace MallMapsApi.Controllers.Decorators
{
    public abstract class FirmUserDecorator
    {
        private FirmUser _firmUser;

        public FirmUserDecorator(FirmUser firmUser)
        {
            _firmUser = firmUser;
        }

        public string Role
        {
            get { return _firmUser.Role; }
        }

        public string SessionKey
        {
            get { return _firmUser.SessionKey; }
        }




    }
}
