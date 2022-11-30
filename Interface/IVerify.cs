using MallMapsApi.Controllers.Decorators;
using MallMapsApi.Data.DTO;

namespace MallMapsApi.Interface
{
    public interface IVerify
    {
        public SessionUserDecorator Verifiy(string uName, string password);
        public string GenerateSessionKey();
        public string Sha256Hash(string password);
        public string CreateUser(string uName, string password, string role, int firmid);

        public IEnumerable<FirmUser> GetUsers();
    }
}
