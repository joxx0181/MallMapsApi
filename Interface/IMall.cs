using MallMapsApi.Controllers.Decorators;
using MallMapsApi.Data.DTO;

namespace MallMapsApi.Interface
{
    public interface IMall
    {
        /// <summary>
        /// Decorate mall from firmid and location
        /// </summary>
        /// <param name="firmid"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public string CreateMall(int firmid, string location);
        /// <summary>
        /// Get malls by cvr number
        /// </summary>
        /// <param name="cvrnr">the cvr number of the mall</param>
        /// <returns></returns>
        public IEnumerable<MallDecorator> GetMalls(int cvrnr);
    }
}
