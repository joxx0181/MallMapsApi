namespace MallMapsApi.Controllers.Views
{
    /// <summary>
    /// MapV recieved from frontend
    /// </summary>
    public class MapV
    {
        /// <summary>
        /// create instance of Mapv with list of ComponentV 
        /// </summary>
        /// <param name="mallID">Id of the mall</param>
        /// <param name="layer">the layer of the map</param>
        /// <param name="components">all the map components</param>
        public MapV(int mallID, int layer, List<ComponentV> components)
        {
            MallID = mallID;
            Layer = layer;
            Components = components;
        }
        /// <summary>
        /// Id of the Mall
        /// </summary>
        public int MallID { get; set; }
        /// <summary>
        /// Map layer
        /// </summary>
        public int Layer { get; set; }
        /// <summary>
        /// Collection of components
        /// </summary>
        public List<ComponentV> Components { get; set; }
    }
}
