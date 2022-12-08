

using MallMapsApi.Data.DTO;

namespace MallMapsApi.Controllers.Decorators
{
    /// <summary>
    /// Decorator for FirmUser
    /// </summary>
    public class SessionUserDecorator
    {
        /// <summary>
        /// Private object of FirmUser
        /// </summary>
        private FirmUser _firmuser;
        /// <summary>
        /// The FirmUser role
        /// </summary>
        public string Role
        {
            get { return _firmuser.Role; }
        }
        /// <summary>
        /// FirmUser sessionKey
        /// </summary>
        public string SessionKey
        {
            get { return _firmuser.SessionKey; }
        }
        /// <summary>
        /// The firm id that belongs to the firm user
        /// </summary>
        public int FirmID
        {
            get { return _firmuser.Firmid; }
        }
        /// <summary>
        /// Create an instance of SessionUserDecorator and decorate it from firmUser
        /// </summary>
        /// <param name="firmUser"></param>
        public SessionUserDecorator(FirmUser firmUser)
        {
            _firmuser = firmUser;
        }
    }
}
