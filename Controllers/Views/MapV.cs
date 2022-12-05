namespace MallMapsApi.Controllers.Views
{
    public class MapV
    {
        public MapV( int mallID, int layer, List<ComponentV> components)
        {

            MallID = mallID;
            Layer = layer;
            Components = components;
        }
     
        public int MallID { get; set; }
        public int Layer { get; set; }
        public List<ComponentV> Components { get; set; }
    }
}
