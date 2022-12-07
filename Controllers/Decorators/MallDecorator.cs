using MallMapsApi.Data.DTO;

namespace MallMapsApi.Controllers.Decorators
{
    /// <summary>
    /// Decorator for mall
    /// </summary>
    public class MallDecorator
    {
        //private object of Mall
        private Mall mall;

        /// <summary>
        /// Mall Id from Mall
        /// </summary>
        public int Id
        {
            get { return mall.Id; }
        }
        /// <summary>
        /// Location from Mall
        /// </summary>
        public string Location
        {
            get { return mall.Location; }
        }
        
        /// <summary>
        /// create an instance of Mall and decorate it
        /// </summary>
        /// <param name="mall"></param>
        public MallDecorator(Mall mall)
        {
            this.mall = mall;
        }
    }
}
