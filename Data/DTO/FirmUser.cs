using MallMapsApi.CustomAttributes;
using MallMapsApi.Interface;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MallMapsApi.Data.DTO
{

    [Table(Name = "FirmUser")]
    public class FirmUser
    {
        internal FirmUser(int iD, string username, string password, string role, string sessionKey, int firmid)
        {
            ID = iD;
            Username = username;
            Password = password;
            Role = role;
            SessionKey = sessionKey;
            Firmid = firmid;
        }

        [JsonIgnore]
        public int ID { get; private set; }
        [Column(Name = "username")]
        public string Username { get; set; }
        [Column(Name = "password")]
        public string Password { get; set; }
        [Column(Name = "role")]
        public string Role { get; set; }
        [Column(Name = "sessionKey")]
        public string SessionKey { get; set; }
        [Column(Name = "firmid")]
        public int Firmid { get; set; }


    }
}
