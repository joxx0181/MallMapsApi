namespace MallMapsApi.Controllers.Views
{
    public class MapV
    {
        public MapV(int id, int mallID, int layer, List<ComponentV> components)
        {
            Id = id;
            MallID = mallID;
            Layer = layer;
            Components = components;
        }

        public int Id { get; set; }
        public int MallID { get; set; }
        public int Layer { get; set; }
        public List<ComponentV> Components { get; set; }
    }
}
