using MallMapsApi.Interface;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MallMapsApi.Data.DTO;
using MallMapsApi.Utils;
using System.Data;
using MallMapsApi.CustomAttributes;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Text;

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
                    var dataTable = new DataTable();
                    dataTable.Load(reader);
                    var entities = DbHelper.ConvertToBaseEntity<BaseEntity>(dataTable);

                    return entities;
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




        private IEnumerable<object> GetChildren(Type type = default(Type))
        {
            try
            {
                if (type == null || type == default(Type))
                    return null;
                OpenConnection();
                var cmd = con.CreateCommand();
                cmd.CommandText = $"SELECT * FROM {type.GetCustomAttribute<Table>().Name}";
                DataTable data = new DataTable();
                SqlDataReader reader = cmd.ExecuteReader();
                data.Load(reader);
                return DbHelper.ConvertToBaseEntity(data, type);
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
        public IEnumerable<BaseEntity> Get<BaseEntity>()
        {
            try
            {
                OpenConnection();
                var cmd = con.CreateCommand();
                cmd.CommandText = $"SELECT * FROM {typeof(BaseEntity).Name}";
                DataTable data = new DataTable();
                SqlDataReader reader = cmd.ExecuteReader();
                data.Load(reader);
                var entities = DbHelper.ConvertToBaseEntity<BaseEntity>(data);

                foreach (var en in entities)
                {
                    Join<BaseEntity>(en);
                }
                return entities;


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


        public void Join<BaseEntity>(BaseEntity entity)
        {
            var classProperties = entity.GetType().GetProperties().Where(x => x.GetCustomAttribute<ForeignKey>() != null && x.GetType().IsClass);
            foreach (var prop in classProperties)
            {
                var val = prop.GetValue(entity);

                if (val == null)
                    continue;

                var refProp = entity.GetType().GetProperty(prop.Name + "Ref");

                var pair = GetChildren(refProp.PropertyType).FirstOrDefault();
                
                refProp.SetValue(entity, pair);

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


    }
}
