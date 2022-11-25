using MallMapsApi.Interface;
using System.Text.Json.Serialization;

namespace MallMapsApi.Data.DTO
{
    public class FirmUser : BaseEntity
    {
        internal FirmUser(int id, string username, string password, string role, string sessionKey, string firm)
        {
            Id = id;
            Username = username;
            Password = password;
            Role = role;
            SessionKey = sessionKey;
            Firm = firm;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string SessionKey { get; set; }
        public string Firm { get; set; }

    }
}
