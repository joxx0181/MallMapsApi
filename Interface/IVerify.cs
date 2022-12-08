using MallMapsApi.Controllers.Decorators;
using MallMapsApi.Data.DTO;

namespace MallMapsApi.Interface
{
    public interface IVerify
    {
        /// <summary>
        /// Verify user and return new session
        /// </summary>
        /// <param name="uName">username</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        public SessionUserDecorator Verifiy(string uName, string password);
        /// <summary>
        /// Generate a session key
        /// </summary>
        /// <returns>Session key</returns>
        public string GenerateSessionKey();
        /// <summary>
        /// Hash string to Sha256
        /// </summary>
        /// <param name="password"></param>
        /// <returns>hashed 256 string</returns>
        public string Sha256Hash(string password);
        /// <summary>
        /// Create FirmUser
        /// </summary>
        /// <param name="uName">Username</param>
        /// <param name="password">password</param>
        /// <param name="role">role for the user</param>
        /// <param name="firmid">cvr number</param>
        /// <returns></returns>
        public string CreateUser(string uName, string password, string role, int firmid);
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>IEnumerable of FirmUser</returns>
        public IEnumerable<FirmUser> GetUsers();
    }
}
