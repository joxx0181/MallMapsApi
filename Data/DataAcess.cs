using MallMapsApi.Interface;
using System.Data.SqlClient;
using MallMapsApi.Utils;
using System.Data;
using MallMapsApi.CustomAttributes;
using System.Reflection;


namespace MallMapsApi.Data
{
    /// <summary>
    /// Class for dataAccess
    /// </summary>
    public class DataAcess : ICrudAcess
    {
        /// <summary>
        /// configration file
        /// </summary>
        private readonly IConfiguration _configuration;
        /// <summary>
        /// SqlConnection
        /// </summary>
        private SqlConnection con;
        /// <summary>
        /// constructor for datacess, setup data connection
        /// </summary>
        /// <param name="configuration">Configuration file</param>
        public DataAcess(IConfiguration configuration)
        {
            //sets the configuration
            _configuration = configuration;
            //Set sql connection string
            con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

        }


        /// <summary>
        /// We can search for up to 2 diffrent criterias, Key is colum name and value is value
        /// </summary>
        /// <typeparam name="BaseEntity"></typeparam>
        /// <param name="searchData"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<BaseEntity> Get<BaseEntity>(Dictionary<string, object> searchData)
        {
            try
            {
                //if empty search data throw null execption
                if (searchData.Count() == 0)
                    throw new ArgumentNullException("Search data is empty");
                //Open connection to db
                OpenConnection();
                //create sql where command
                SqlCommand cmd = DbHelper.BuildWhereCommand<BaseEntity>(searchData, con.CreateCommand());

                //Get Data from db
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    //creating an datatable 
                    var dataTable = new DataTable();
                    //load data from db into datatable
                    dataTable.Load(reader);
                    //Convert entities to type param 
                    var entities = DbHelper.ConvertToBaseEntity<BaseEntity>(dataTable, false);
                    //Entities
                    return entities;
                }
            }
            catch (Exception)
            {
                //Return empty list upon execption TODO : log
                return Enumerable.Empty<BaseEntity>();
            }
            finally
            {
                //After all is done close connection as the last step before leaving
                CloseConnection();
            }
        }

        /// <summary>
        /// Get all children from an object 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private IEnumerable<object> GetChildren(Type type = default(Type))
        {
            try
            {
                //Null check 
                if (type == null || type == default(Type))
                    return null;
                //Open connection to db
                OpenConnection();
                //Create sql command
                var cmd = con.CreateCommand();
                //create select statement
                cmd.CommandText = $"SELECT * FROM {type.GetCustomAttribute<Table>().Name}";
                //Create datatable for data storage
                DataTable data = new DataTable();
                //Execute reader 
                SqlDataReader reader = cmd.ExecuteReader();
                //load all data from reader into datatable 
                data.Load(reader);
                //Convert DataTable to list of objects and return it 
                return DbHelper.ConvertToBaseEntity(data, type);
            }
            catch (Exception ex)
            {
                //TODO : log for now return null
                return null;
            }
            finally
            {
                //close connection
                CloseConnection();
            }
        }
        /// <summary>
        /// Db Get command with type param
        /// </summary>
        /// <typeparam name="BaseEntity">object type</typeparam>
        /// <returns>IEnumerable objecets</returns>
        public IEnumerable<BaseEntity> Get<BaseEntity>()
        {
            try
            {
                //Open connection
                OpenConnection();
                //CReate sql command 
                var cmd = con.CreateCommand();
                //create Select statement
                cmd.CommandText = $"SELECT * FROM {typeof(BaseEntity).GetCustomAttribute<Table>().Name}";
                //create Data table for storage
                DataTable data = new DataTable();
                //execute sql statment;
                SqlDataReader reader = cmd.ExecuteReader();
                //load all data from reader into datatable 
                data.Load(reader);
                //convert datatable to entities 
                var entities = DbHelper.ConvertToBaseEntity<BaseEntity>(data, false);
                foreach (var en in entities)
                {
                    //Join all properties with foreign key attribute
                    JoinOnGet<BaseEntity>(en);
                }
                //return dto with full map
                return entities;


            }
            catch (Exception ex)
            {
                //return null TODO : log execption 
                return null;
            }
            finally
            {
                //Close connection
                CloseConnection();
            }
        }

