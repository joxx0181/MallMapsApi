using MallMapsApi.DTO;
using MallMapsApi.Interface;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace MallMapsApi.Data
{
    public class DataHandler : IVerify
    {
        private readonly ICrudAcess _crud;
        public DataHandler(ICrudAcess crud)
        {
            _crud = crud;
        }

        public string CreateUser(string uName, string password, string role, int firmid)
        {
            string hashPas = Sha256Hash(password);
            _crud.Insert(user);
            return "User added";
        }

        public string GenerateSessionKey()
        {
            string token = Guid.NewGuid().ToString();

            return token;
        }

        public string Sha256Hash(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                return BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(password))).Replace("-", "");
            }
        }
        public FirmUser Verifiy(FirmUser user)
        {
            Dictionary<string, object> searchObject = new Dictionary<string, object>();
            if (user.Password == "Admin" && user.Username == "Admin")
            {
                searchObject.Add("username", user.Username);
                searchObject.Add("password", user.Password);
            }
            else
            {
                searchObject.Add("username", user.Username);
                searchObject.Add("password", user.Password = Sha256Hash(user.Password));
            }
            _crud.Get<FirmUser>(searchObject);
            throw new NotImplementedException();
        }
    }
}
