using MallMapsApi.Interface;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MallMapsApi.Data.DTO;
using MallMapsApi.Utils;

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


        /// <summary>
        /// We can search for up to 2 diffrent criterias, Key is colum name and value is value
        /// </summary>
        /// <typeparam name="BaseEntity"></typeparam>
        /// <param name="searchData"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<BaseEntity> Get<BaseEntity>(Dictionary<string, object> searchData)
        {
            try
            {
                if (searchData.Count() == 0)
                    throw new ArgumentNullException("Search data is empty");

                OpenConnection();
                SqlCommand cmd = DbHelper.BuildWhereCommand<BaseEntity>(searchData, con.CreateCommand());


                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.HasRows)
                        return Enumerable.Empty<BaseEntity>();

                    return reader.Cast<BaseEntity>();
                }


            }
            catch (Exception)
            {
                return Enumerable.Empty<BaseEntity>();
            }
            finally
            {
                CloseConnection();
            }
        }

        public IEnumerable<BaseEntity> Get<BaseEntity>()
        {
            try
            {
                OpenConnection();
                var cmd = con.CreateCommand();
                cmd.CommandText = $"SELECT * FROM {typeof(BaseEntity).Name}";



                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    return new HashSet<BaseEntity>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                CloseConnection();
            }
        }


        public BaseEntity Insert<BaseEntity>(BaseEntity baseEntity)
        {
            try
            {
                OpenConnection();
                

                SqlCommand cmd = DbHelper.BuildInsert<BaseEntity>(baseEntity, con.CreateCommand());

                if (cmd.ExecuteNonQuery() > 0)
                    return baseEntity;
                

                return default(BaseEntity);
            }
            catch (Exception ex)
            {
                return default(BaseEntity);
            }
            finally
            {
                CloseConnection();
            }
        }

        public BaseEntity Update<BaseEntity>(BaseEntity baseEntity)
        {
            throw new NotImplementedException();
        }

        public BaseEntity Delete<BaseEntity>(BaseEntity baseEntity)
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

        public string InsertMap(Map map)
        {
            throw new NotImplementedException();
        }
    }
}
