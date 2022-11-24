using MallMapsApi.DTO;

namespace MallMapsApi.Interface
{
    public interface IVerify
    {
        public FirmUser Verifiy(FirmUser user);
        public string GenerateSessionKey();
        public string Sha256Hash(string password);
        public string CreateUser(FirmUser user);
    }
}
