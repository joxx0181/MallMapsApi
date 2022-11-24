using MallMapsApi.Interface;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MallMapsApi.Data
{
    public class DataAcess : ICrudAcess
    {

        private readonly IConfiguration _configuration; 
        private SqlConnection con;
        public DataAcess(IConfiguration configuration)
        {
            _configuration = configuration;
            con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        public T Delete<T>(T type)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(T type)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(Dictionary<object, object> searchData)
        {
            throw new NotImplementedException();
        }

        public T Insert<T>(T type)
        {
            throw new NotImplementedException();
        }

        public T Update<T>(T type)
        {
            throw new NotImplementedException();
        }
        private void OpenConnection()
        {
            if(con.State != System.Data.ConnectionState.Open)
                con.Open();
        }
        
        private void CloseConnection()
        {
            if(con.State != System.Data.ConnectionState.Closed)
                con.Close();
        }
    }
}
