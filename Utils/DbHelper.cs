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
    /// Database helper is for building generic sql statements with mappings
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
        /// <param name="entity">Object to insert  into sql</param>
        /// <param name="sql">Methods will add into sql</param>
        /// <returns>SqlCommand</returns>
        /// <exception cref="ArgumentNullException">Require entity has CustomAttribute Column and Table -> see CustomAttributes folder</exception>
        internal static SqlCommand BuildInsert<BaseEntity>(BaseEntity entity, SqlCommand sql)
        {
            //Get the custom attribute table name 
            var tableName = GetTableName<BaseEntity>(entity);
            //Check if the table name is null or empty 
            if (DataHelper.IsStringNullOrWhiteSpace(tableName))
                throw new ArgumentNullException("Check if table name is set or table name is spelled correctly");
            //Check if entity is null
            if (entity == null)
                throw new ArgumentNullException("baseEntity must not be null!");
            //Check if sqlCommand is null
            if (sql == null)
                throw new ArgumentNullException("baseEntity must not be null!");
            //GetChildren IEnumerable<propertyInfo> for all properties that has an row.
            var ColumnProperties = entity.GetType().GetProperties().Where(o => o.GetCustomAttribute<Column>() != null);
            //if no ColumnProperties found, then its not gonna match our database.
            if (ColumnProperties.Count() == 0)
                throw new ArgumentNullException("BaseEntity does not contain CustomAttributeColumn");
            //loop through each column that contain customattribute column
            foreach (var propInfo in ColumnProperties)
            {
                //by using pattern matching we type testing if the customattribute is type column and then with declaring pattern to get run time parameter
                if (propInfo.GetCustomAttribute<Column>() is Column column)
                {
                    //Get custom attribute from prop propInfo, due to where statement above, i know there is no null attr
                    if (column.IgnoreSql == true)
                        continue;
                    //add paramaters to prevent sql injection
                    if (propInfo.GetValue(entity) is null)
                        sql.Parameters.AddWithValue(GetColumnName<BaseEntity>(entity,propInfo,true), -1);
                    else
                        sql.Parameters.AddWithValue(GetColumnName<BaseEntity>(entity, propInfo,true, '@'), GetPropertyColumnValue<BaseEntity>(entity, propInfo));
                }
            }

            //creating sql command text for insert command
            sql.CommandText = $"Insert into {tableName} ({GetColumnNames<BaseEntity>(entity, ',')}) VALUES({GetColumnNames<BaseEntity>(entity, ',', '@')})";
            return sql;
        }

        /// <summary>
        /// GetChildren custom attribute row names, 
        /// </summary>
        /// <typeparam name="BaseEntity"></typeparam>
        /// <param name="entity">object</param>
        /// <param name="seperator">Seperator return value example "Key,Description" </param>
        /// <param name="frontChar">Character added in front of row values return example "@name,@description"</param>
        /// <returns>joined string of ColumnProperties seperate from input seperator</returns>
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
        /// <param name="entity">object that belong to property objPropInfo</param>
        /// <param name="propInfo">the propertyInfo you want the columnName from</param>
        /// <param name="frontChar">Puts an char in front of the row name</param>
        /// <returns>string value of row name with or without frontchar</returns>
        /// <exception cref="ArgumentNullException">Throw on nulls, No customattribute, customAttribute with IgnoreSql == true</exception>
        internal static string GetColumnName<Entity>(Entity entity, PropertyInfo propInfo,bool ignoresql, char frontChar = default(char))
        {
            try
            {
                //security check
                if (entity == null || propInfo == null)
                    throw new ArgumentNullException("entity is null");
                //We should not recieve and propertyInfo with ignoreSql here
                if (propInfo.GetCustomAttribute<Column>() is Column column)
                {
                    //check if ignoreSql i set, if true return empty string
                    if (column.IgnoreSql == true && ignoresql == true)
                        return string.Empty;

                    //if frontchar is default return without adding char infron of the string.
                    if (frontChar == default(char))
                        return $"{column.Name}";
                    //If frontchar is not default add char to front of string.
                    return $"{frontChar}{column.Name}";
                }
                else //Return empty string if propertyInfo doesnt contain any columns
                    return string.Empty;
            }
            catch (Exception ex)
            {
                //Throw execption, due warning about ex could be null, we making sure that we return our own execption.
                throw ex ?? new ArgumentNullException("GetColumnNames threw an execption that was null");
            }
        }

        /// <summary>
        /// GetChildren property value with security check
        /// </summary>
        /// <typeparam name="BaseEntity">object type</typeparam>
        /// <param name="entity">the base entity object </param>
        /// <param name="propInfo">property propInfo</param>
        /// <returns>Return the value from an property if property has custom attribute row</returns>
        /// <exception cref="ArgumentNullException">Checks for null and customAttribute is set, if ignoreSql = true throw execption</exception>
        internal static object GetPropertyColumnValue<BaseEntity>(BaseEntity entity, PropertyInfo propInfo)
        {
            try
            {
                //Info is not allowed to be null here, if not throw and Execption.
                if (propInfo == null)
                    throw new ArgumentNullException("GetPropertyValue : propertyInfo is null");
                //BaseEntity is not allowed to be null here, if not throw and Execption.
                if (entity == null)
                    throw new ArgumentNullException("GetPropertyValue : propertyInfo is null");
                //Info should contain customAttribute, if not throw and Execption
                if (propInfo.GetCustomAttribute<Column>() is Column column)
                {
                    //We should not recieve and propertyInfo with ignoreSql here
                    if (column.IgnoreSql == true)
                        throw new Exception("GetColumnName : Custom attribute Ignoresql found.");

                    //GetChildren value from propertyInfo from the object parsed, if value is null return null value
                    return propInfo.GetValue(entity) ?? null;
                }
                else
                    throw new ArgumentNullException("GetPropertyValue : CustomAttribute Column is null");

            }
            catch (Exception ex)
            {
                //TODO : LOG execption
                return null;
            }
        }

        /// <summary>
        /// GetChildren table custom atribute of the class by using reflection
        /// </summary>
        /// <typeparam name="BaseEntity">Object type</typeparam>
        /// <param name="entity">object</param>
        /// <returns></returns>
        internal static string GetTableName<BaseEntity>(BaseEntity entity)
        {
            //GetChildren custom attribute from class with reflection
            if (entity.GetType().GetCustomAttribute<Table>() is Table table)
                return table.Name;
            throw new ArgumentNullException($"GetTableName : {nameof(entity)} table annotation is empty or nullw");
        }

        /// <summary>
        /// Convert DataRow to entity
        /// </summary>
        /// <param name="row">Data row to convert</param>
        /// <param name="type">Type u want back</param>
        /// <returns>Object of type parsed</returns>
        /// <exception cref="ArgumentNullException">Return null execptions</exception>
        internal static object DataRowToObject(bool ignoreSql,DataRow row, Type type)
        {
            //If datarow entered is null, throw null execption
            if (row == null)
                throw new ArgumentNullException("DataRowToObject : DataRow is null");
            //Create an instance of given type
            var obj = Activator.CreateInstance(type);
            //if obj is null throw null execption
            if (obj == null)
                return new ArgumentNullException($"DataRowToObject : Failed creating an instance of {type.Name}");
            //Get all propertyInfo from obj where it contains custom attribute and ignore sql is false. 
            var objPropertyInfos = obj.GetType().GetProperties().Where(x => x.GetCustomAttribute<Column>() != null && x.GetCustomAttribute<Column>().IgnoreSql == false);
            //run through each dataColumn from the parsing DataRow
            foreach (DataColumn column in row.Table.Columns)
            {
                //GetChildren properties 
                foreach (var objPropInfo in objPropertyInfos)
                {
                    //GetChildren ColumName
                    var columnName = GetColumnName<object>(type, objPropInfo,ignoreSql);
                    //Copy fields from Datacolumn to row
                    if (columnName == column.ColumnName)
                        objPropInfo.SetValue(obj, row[columnName]);
                }
            }
            //Return generic object of type entity
            return obj;
        }
        /// <summary>
        /// Convert DataRow to generic type parameter
        /// </summary>
        /// <typeparam name="BaseEntity">type parameter</typeparam>
        /// <param name="row">DataRow</param>
        /// <param name="ignoreSql">If true ignore sql attributes</param>
        /// <returns>object of type from typeparam</returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static BaseEntity DataRowToBaseEntity<BaseEntity>(DataRow row, bool ignoreSql)
        {
            try
            {
                //Throw if null
                if (row.Table == null)
                    throw new ArgumentNullException("Row is null");
                //Create new instance of type.
                BaseEntity obj = Activator.CreateInstance<BaseEntity>();
                if (obj == null)
                    throw new ArgumentNullException($"DataRowToBaseEntity : failed creating an instance of {nameof(BaseEntity)}");
                //Creating an IEnumerable for poperty infos
                IEnumerable<PropertyInfo> propInfos;
                //if ignore sql get all where custom attribute is not null and all colums where ignoreSql is false 
                if (ignoreSql)
                    propInfos = obj.GetType().GetProperties().Where(x => x.GetCustomAttribute<Column>() != null && x.GetCustomAttribute<Column>().IgnoreSql == false);
                else // else get all properties where CustomAttribute is column
                    propInfos = obj.GetType().GetProperties().Where(x => x.GetCustomAttribute<Column>() != null);

                //Run through all Columns
                foreach (DataColumn column in row.Table.Columns)
                {
                    //GetChildren properties 
                    foreach (var propInfo in propInfos)
                    {
                        //GetChildren ColumNames
                        var columnName = GetColumnName<BaseEntity>(obj, propInfo,ignoreSql);
                        //Get Column names 
                        if (columnName.IsStringNullOrWhiteSpace())
                            continue;
                        //set property value from Datacolumn to PropInfo
                        if (columnName == column.ColumnName)
                            propInfo.SetValue(obj, Convert.ChangeType(row[columnName], propInfo.PropertyType), null);

                    }
                }
                //Return object with added values from datarow
                return obj;

            }
            catch (Exception ex)
            {
                throw ex ?? new ArgumentNullException("GetAsEntity threw an execption");
            }
        }
        /// <summary>
        /// Convert DataTAble to list of object from Type
        /// </summary>
        /// <param name="table"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static IEnumerable<object> ConvertToObjectCollection(bool ignoreSql,DataTable table, Type type = default(Type))
        {
            //Creating an list of objects
            List<object> entities = new List<object>();
            //Looping through each DataTable rows
            foreach (DataRow row in table.Rows)
            {
                //Converting DataRwo to object 
                var entity = DataRowToObject(ignoreSql,row, type);
                //Adding eneity to list if not null
                if (entity != null)
                    entities.Add(entity);
            }
            //Returning enitites
            return entities;
        }
        /// <summary>
        /// Convert DataTable to IEnumerable of typeParam
        /// </summary>
        /// <typeparam name="BaseEntity">TypeParam object type</typeparam>
        /// <param name="table">DataTable to map</param>
        /// <param name="ignoreSql">Use ignoreSql annotation?</param>
        /// <returns></returns>
        internal static IEnumerable<BaseEntity> ConvertToBaseEntity<BaseEntity>(DataTable table, bool ignoreSql)
        {
            try
            {
                //Creating an list of entities
                List<BaseEntity> entities = new List<BaseEntity>();
                //Running through all Datatable Rows
                foreach (DataRow row in table.Rows)
                {
                    //Convert DataRowToBaseEntity
                    var entity = DataRowToBaseEntity<BaseEntity>(row, ignoreSql);
                    //if entity is null skip it
                    if (entity == null)
                        continue;
                    //Add to list
                    entities.Add(entity);
                }
                //Return found entities
                return entities;
            }
            catch (Exception ex)
            {
                throw ex ?? new Exception("ConvertToBaseEntity : threw an execption that was null");
            }
        }


    }
}
