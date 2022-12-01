using MallMapsApi.Data.DTO;

namespace MallMapsApi.Interface
{
    public interface ICrudAcess
    {
        public int InsertScalar<BaseEntity>(BaseEntity baseEntity);
        public int Insert<BaseEntity>(BaseEntity baseEntity);
        public BaseEntity Update<BaseEntity>(BaseEntity baseEntity);
        public BaseEntity Delete<BaseEntity>(BaseEntity baseEntity);
        public IEnumerable<BaseEntity> Get<BaseEntity>();
        public IEnumerable<BaseEntity> Get<BaseEntity>(Dictionary<string,object> searchData);

        public int ValidateUser(string user, string psw);
        public void UpdateSession(string session, int sessionID);
        public void OpenConnection();
        public void CloseConnection();
        public IEnumerable<BaseEntity> GetByProcedure<BaseEntity>(string procedure);

    }
}
