using MallMapsApi.Controllers.Decorators;
using MallMapsApi.Data.DTO;

namespace MallMapsApi.Interface
{
    public interface IMall
    {
        public string CreateMall(int firmid, string location);
        public IEnumerable<MallDecorator> GetMalls(int cvrnr);
    }
}
