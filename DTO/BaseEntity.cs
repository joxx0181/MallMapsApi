using System.Text.Json.Serialization;

namespace MallMapsApi.DTO
{
    public abstract class BaseEntity 
    {
        [JsonIgnore]
        private protected string tableID;

        [JsonIgnore]
        public string TableID { get { return tableID; } }

     
    }
}
