using Microsoft.SqlServer.Types;


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
            var test = SqlGeometry.STGeomFromText(GeoData.SqlCharBuilder(), GeoData.Srid);
            return test;
        }

    }
}
