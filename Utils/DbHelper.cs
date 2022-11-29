using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;


namespace MallMapsApi.Utils
{
    /// <summary>
    /// Database helper
    /// </summary>
    public static class DbHelper
    {
        /// <summary>
        /// Creates and build where command by the keyValuePairs
        /// </summary>
        /// <typeparam name="BaseEntity"></typeparam>
        /// <param name="keyValuePairs"></param>
        /// <param name="command"></param>
        public static SqlCommand BuildWhereCommand<BaseEntity>(Dictionary<string, object> keyValuePairs, SqlCommand command)
        {
            try
            {
                //string for keyString
                string keyString = "";
                //loop through each key and value in keyValuePairs

                foreach (var diction in keyValuePairs)
                {
                    //Adding an @ColumnName = 
                    if (diction.Key == keyValuePairs.Last().Key)
                        keyString += $"{diction.Key}=@{diction.Key}";
                    else
                        keyString += $"{diction.Key}=@{diction.Key},";
                    //Add parameters
                    command.Parameters.AddWithValue($"@{diction.Key}", diction.Value);
                }
                //complete the command
                command.CommandText = $"SELECT * FROM {typeof(BaseEntity).Name} WHERE ({keyString})";
                //return the command
                return command;
            }
            catch (Exception ex)
            {
                //TODO : Log execption
                return null;
            }

        }

        public static SqlCommand BuildInsert<BaseEntity>(BaseEntity obj, SqlCommand sql)
        {

            var tableName = typeof(BaseEntity).Name;
            string columnNames = "";
            string ColumnValueNames = "";

            foreach (var item in obj.GetType().GetProperties())
            {
                //Generate columnNames
                columnNames += $"{item.Name},";
                ColumnValueNames += $"@{item.Name},";
                //add paramaters
                sql.Parameters.AddWithValue($"@{item.Name}", item.GetValue(obj));
            }

            columnNames = columnNames.Remove(columnNames.Length - 1, 1);
            ColumnValueNames = ColumnValueNames.Remove(ColumnValueNames.Length - 1, 1);
            sql.CommandText = $"Insert into {tableName} ({columnNames}) VALUES({ColumnValueNames})";
            return sql;
        }
    }


    

}
