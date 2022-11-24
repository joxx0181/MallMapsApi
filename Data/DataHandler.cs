using MallMapsApi.Interface;

namespace MallMapsApi.Data
{
    public class DataHandler : ITest
    {
        private readonly ICrudAcess _crud;
        public DataHandler(ICrudAcess crud)
        {
            _crud = crud;
        }
        public void Test()
        {
            _crud.Insert("");
        }
    }
}
