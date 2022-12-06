using MallMapsApi.CustomAttributes;
using Microsoft.SqlServer.Types;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MallMapsApi.Data.DTO
{
    [Table(Name = "Components")]
    public class Component
    {

        [JsonIgnore]
        [Column(Name = "id", IgnoreSql = true)]
        public int Id { get; set; }

        [Column(Name = "mapid")]
        public int MapID { get; set; }

        [Column(Name = "image")]
        public byte[] Img { get; set; }

        [Column(Name = "description")]
        public string Description { get; set; }

        [Column(Name = "geodata")]
        public string GetGeoData 
        { 
            get 
            { 
                return Geodata.ToString(); 
            } 
            set 
            {
                Geodata =  SqlGeometry.Parse(value);        
            }
        }

        [JsonIgnore]
        public SqlGeometry Geodata { get; set; }

        [Column(Name = "zindex")]
        public int Zindex { get; set; }


        public Component(int mapID, byte[] img, string description, SqlGeometry geodata, int zindex)
        {
            this.MapID = mapID;
            this.Img = img;
            this.Description = description;
            this.Geodata = geodata;
            this.Zindex = zindex;
        }

        public Component()
        {

        }

    }
}
