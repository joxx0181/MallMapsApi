using MallMapsApi.Controllers.Decorators;
using MallMapsApi.Data.DTO;
using MallMapsApi.Interface;

namespace MallMapsApi.Data
{
    public class MallHandler : IMall
    {
        public DataMapper mapper;

        private readonly ICrudAcess _crud;
        public MallHandler(ICrudAcess crud)
        {
            _crud = crud;
        }
        /// <summary>
        /// Create a new mall with the parameters
        /// </summary>
        /// <param name="firmid"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public string CreateMall(int firmid, string location)
        {
            mapper = new DataMapper();
            int state = _crud.Insert<Mall>(mapper.MallMapper(firmid, location));
            if (state == -1)
                return "Nothing happend";
            return "Mall added";
        }

        /// <summary>
        /// Returns an IEnumerable with all malls connected to the CVRNR
        /// </summary>
        /// <param name="cvrnr"></param>
        /// <returns></returns>
        public IEnumerable<MallDecorator> GetMalls(int cvrnr)
        {
            mapper = new DataMapper();
            var malls = mapper.DecoratorMallMapper(_crud.Get<Mall>().Where(x => x.FirmId == cvrnr));
            if(malls.Count() == 0)
                return null;
            return malls;
        }
    }
}
