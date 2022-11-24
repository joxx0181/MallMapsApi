﻿using MallMapsApi.Interface;

namespace MallMapsApi.DTO
{
    public class FirmUser : BaseEntity
    {
        public string TableId { get; private set; }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string SessionKey { get; set; }
        public string Firm { get; set; }

    }
}
