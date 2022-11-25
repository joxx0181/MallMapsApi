using System.Runtime.Serialization;

namespace MallMapsApi.Data.DTO
{
    public abstract class BaseEntity
    {
        public virtual string TableID { get; set; }
    }
}
