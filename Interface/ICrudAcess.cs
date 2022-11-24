namespace MallMapsApi.Interface
{
    public interface ICrudAcess
    {
        public T Insert<T>(T type);
        public T Update<T>(T type);
        public T Delete<T>(T type);
        public T Get<T>(T type);
        public T Get<T>(Dictionary<object,object> searchData, T type);
        public void OpenConnection();
        public void CloseConnection();

    }
}
