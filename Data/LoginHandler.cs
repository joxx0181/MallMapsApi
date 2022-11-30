using MallMapsApi.Controllers.Decorators;
using MallMapsApi.Data.DTO;
using MallMapsApi.Interface;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace MallMapsApi.Data
{
    public class LoginHandler : IVerify
    {
        public DataMapper mapper;
        private readonly ICrudAcess _crud;
        public LoginHandler(ICrudAcess crud)
        {
            _crud = crud;
        }

        /// <summary>
        /// Creates a firmuser object and sends it to dataacess
        /// </summary>
        /// <param name="uName"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <param name="firmid"></param>
        /// <returns></returns>
        public string CreateUser(string uName, string password, string role, int firmid)
        {
            string hashPas = Sha256Hash(password);
            mapper = new DataMapper();
            _crud.Insert(mapper.FirmUserMapper(uName, hashPas, role, firmid));
            return "User added";
        }
        /// <summary>
        /// Generates random sessionkey
        /// </summary>
        /// <returns></returns>
        public string GenerateSessionKey()
        {
            string token = Guid.NewGuid().ToString().Replace("-", "");

            return token;
        }

        /// <summary>
        /// Hash string using sha256
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string Sha256Hash(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                return BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(password))).Replace("-", "");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public SessionUserDecorator Verifiy(string username, string password)
        {
            Dictionary<string, object> searchObject = new Dictionary<string, object>();
            searchObject.Add("username", username);
            searchObject.Add("password", password);
            FirmUser user = _crud.Get<FirmUser>(searchObject)?.FirstOrDefault();
            if (user == null)
                return null;
            if (user.SessionKey != null)
                user.SessionKey = GenerateSessionKey();
            return new SessionUserDecorator(user);
        }

        public IEnumerable<FirmUser> GetUsers()
        {
            return _crud.Get<FirmUser>();
        }
    }
}
