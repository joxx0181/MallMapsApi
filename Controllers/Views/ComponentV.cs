using Microsoft.SqlServer.Types;
using System.Text.Json.Serialization;

namespace MallMapsApi.Controllers.Views
{
    public class ComponentV
    {
        public ComponentV(string description, int zIndex, GeodataV geodata)
        {
            Description = description;
            ZIndex = zIndex;
            GeoData = geodata;
        }
        public string Description { get; set; }
        public int ZIndex { get; set; }
        public GeodataV GeoData { get; set; }
        public SqlGeometry CreateGeoDat()
        {
            return SqlGeometry.STGeomFromText(GeoData.SqlCharBuilder(), GeoData.Srid);
        }

    }
}
