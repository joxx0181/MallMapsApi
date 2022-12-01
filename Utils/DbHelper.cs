using MallMapsApi.CustomAttributes;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
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
        /// <summary>
        /// Build sql insert command use AddWithValues to prevent sql injection
        /// </summary>
        /// <typeparam name="BaseEntity">object type </typeparam>
        /// <param name="baseEntity">Object to insert  into sql</param>
        /// <param name="sql">Methods will add into sql</param>
        /// <returns>SqlCommand</returns>
        /// <exception cref="ArgumentNullException">Require baseEntity has CustomAttribute Column and Table -> see CustomAttributes folder</exception>
        internal static SqlCommand BuildInsert<BaseEntity>(BaseEntity baseEntity, SqlCommand sql)
        {
            //GetChildren the db table name
            var tableName = GetTableName<BaseEntity>(baseEntity);
            if (DataHelper.IsStringNullOrWhiteSpace(tableName))
                throw new ArgumentNullException("Check if table name is set or table name is spelled correctly");
            //Check if baseEntity is null
            if (baseEntity == null)
                throw new ArgumentNullException("baseEntity must not be null!");
            //Check if sqlCommand is null
            if (sql == null)
                throw new ArgumentNullException("baseEntity must not be null!");
            //GetChildren IEnumerable<propertyInfo> for all properties that has an row.
            var columns = baseEntity.GetType().GetProperties().Where(o => o.GetCustomAttribute<Column>() != null);
            //if no columns found, then its not gonna match our database.
            if (columns.Count() == 0)
                throw new ArgumentNullException("BaseEntity does not contain CustomAttributeColumn");

            foreach (var propInfo in columns)
            {
                if (propInfo.GetCustomAttribute<Column>().IgnoreSql == true)
                    continue;
                //add paramaters
                if (propInfo.GetValue(baseEntity) is null)
                    sql.Parameters.AddWithValue(GetColumnName<BaseEntity>(baseEntity, propInfo), -1);
                else
                    sql.Parameters.AddWithValue(GetColumnName<BaseEntity>(baseEntity, propInfo, '@'), GetPropertyValue<BaseEntity>(baseEntity, propInfo));
            }


            sql.CommandText = $"Insert into {tableName} ({GetColumnNames<BaseEntity>(baseEntity, ',')}) VALUES({GetColumnNames<BaseEntity>(baseEntity, ',', '@')})";
            return sql;
        }

        /// <summary>
        /// GetChildren custom attribute row names, 
        /// </summary>
        /// <typeparam name="BaseEntity"></typeparam>
        /// <param name="entity">object</param>
        /// <param name="seperator">Seperator return value example "Key,Description" </param>
        /// <param name="frontChar">Character added in front of row values return example "@name,@description"</param>
        /// <returns>joined string of columns seperate from input seperator</returns>
        internal static string GetColumnNames<BaseEntity>(BaseEntity entity, char seperator, char frontChar = default(char))
        {
            try
            {
                //security check
                if (entity == null)
                    throw new ArgumentNullException("entity is null");

                //GetChildren propertyInfo where Column is not null and SqlIgnore is false
                var validPropertyInfo = entity.GetType().GetProperties().Where(o => o.GetCustomAttribute<Column>() != null && o.GetCustomAttribute<Column>().IgnoreSql == false);
                //If no propertyInfo found, the insert does not match DB!
                if (validPropertyInfo.Count() == 0)
                    throw new ArgumentNullException($"{typeof(BaseEntity).Name} has no custom properties");
                // if frontchar is default, we dont wish to add custom character in front of the attribute name
                if (frontChar == default(char))
                    //GetChildren all Properties custom attribute and join them into a string and seperate it with the char from seperator.
                    return string.Join(seperator, validPropertyInfo.Select(x => x.GetCustomAttribute<Column>().Name));
                //GetChildren all Properties custom attribute and join them into a string and add custom character in fron of the row name.
                return string.Join(seperator, validPropertyInfo.Select(x => frontChar + x.GetCustomAttribute<Column>().Name));
            }
            catch (Exception ex)
            {
                //if for some reason the exception is null, be sure that throw an execption of wich method failed
                throw ex ?? new ArgumentNullException("GetColumnNames threw an execption that was null");
            }
        }

        /// <summary>
        /// GetChildren row name from custom property from dataanotation. 
        /// </summary>
        /// <typeparam name="Entity">the type of the object</typeparam>
        /// <param name="entity">object that belong to property propInfo</param>
        /// <param name="info">the propertyInfo you want the columnName from</param>
        /// <param name="frontChar">Puts an char in front of the row name</param>
        /// <returns>string value of row name with or without frontchar</returns>
        /// <exception cref="ArgumentNullException">Throw on nulls, No customattribute, customAttribute with IgnoreSql == true</exception>
        internal static string GetColumnName<Entity>(Entity entity, PropertyInfo info, char frontChar = default(char))
        {
            try
            {
                //security check
                if (entity == null || info == null)
                    throw new ArgumentNullException("entity is null");
                //We should not recieve and propertyInfo with ignoreSql here
                if (info.GetCustomAttribute<Column>().IgnoreSql == true)
                    throw new Exception("GetColumnName : Custom attribute Ignoresql found.");
                //Chceks if parameter is set.
                if (frontChar == default(char))
                    return $"{info.GetCustomAttribute<Column>().Name}";
                return $"{frontChar}{info.GetCustomAttribute<Column>().Name}";
                //GetChildren all Properties custom attribute and join them into a string and seperate it with the char from seperator.
            }
            catch (Exception ex)
            {
                throw ex ?? new ArgumentNullException("GetColumnNames threw an execption that was null");
            }
        }

        /// <summary>
        /// GetChildren property value with security check
        /// </summary>
        /// <typeparam name="BaseEntity">object type</typeparam>
        /// <param name="baseEntity">the base entity object </param>
        /// <param name="propInfo">property info</param>
        /// <returns>Return the value from an property if property has custom attribute row</returns>
        /// <exception cref="ArgumentNullException">Checks for null and customAttribute is set, if ignoreSql = true throw execption</exception>
        internal static object GetPropertyValue<BaseEntity>(BaseEntity baseEntity, PropertyInfo propInfo)
        {
            try
            {


                //Info is not allowed to be null here, if not throw and Execption.
                if (propInfo == null)
                    throw new ArgumentNullException("GetPropertyValue : propertyInfo is null");
                //BaseEntity is not allowed to be null here, if not throw and Execption.
                if (baseEntity == null)
                    throw new ArgumentNullException("GetPropertyValue : propertyInfo is null");
                //Info should contain customAttribute, if not throw and Execption
                if (propInfo.GetCustomAttribute<Column>() == null)
                    throw new ArgumentNullException("GetPropertyValue : CustomAttribute Column is null");
                //We should not recieve and propertyInfo with ignoreSql here
                if (propInfo.GetCustomAttribute<Column>().IgnoreSql == true)
                    throw new Exception("GetColumnName : Custom attribute Ignoresql found.");
                //GetChildren value from propertyInfo from the object parsed
                var value = propInfo.GetValue(baseEntity) ?? null;

                return value;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// GetChildren table custom atribute of the class by using reflection
        /// </summary>
        /// <typeparam name="Entity">Object type</typeparam>
        /// <param name="entity">object</param>
        /// <returns></returns>
        internal static string GetTableName<Entity>(Entity entity)
        {
            try
            {
                //GetChildren custom attribute from class with reflection
                return entity.GetType().GetCustomAttribute<Table>().Name;
            }
            catch (Exception)
            {
                throw new ArgumentNullException("GetTableName : DataAnnotation not set");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="BaseEntity"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static object DataRowToBaseEntity(DataRow row, Type type)
        {
            if (row == null)
                throw new ArgumentNullException("Row is null");
            var obj = Activator.CreateInstance(type);
            var propInfos = obj.GetType().GetProperties().Where(x => x.GetCustomAttribute<Column>() != null && x.GetCustomAttribute<Column>().IgnoreSql == false);
            foreach (DataColumn column in row.Table.Columns)
            {
                //GetChildren properties 
                foreach (var propInfo in propInfos)
                {
                    //GetChildren ColumName
                    var columnName = GetColumnName<object>(type, propInfo);
                    //Copy fields from Datacolumn to row
                    if (columnName == column.ColumnName)
                        propInfo.SetValue(obj, row[columnName]);
                }
            }
            //Return generic object of type entity
            return obj;
        }
        internal static BaseEntity DataRowToBaseEntity<BaseEntity>(DataRow row, bool ignoreSql)
        {
            try
            {
                //Throw if null
                if (row.Table == null)
                    throw new ArgumentNullException("Row is null");
                //Create new instance of type.
                BaseEntity obj = Activator.CreateInstance<BaseEntity>();
                IEnumerable<PropertyInfo> propInfos;
                if (ignoreSql)
                    propInfos = obj.GetType().GetProperties().Where(x => x.GetCustomAttribute<Column>() != null && x.GetCustomAttribute<Column>().IgnoreSql == false);
                else
                    propInfos = obj.GetType().GetProperties().Where(x => x.GetCustomAttribute<Column>() != null);

                //Run through all Columns
                foreach (DataColumn column in row.Table.Columns)
                {
                    //GetChildren properties 
                    foreach (var propInfo in propInfos)
                    {
                        //GetChildren ColumName
                        var columnName = GetColumnName<BaseEntity>(obj, propInfo);
                        //Copy fields from Datacolumn to row
                        if (columnName == column.ColumnName)
                            propInfo.SetValue(obj, row[columnName]);
                    }
                }
                //Return generic object of type entity
                return obj;

            }
            catch (Exception ex)
            {
                throw ex ?? new ArgumentNullException("GetAsEntity threw an execption");
            }
        }

        internal static IEnumerable<object> ConvertToBaseEntity(DataTable table, Type type = default(Type))
        {
            List<object> entities = new List<object>();
            foreach (DataRow row in table.Rows)
            {
                var entity = DataRowToBaseEntity(row, type);
                if (entity != null)
                    entities.Add(entity);
            }
            return entities;


        }
        internal static IEnumerable<BaseEntity> ConvertToBaseEntity<BaseEntity>(DataTable table, bool ignoreSql)
        {
            try
            {


                List<BaseEntity> entities = new List<BaseEntity>();

                foreach (DataRow row in table.Rows)
                {
                    var entity = DataRowToBaseEntity<BaseEntity>(row, ignoreSql);
                    if (entity != null)
                        entities.Add(entity);
                }
                return entities;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
