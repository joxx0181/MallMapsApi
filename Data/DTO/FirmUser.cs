using MallMapsApi.Interface;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MallMapsApi.Data.DTO
{

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
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string SessionKey { get; set; }
        public int Firmid { get; set; }


    }
}
