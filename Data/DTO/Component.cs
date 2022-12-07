using MallMapsApi.CustomAttributes;
using Microsoft.SqlServer.Types;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MallMapsApi.Data.DTO
{
    /// <summary>
    /// DTO Model of component
    /// </summary>
    [Table(Name = "Components")]
    public class Component
    {
        /// <summary>
        /// Id for database
        /// </summary>
        [JsonIgnore]
        [Column(Name = "id", IgnoreSql = true)]
        public int Id { get; set; }
        /// <summary>
        /// mapID for Database
        /// </summary>
        [Column(Name = "mapid")]
        public int MapID { get; set; }
        /// <summary>
        /// bytearray for image
        /// </summary>
        [Column(Name = "image")]
        public byte[] Img { get; set; }
        /// <summary>
        /// the description of component
        /// </summary>
        [Column(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Get Geomtry data as string, Api is not allowing to send Geometry objects 
        /// </summary>
        [Column(Name = "geodata")]
        public string GetGeoData
        {
            get
            {
                //returning example {POLYGON(10 20,20 40,30 50,40 60,10 20)
                return Geodata.ToString();
            }
            set
            {
                //Parse the Geometry data as polygon
                Geodata = SqlGeometry.Parse(value);
            }
        }
        /// <summary>
        /// SqlGeometry for SpartialData
        /// </summary>
        [JsonIgnore]
        public SqlGeometry? Geodata { get; set; }

        /// <summary>
        /// The Z index of the component
        /// </summary>
        [Column(Name = "zindex")]
        public int Zindex { get; set; }

        /// <summary>
        /// Create an instance of Firm Class
        /// </summary>
        /// <param name="mapID">the Map id for database usage</param>
        /// <param name="img">IMage as byteArray</param>
        /// <param name="description">Description the component</param>
        /// <param name="geodata">SqlGemotry for Spartial data</param>
        /// <param name="zindex">Index Z </param>
        public Component(int mapID, byte[] img, string description, SqlGeometry geodata, int zindex)
        {
            this.MapID = mapID;
            this.Img = img;
            this.Description = description;
            this.Geodata = geodata;
            this.Zindex = zindex;
        }

        //TODO : Replace constructor and use Activator with Arguments (this message can be seen under View -> TaskList)
        /// <summary>
        /// Empty constructor for Reflection Activator
        /// </summary>
        public Component()
        {
            //Adding values to prevent VS warning
            Img = new byte[0];
            Description = "";
        }

    }
}
