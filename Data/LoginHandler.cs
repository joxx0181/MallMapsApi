using MallMapsApi.Controllers.Decorators;
using MallMapsApi.Data.DTO;
using MallMapsApi.Interface;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace MallMapsApi.Data
{
    /// <summary>
    /// Login handler for FirmUsers
    /// </summary>
    public class LoginHandler : IVerify
    {
        /// <summary>
        /// DataMapper for FirmUser
        /// </summary>
        public DataMapper mapper;
        /// <summary>
        /// Database access
        /// </summary>
        private readonly ICrudAcess _crud;
        /// <summary>
        /// Create an instance and implement database
        /// </summary>
        /// <param name="crud">Interface for db</param>
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
            //hashing password by using sha 256 hasing
            string hashPas = Sha256Hash(password);
            //create an new instance of DataMapper
            mapper = new DataMapper();
            //Insert firmUser
            _crud.InsertScalar(mapper.FirmUserMapper(uName, hashPas, role, firmid));
            //Return user added
            return "User added";
        }
        /// <summary>
        /// Generates random sessionkey
        /// </summary>
        /// <returns></returns>
        public string GenerateSessionKey()
        {
            //Genere new session key
            string token = Guid.NewGuid().ToString().Replace("-", "");
            //return session key
            return token;
        }

        /// <summary>
        /// Hash string using sha256
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string Sha256Hash(string password)
        {
            //using sha256 to hash passwrod.
            using (var sha256 = SHA256.Create())
            {
                //Return hashed string
                return BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(password))).Replace("-", "");
            }
        }

        /// <summary>
        /// verify user
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public SessionUserDecorator Verifiy(string username, string password)
        {
            //Get firmUser that match search object 
            FirmUser user = _crud.Get<FirmUser>().FirstOrDefault(o => o.Username == username && o.Password == password);
            //Check if any user found 
            if (user == null)
                return null;
            //check if sessionKEy is null or empty else create one
            if (user.SessionKey != null)
                user.SessionKey = GenerateSessionKey();
            //return new user
            return new SessionUserDecorator(user);
        }
        /// <summary>
        /// Get all users created
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FirmUser> GetUsers()
        {
            return _crud.Get<FirmUser>();
        }
    }
}
