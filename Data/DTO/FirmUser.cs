using MallMapsApi.CustomAttributes;
using MallMapsApi.Interface;
using Microsoft.Data.SqlClient;
using System;
using System.Text.Json.Serialization;

namespace MallMapsApi.Data.DTO
{

    [Table(Name = "FirmUser")]
    public class FirmUser
    {
        

        [JsonIgnore]
        [Column(Name = "id", IgnoreSql = true)]
        public int ID { get; private set; }

        [Column(Name = "username")]
        public string Username { get; set; }

        [Column(Name = "password")]
        public string Password { get; set; }

        [Column(Name = "role")]
        public string Role { get; set; }

        [Column(Name = "sessionid", IgnoreSql = true)]
        public string SessionKey { get; set; }
        [ForeignKey(Key = "cvnr")]
        [Column(Name = "firmid")]
        public int Firmid { get; set; }
        
        public Firm FirmidRef { get; set; }
        internal FirmUser(int id, string username, string password, string role, string sessionKey, int firmid)
        {
            this.ID = id;
            this.Username = username;
            this.Password = password;
            this.Role = role;
            this.SessionKey = sessionKey;
            this.Firmid = firmid;
        }
        public FirmUser()
        {

        }
    }
}
