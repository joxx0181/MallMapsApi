namespace MallMapsApi.Interface
{
    public interface ICrudAcess
    {
        public BaseEntity Insert<BaseEntity>(BaseEntity baseEntity);
        public BaseEntity Update<BaseEntity>(BaseEntity baseEntity);
        public BaseEntity Delete<BaseEntity>(BaseEntity baseEntity);
        public IEnumerable<BaseEntity> Get<BaseEntity>();
        public IEnumerable<BaseEntity> Get<BaseEntity>(Dictionary<string,object> searchData);


        public void OpenConnection();
        public void CloseConnection();

    }
}
