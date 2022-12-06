using MallMapsApi.Data.DTO;

namespace MallMapsApi.Controllers.Decorators
{
    public class MallDecorator
    {
        private Mall mall;

        public int Id
        {
            get { return mall.Id; }
        }
        public string Location
        {
            get { return mall.Location; }
        }
        
        public MallDecorator(Mall mall)
        {
            this.mall = mall;
        }
    }
}
