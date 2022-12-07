using MallMapsApi.Data.DTO;

namespace MallMapsApi.Interface
{
    public interface ICrudAccess
    {
        /// <summary>
        /// Insert Enity into database
        /// </summary>
        /// <typeparam name="BaseEntity">object type parameter</typeparam>
        /// <param name="baseEntity">object to insert</param>
        /// <returns>id from the insert</returns>
        public int InsertScalar<BaseEntity>(BaseEntity baseEntity);
        /// <summary>
        /// Insert into database
        /// </summary>
        /// <typeparam name="BaseEntity">Object type parameter</typeparam>
        /// <param name="baseEntity"></param>
        /// <returns>number of rows added</returns>
        public int Insert<BaseEntity>(BaseEntity baseEntity);
        /// <summary>
        /// Database get command 
        /// </summary>
        /// <typeparam name="BaseEntity">object type</typeparam>
        /// <returns>Collection of BaseEntity</returns>
        public IEnumerable<BaseEntity> Get<BaseEntity>();
        /// <summary>
        /// Database Getcommand
        /// </summary>
        /// <typeparam name="BaseEntity"></typeparam>
        /// <param name="searchData"></param>
        /// <returns>Collection of BaseEntity</returns>
        public IEnumerable<BaseEntity> Get<BaseEntity>(Dictionary<string,object> searchData);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="BaseEntity"></typeparam>
        /// <param name="procedure"></param>
        /// <returns>Collection of BaseEntity</returns>
        public IEnumerable<BaseEntity> GetByProcedure<BaseEntity>(string procedure);
        /// <summary>
        /// Join property reference to other properties
        /// </summary>
        /// <typeparam name="BaseEntity">object type</typeparam>
        /// <param name="baseEntity">baseEntity to lookup</param>
        public void JoinOnGet<BaseEntity>(BaseEntity baseEntity);
        /// <summary>
        /// Validation of the user
        /// </summary>
        /// <param name="user">username</param>
        /// <param name="password">the user password</param>
        /// <returns>-1 if not valid</returns>
        public int ValidateUser(string user, string password);
        /// <summary>
        /// Run store procedure and upate session in database
        /// </summary>
        /// <param name="session">session</param>
        /// <param name="sessionID">session id</param>
        public void UpdateSession(string session, int sessionID);
        /// <summary>
        /// Open database Connection 
        /// </summary>
        internal protected void OpenConnection();
        /// <summary>
        /// Close database connection
        /// </summary>
        internal protected void CloseConnection();

    }
}
