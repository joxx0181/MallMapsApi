using Microsoft.SqlServer.Types;
using Newtonsoft.Json;

namespace MallMapsApi.Data.DTO
{
    public class Component
    {
        public Component(int mapID, byte[] img, string description, SqlGeometry geodata, int zindex)
        {
            MapID = mapID;
            Img = img;
            Description = description;
            Geodata = geodata;
            Zindex = zindex;
        }

        public int MapID { get; set; }
        public byte[] Img { get; set; }
        public string Description { get; set; }
        public SqlGeometry Geodata { get; set; }
        public int Zindex { get; set; }
    }
}
