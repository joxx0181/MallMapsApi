using MallMapsApi.CustomAttributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;

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
        internal static SqlCommand BuildWhereCommand<BaseEntity>(Dictionary<string, object> keyValuePairs, SqlCommand command)
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

        internal static SqlCommand BuildInsert<BaseEntity>(BaseEntity obj, SqlCommand sql)
        {

            var tableName = typeof(BaseEntity).Name;


            foreach (var propertyInfo in obj.GetType().GetProperties())
            {
                if (propertyInfo.Name.ToLower() == "id")
                    continue;
                //add paramaters
                if (propertyInfo.GetValue(obj) is null)
                    sql.Parameters.AddWithValue(GetColumnName<BaseEntity>(obj, propertyInfo), null);
                else
                    sql.Parameters.AddWithValue(GetColumnName<BaseEntity>(obj, propertyInfo), GetPropertyValue<BaseEntity>(obj, propertyInfo));
            }


            sql.CommandText = $"Insert into {tableName} ({GetColumnNames<BaseEntity>(obj, ',')}) VALUES({GetColumnNames<BaseEntity>(obj, ',', '@')})";
            return sql;
        }

        /// <summary>
        /// Get custom attribute column names, 
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="entity">object</param>
        /// <param name="seperator">Seperator return value example "Name,Description" </param>
        /// <param name="frontChar">Character added in front of column values return example "@name,@description"</param>
        /// <returns>joined string of columns seperate from input seperator</returns>
        internal static string GetColumnNames<Entity>(Entity entity, char seperator, char frontChar = default(char))
        {
            try
            {
                //security check
                if (entity == null)
                    throw new ArgumentNullException("entity is null");
                //Chceks if parameter is set.
                if (frontChar == default(char))
                    return string.Join(seperator, entity.GetType().GetProperties().Where(o => o.GetCustomAttribute<Column>() != null).Select(x => x.GetCustomAttribute<Column>().Name));
                //Get all Properties custom attribute and join them into a string and seperate it with the char from seperator.
                return string.Join(seperator, entity.GetType().GetProperties().Where(o => o.GetCustomAttribute<Column>() != null).Select(x => frontChar + x.GetCustomAttribute<Column>().Name));
            }
            catch (Exception ex)
            {
                throw ex ?? new ArgumentNullException("GetColumnNames threw an execption that was null");
            }
        }
        internal static string GetColumnName<Entity>(Entity entity, PropertyInfo info, char frontChar = default(char))
        {
            try
            {
                //security check
                if (entity == null || info == null)
                    throw new ArgumentNullException("entity is null");
                //Chceks if parameter is set.
                if (frontChar == default(char))
                    return $"@{info.GetCustomAttribute<Column>().Name}";
                return $"{info.GetCustomAttribute<Column>().Name}";
                //Get all Properties custom attribute and join them into a string and seperate it with the char from seperator.
            }
            catch (Exception ex)
            {
                throw ex ?? new ArgumentNullException("GetColumnNames threw an execption that was null");
            }
        }


        internal static object GetPropertyValue<Entity>(Entity entity, PropertyInfo info)
        {
            try
            {
                if (info == null)
                    throw new ArgumentNullException("GetPropertyValue : propertyInfo is null");
                if (entity == null)
                    throw new ArgumentNullException("GetPropertyValue : propertyInfo is null");

                var value = info.GetValue(entity) ?? null;

                return value;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get table custom atribute of the class by using reflection
        /// </summary>
        /// <typeparam name="Entity">Object type</typeparam>
        /// <param name="entity">object</param>
        /// <returns></returns>
        internal static string GetTableName<Entity>(Entity entity)
        {
            try
            {
                //Get custom attribute from class with reflection
                return entity.GetType().GetCustomAttribute<Table>().Name;
            }
            catch (Exception)
            {
                throw new  ArgumentNullException("GetTableName : DataAnnotation not set");
            }
        }

    }
}
