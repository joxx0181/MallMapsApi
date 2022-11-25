using MallMapsApi.Interface;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MallMapsApi.DTO
{

    public class FirmUser
    {
        [JsonIgnore]
        public int ID { get; private set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string SessionKey { get; set; }
        public int Firmid { get; set; }


        public FirmUser()
        {
       
        }
    }
}
