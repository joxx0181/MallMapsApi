using MallMapsApi.Interface;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MallMapsApi.DTO;

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

        /// <summary>
        /// We can search for up to 2 diffrent criterias, Key is colum name and value is value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="searchData"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T Get<T>(Dictionary<object, object> searchData, T type)
        {
            string tType = type.GetType().ToString();
            OpenConnection();
            SqlCommand cmd = con.CreateCommand();
            if (searchData.Count == 1)
            {
                cmd.CommandText = $"select * from {type.GetType().ToString} Where @colum = @value";
                cmd.Parameters.AddWithValue("@colum", searchData.ElementAt(0).Key);
                cmd.Parameters.AddWithValue("@value", searchData.ElementAt(1).Value);
            }
            if (searchData.Count == 2)
            {
                cmd.CommandText = $"select * from {type.GetType().ToString} Where @colum = @value and @colum2 = @value2";
                cmd.Parameters.AddWithValue("@colum", searchData.ElementAt(0).Key);
                cmd.Parameters.AddWithValue("@value", searchData.ElementAt(1).Value);
                cmd.Parameters.AddWithValue("@colum2", searchData.ElementAt(2).Key);
                cmd.Parameters.AddWithValue("@value2", searchData.ElementAt(3).Value);
            }
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (tType == "FirmUser")
                {
                    FirmUser user = new FirmUser();
                    user.Firm = reader["firmid"].ToString();
                    user.Id = Convert.ToInt32(reader["id"].ToString());
                    user.SessionKey = reader["sessionkey"].ToString();
                    user.Password = reader["password"].ToString();
                    user.Username = reader["username"].ToString();
                    user.Role = reader["role"].ToString();

                }
            };
            CloseConnection();
            throw new NotImplementedException();
        }

        public T Insert<T>(T type)
        {
            OpenConnection();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = $"insert into FirmUser(username,password,firmid,role,sessionkey) values(@username,@password,@firmid,@role,@sessionkey)";
            CloseConnection();
            throw new NotImplementedException();
        }

        public T Update<T>(T type)
        {
            throw new NotImplementedException();
        }
        public void OpenConnection()
        {
            if (con.State != System.Data.ConnectionState.Open)
                con.Open();
        }

        public void CloseConnection()
        {
            if (con.State != System.Data.ConnectionState.Closed)
                con.Close();
        }
    }
}
