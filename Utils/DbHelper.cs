using MallMapsApi.Interface;
using Microsoft.AspNetCore.Routing.Template;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

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
                command.CommandText = $"SELECT * FROM {typeof(BaseEntity).Name} WHERE ( {keyString} )";
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
            var jsonElement = JsonSerializer.Serialize(obj, obj.GetType());
            var jsonObject = JObject.Parse(jsonElement);
            string columns = "";
            foreach (var item in jsonObject)
            {
                //Generate columns
                columns += $"@{item.Key},";
                //add paramaters
                sql.Parameters.AddWithValue($"@{item.Key}", item.Value);
            }

            columns = columns.Remove(columns.Length - 1, 1);

            sql.CommandText = $"Insert into {tableName} ({columns})";

            return sql;
        }
    }




}
