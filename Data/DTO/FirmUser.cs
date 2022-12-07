using MallMapsApi.CustomAttributes;
using MallMapsApi.Interface;
using Microsoft.Data.SqlClient;
using System;
using System.Text.Json.Serialization;

namespace MallMapsApi.Data.DTO
{

    /// <summary>
    /// DTO model of users
    /// </summary>
    [Table(Name = "FirmUser")]
    public class FirmUser
    {
        /// <summary>
        /// auto incremented id
        /// </summary>
        [JsonIgnore]
        [Column(Name = "id", IgnoreSql = true)]
        public int ID { get; private set; }
        /// <summary>
        /// username of the FirmUser
        /// </summary>
        [Column(Name = "username")]
        public string Username { get; set; }
        /// <summary>
        /// The FirmUser password
        /// </summary>
        [Column(Name = "password")]
        public string Password { get; set; }
        /// <summary>
        /// Role used to Specify what are allowed
        /// </summary>
        [Column(Name = "role")]
        public string Role { get; set; }
        /// <summary>
        /// Session id
        /// </summary>
        [Column(Name = "sessionid", IgnoreSql = true)]
        public string SessionKey { get; set; }
        /// <summary>
        /// Id of the firm the user belongs to
        /// </summary>
        [ForeignKey(Key = "cvnr")]
        [Column(Name = "firmid")]
        public int Firmid { get; set; }
        /// <summary>
        /// Firm reference
        /// </summary>
        public Firm FirmidRef { get; set; }
        /// <summary>
        /// Construct an FirmUser from parameters
        /// </summary>
        /// <param name="id">user id</param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <param name="sessionKey">session key to check login</param>
        /// <param name="firmid">firm id to bind user to firm</param>
        internal FirmUser(int id, string username, string password, string role, string sessionKey, int firmid)
        {
            this.ID = id;
            this.Username = username;
            this.Password = password;
            this.Role = role;
            this.SessionKey = sessionKey;
            this.Firmid = firmid;
        }

        //TODO : Replace constructor and use Activator with Arguments (this message can be seen under View -> TaskList)
        /// <summary>
        /// Empty constructor for Reflection Activator. 
        /// </summary>
        internal FirmUser()
        {

        }
    }
}
