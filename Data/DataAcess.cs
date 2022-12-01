using MallMapsApi.Interface;
using System.Data.SqlClient;
using MallMapsApi.Utils;
using System.Data;
using MallMapsApi.CustomAttributes;
using System.Reflection;
using Microsoft.SqlServer.Types;

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
                    var entities = DbHelper.ConvertToBaseEntity<BaseEntity>(dataTable, false);

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
                cmd.CommandText = $"SELECT * FROM {typeof(BaseEntity).GetCustomAttribute<Table>().Name}";
             
                AppDomain.CurrentDomain.SetData("System.Data.DataSetDefaultAllowedTypes", typeof(SqlGeography));
                DataTable data = new DataTable();
                //SqlDataReader reader = cmd.ExecuteReader();
                data.Columns.Add("SpatialData", typeof(System.Data.SqlTypes.SqlBytes));
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(data);

                //if (reader.GetFieldType("geodata") != null)
                //data.Load(reader);
                var entities = DbHelper.ConvertToBaseEntity<BaseEntity>(data, false);

                foreach (var en in entities)
                {
                    JoinOnGet<BaseEntity>(en);
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


        public void JoinOnGet<BaseEntity>(BaseEntity entity)
        {
            var classProperties = entity.GetType().GetProperties().Where(x => x.GetCustomAttribute<ForeignKey>() != null && x.GetType().IsClass);
            foreach (var prop in classProperties)
            {
                var val = prop.GetValue(entity);

                if (val == null)
                    continue;

                var refProp = entity.GetType().GetProperty(prop.Name + "Ref");

                if (refProp == null)
                    continue;

                if (DataHelper.IsEnumerableType(refProp))
                    refProp.SetValue(entity, GetChildren(refProp.PropertyType));
                else
                    refProp.SetValue(entity, GetChildren(refProp.PropertyType).FirstOrDefault());
            }
        }

        public int InsertScalar<BaseEntity>(BaseEntity baseEntity)
        {
            try
            {
                OpenConnection();

                //todo : 
                SqlCommand cmd = DbHelper.BuildInsert<BaseEntity>(baseEntity, con.CreateCommand());
                cmd.CommandText += "; Select SCOPE_IDENTITY()";
                return int.Parse(cmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                CloseConnection();
            }
        }
        public int Insert<BaseEntity>(BaseEntity baseEntity)
        {
            try
            {
                OpenConnection();

                //todo : 
                SqlCommand cmd = DbHelper.BuildInsert<BaseEntity>(baseEntity, con.CreateCommand());
                return int.Parse(cmd.ExecuteNonQuery().ToString());
            }
            catch (Exception ex)
            {
                return -1;
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




        public int ValidateUser(string user, string psw)
        {
            try
            {
                OpenConnection();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "ValidateUser";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(user);
                cmd.Parameters.Add(psw);
                var reader = cmd.ExecuteReader();
                return reader.GetInt32(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

        }

        public void UpdateSession(string session, int sessionID)
        {
            try
            {
                OpenConnection();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UpdateSession";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(session);
                cmd.Parameters.Add(sessionID);
                var reader = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