        /// <summary>
        /// Get collection from storeProcedure 
        /// </summary>
        /// <typeparam name="BaseEntity">object typeparam</typeparam>
        /// <param name="procedure">call stored procedure</param>
        /// <returns>Collection of enetities</returns>
        public IEnumerable<BaseEntity> GetByProcedure<BaseEntity>(string procedure)
        {
            try
            {
                //Open connection
                OpenConnection();
                //If procedure is null or empty return empty list
                if (procedure.IsStringNullOrWhiteSpace())
                    return new HashSet<BaseEntity>();
                //Create sql command
                var cmd = con.CreateCommand();
                //Set sql command to store procedure
                cmd.CommandType = CommandType.StoredProcedure;
                //Set command text to stored procedure 
                cmd.CommandText = procedure;
                //Create data table for data storage
                DataTable data = new DataTable();
                //Execute command
                SqlDataReader reader = cmd.ExecuteReader();
                //load data into datatable 
                data.Load(reader);
                //Convert data into collection from typeParam
                var entities = DbHelper.ConvertToBaseEntity<BaseEntity>(data, true);
                //Loop through each entity and map foreignkeys
                foreach (var entity in entities)
                {
                    //map all foreignkeys
                    JoinOnGet<BaseEntity>(entity);
                }
                //Return collection from type param
                return entities;
            }
            catch (Exception ex)
            {
               //TODO : LOG ex
               //return empty collection
                return new HashSet<BaseEntity>();
            }
            finally
            {
                //close connection
                CloseConnection();
            }
        }
        /// <summary>
        /// Add all reference to entity
        /// </summary>
        /// <typeparam name="BaseEntity">object type param</typeparam>
        /// <param name="entity">object type</param>
        public void JoinOnGet<BaseEntity>(BaseEntity entity)
        {
            //Get all properties where it has an foreignkey and type is class
            var classProperties = entity.GetType().GetProperties().Where(x => x.GetCustomAttribute<ForeignKey>() != null && x.GetType().IsClass);
            //Loop throgu all properties
            foreach (var prop in classProperties)
            {
                //Get foreignkey value
                var val = prop.GetValue(entity);
                //if value is null go to next 
                if (val == null)
                    continue;
                //Get object property reference 
                var refProp = entity.GetType().GetProperty(prop.Name + "Ref");
                //if there is no object property that referece to object then continue
                if (refProp == null)
                    continue;
                //if refprop is an type of collection then we want to set all into the property 
                if (DataHelper.IsEnumerableType(refProp))
                    refProp.SetValue(entity, GetChildren(refProp.PropertyType));
                else //if nots an property of an object we only want the first match.
                    refProp.SetValue(entity, GetChildren(refProp.PropertyType).FirstOrDefault());
            }
        }

        /// <summary>
        /// Insert and return id
        /// </summary>
        /// <typeparam name="BaseEntity">typeparam of object type</typeparam>
        /// <param name="baseEntity">insertable object</param>
        /// <returns>Row ID</returns>
        public int InsertScalar<BaseEntity>(BaseEntity baseEntity)
        {
            try
            {
                //Open connection
                OpenConnection();

                //create sql insert command
                SqlCommand cmd = DbHelper.BuildInsert<BaseEntity>(baseEntity, con.CreateCommand());
                //To get Last index we need to use select scope identity that return the id
                cmd.CommandText += "; Select SCOPE_IDENTITY()";
                //now we execute scalar and return the id
                return int.Parse(cmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                //TODO : log execption
                //Return -1 if error happen
                return -1;
            }
            finally
            {
                //finaly close the connection when all is done 
                CloseConnection();
            }
        }
        /// <summary>
        /// Insert generic object into db
        /// </summary>
        /// <typeparam name="BaseEntity">generic object</typeparam>
        /// <param name="baseEntity">object to insert</param>
        /// <returns>-1 error, bigger than -1 rows affected</returns>
        public int Insert<BaseEntity>(BaseEntity baseEntity)
        {
            try
            {
                //Open conncetion
                OpenConnection();
                //build insert command 
                SqlCommand cmd = DbHelper.BuildInsert<BaseEntity>(baseEntity, con.CreateCommand());
                //run execute non query and get rows affected
                return int.Parse(cmd.ExecuteNonQuery().ToString());
            }
            catch (Exception ex)
            {
                //TODO : LOG exception
                //Return -1 upon error
                return -1;
            }
            finally
            {
                //last step close connection
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

        /// <summary>
        /// Open db connection
        /// </summary>
        public void OpenConnection()
        {
            //if connection already is open dont open it again
            if (con.State != System.Data.ConnectionState.Open)
                con.Open();
        }

        /// <summary>
        /// Close connection for db 
        /// </summary>
        public void CloseConnection()
        {
            //if connection is closed no reason to close it 
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
