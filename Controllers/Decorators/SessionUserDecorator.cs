

using MallMapsApi.Data.DTO;

namespace MallMapsApi.Controllers.Decorators
{
    public class SessionUserDecorator
    {
        private FirmUserDecorator _firmUserDecorator;

        public SessionUserDecorator(FirmUserDecorator firmUserDecorator)
        {
            _firmUserDecorator = firmUserDecorator;
        }
        public string Role
        {
            get { return _firmUserDecorator.Role; }
        }
        
        public string SessionKey
        {
            get { return _firmUserDecorator.SessionKey; }
        }
    }
}
