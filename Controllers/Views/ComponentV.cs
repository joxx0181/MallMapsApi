using Microsoft.SqlServer.Types;
using System.Text.Json.Serialization;

namespace MallMapsApi.Controllers.Views
{
    /// <summary>
    /// COmponentV model is fronend model
    /// </summary>
    public class ComponentV
    {
        /// <summary>
        /// Create instance of ComponentV from parameters
        /// </summary>
        /// <param name="description">description of the Component</param>
        /// <param name="zIndex">The Z index of the component</param>
        /// <param name="geodata">GeodataV</param>
        public ComponentV(string description, int zIndex, GeodataV geodata)
        {
            Description = description;
            ZIndex = zIndex;
            GeoData = geodata;
        }
        /// <summary>
        /// Description of the component
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The Z Index of the component
        /// </summary>
        public int ZIndex { get; set; }
        /// <summary>
        /// GeodataV reference
        /// </summary>
        public GeodataV GeoData { get; set; }
        /// <summary>
        /// Get SqlGemtry object from GeoData
        /// </summary>
        /// <returns></returns>
        public SqlGeometry CreateGeoDat()
        {
            return SqlGeometry.STGeomFromText(GeoData.SqlCharBuilder(), GeoData.Srid);
        }

    }
}
