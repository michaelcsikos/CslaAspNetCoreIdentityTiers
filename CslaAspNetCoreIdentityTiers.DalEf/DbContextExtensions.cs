using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CslaAspNetCoreIdentityTiers.DalEf
{
    public static class DbContextExtensions
    {
        #region Constants

        public const string NULL           = "NULL",
                            SET_NOCOUNT_ON = "SET NOCOUNT ON";

        #endregion

        #region Insert

        public static string GenerateInsertCommand<T>(this DbContext db, T data)
            where T : class
        {
            var dataType        = data.GetType();
            var tableDefinition = db.GetTableDefinition(dataType);
            var builderInsert   = new StringBuilder();
            var builderValues   = new StringBuilder();

            builderInsert.Append    ("INSERT INTO ");
            builderInsert.AppendLine(tableDefinition.TableName);
            builderInsert.AppendLine("(");

            builderValues.AppendLine("VALUES");
            builderValues.AppendLine("(");

            bool isFirst = true;

            foreach (var property in tableDefinition.Properties)
            {
                if (isFirst)
                    isFirst = false;
                else
                {
                    builderInsert.Append(",");
                    builderValues.Append(",");
                }
            
                builderInsert.Append("[");
                builderInsert.Append(property.Relational().ColumnName);
                builderInsert.Append("]");
                builderInsert.AppendLine();
            
                var memberAccessor = dataType.GetProperty(property.Name);
                var obj            = memberAccessor.GetValue(data, null);
            
                AppendUnboxedValue(builderValues, obj, memberAccessor.PropertyType);
            
                builderValues.AppendLine();
            }

            builderInsert.AppendLine(")");
            builderValues.AppendLine(")");

            builderInsert.AppendLine(builderValues.ToString());

            var command = builderInsert.ToString();

            return command;
        }

        #endregion

        #region Update

        public static string GenerateUpdateCommand<T>(this DbContext db, T data)
            where T : class
        {
            var dataType        = data.GetType();
            var tableDefinition = db.GetTableDefinition(dataType);
            var builderUpdate   = new StringBuilder();
            var builderWhere    = new StringBuilder();

            builderUpdate.Append    ("UPDATE ");
            builderUpdate.AppendLine(tableDefinition.TableName);
            builderUpdate.AppendLine("SET");

            builderWhere .AppendLine("WHERE");

            bool isFirstUpdate = true,
                 isFirstWhere  = true;

            foreach (var property in tableDefinition.Properties)
            {
                StringBuilder builder;
            
                if (tableDefinition.IsPrimaryKey(property) || property.IsConcurrencyToken)
                {
                    builder = builderWhere;
            
                    if (isFirstWhere)
                        isFirstWhere = false;
                    else
                        builder.Append("AND ");
                }
                else
                {
                    builder = builderUpdate;
            
                    if (isFirstUpdate)
                        isFirstUpdate = false;
                    else
                        builder.Append(",");
                }
            
                builder.Append("[");
                builder.Append(property.Relational().ColumnName);
                builder.Append("] = ");
            
                var memberAccessor = dataType.GetProperty(property.Name);
                var obj            = memberAccessor.GetValue(data, null);
            
                AppendUnboxedValue(builder, obj, memberAccessor.PropertyType);
            
                builder.AppendLine();
            }

            builderUpdate.Append(builderWhere.ToString());

            var command = builderUpdate.ToString();

            return command;
        }

        #endregion

        #region Stored procedure

        public static string GenerateStoredProcedureCommand(this DbContext db, string storedProcedure, params object[] parameters)
        {
            var builder = new StringBuilder();

            builder.Append("EXEC ");
            builder.Append(storedProcedure);
            builder.Append(" ");

            bool isFirst = true;

            foreach (var parameter in parameters)
            {
                if (isFirst)
                    isFirst = false;
                else
                    builder.Append(",");

                AppendUnboxedValue(builder, parameter, parameter.GetType());
            }

            builder.AppendLine();

            return builder.ToString();
        }

        #endregion

        #region Delete

        public static string GenerateDeleteCommand<T, P>(this DbContext db, P primaryKey)
            where T : class
        {
            var dataType        = typeof(T);
            var tableDefinition = db.GetTableDefinition(dataType);
            var builder         = new StringBuilder();

            builder.Append("DELETE FROM ");
            builder.Append(tableDefinition.TableName);
            builder.Append(" WHERE ");
            builder.Append(tableDefinition.PrimaryKey);
            builder.Append(" = ");

            AppendUnboxedValue(builder, primaryKey, typeof(P));

            return builder.ToString();
        }

        public static void DeleteByPK<T, P>(this DbContext db, P primaryKey)
            where T : class
        {
            var command = db.GenerateDeleteCommand<T, P>(primaryKey);

            db.Database.ExecuteSqlCommand(command);
        }

        public static void DeleteByPK<T>(this DbContext db, Guid primaryKey)
            where T : class
        {
            db.DeleteByPK<T, Guid>(primaryKey);
        }

        public static void DeleteByPK<T>(this DbContext db, DbSet<T> dbSet, Guid primaryKey)
            where T : class
        {
            db.DeleteByPK<T, Guid>(primaryKey);
        }

        #endregion

        #region Append unboxed value

        private static void AppendUnboxedValue(StringBuilder builder, object obj, Type type)
        {
            if (type == typeof(bool))
                builder.Append((bool)obj ? 1 : 0);

            else if (type == typeof(byte))
                builder.Append((byte)obj);

            else if (type == typeof(char))
            {
                builder.Append("'");
                builder.Append(EscapeChar((char)obj));
                builder.Append("'");
            }

            else if (type == typeof(char[]))
            {
                builder.Append("'");
                builder.Append(EscapeChars((char[])obj));
                builder.Append("'");
            }

            else if (type == typeof(DateTime))
                builder.Append(ToSqlDateTime2((DateTime)obj));

            else if (type == typeof(decimal))
                builder.Append((decimal)obj);

            else if (type == typeof(double))
                builder.Append((double)obj);

            else if (type == typeof(float))
                builder.Append((float)obj);

            else if (type == typeof(Guid))
            {
                builder.Append("'");
                builder.Append((Guid)obj);
                builder.Append("'");
            }

            else if (type == typeof(int))
                builder.Append((int)obj);

            else if (type == typeof(long))
                builder.Append((long)obj);

            else if (type == typeof(sbyte))
                builder.Append((sbyte)obj);

            else if (type == typeof(short))
                builder.Append((short)obj);

            else if (type == typeof(string))
            {
                var value = (string)obj;

                if (value != null)
                {
                    builder.Append("'");
                    builder.Append(EscapeString(value));
                    builder.Append("'");
                }
                else
                    builder.Append(NULL);
            }

            else if (type == typeof(uint))
                builder.Append((uint)obj);

            else if (type == typeof(ulong))
                builder.Append((ulong)obj);

            else if (type == typeof(ushort))
                builder.Append((ushort)obj);

            // Nullable types
            else if (type == typeof(bool?))
            {
                var value = (bool?)obj;

                if (value.HasValue)
                    builder.Append(value.Value ? 1 : 0);
                else
                    builder.Append(NULL);
            }

            else if (type == typeof(byte?))
            {
                var value = (byte?)obj;

                if (value.HasValue)
                    builder.Append(value.Value);
                else
                    builder.Append(NULL);
            }

            else if (type == typeof(char?))
            {
                builder.Append("'");
                builder.Append((char?)obj);
                builder.Append("'");
            }

            else if (type == typeof(DateTime?))
            {
                var value = (DateTime?)obj;

                if (value.HasValue)
                    builder.Append(ToSqlDateTime2(value.Value));
                else
                    builder.Append(NULL);
            }

            else if (type == typeof(decimal?))
            {
                var value = (decimal?)obj;

                if (value.HasValue)
                    builder.Append(value.Value);
                else
                    builder.Append(NULL);
            }

            else if (type == typeof(double?))
            {
                var value = (double?)obj;

                if (value.HasValue)
                    builder.Append(value.Value);
                else
                    builder.Append(NULL);
            }

            else if (type == typeof(float?))
            {
                var value = (float?)obj;

                if (value.HasValue)
                    builder.Append(value.Value);
                else
                    builder.Append(NULL);
            }

            else if (type == typeof(Guid?))
            {
                var value = (Guid?)obj;

                if (value.HasValue)
                {
                    builder.Append("'");
                    builder.Append(value.Value);
                    builder.Append("'");
                }
                else
                    builder.Append(NULL);
            }

            else if (type == typeof(int?))
            {
                var value = (int?)obj;

                if (value.HasValue)
                    builder.Append(value.Value);
                else
                    builder.Append(NULL);
            }

            else if (type == typeof(long?))
            {
                var value = (int?)obj;

                if (value.HasValue)
                    builder.Append(value.Value);
                else
                    builder.Append(NULL);
            }

            else if (type == typeof(sbyte?))
            {
                var value = (sbyte?)obj;

                if (value.HasValue)
                    builder.Append(value.Value);
                else
                    builder.Append(NULL);
            }

            else if (type == typeof(short?))
            {
                var value = (short?)obj;

                if (value.HasValue)
                    builder.Append(value.Value);
                else
                    builder.Append(NULL);
            }

            else if (type == typeof(uint?))
            {
                var value = (uint?)obj;

                if (value.HasValue)
                    builder.Append(value.Value);
                else
                    builder.Append(NULL);
            }

            else if (type == typeof(ulong?))
            {
                var value = (ulong?)obj;

                if (value.HasValue)
                    builder.Append(value.Value);
                else
                    builder.Append(NULL);
            }

            else if (type == typeof(ushort?))
            {
                var value = (ushort?)obj;

                if (value.HasValue)
                    builder.Append(value.Value);
                else
                    builder.Append(NULL);
            }

            else
                builder.Append(NULL);
        }

        public static string ToSqlDateTime2(DateTime value)
        {
            return "'" + value.ToString("yyyy-MM-dd HH:mm:ss.fffffff") + "'";
        }

        public static string EscapeString(string value)
        {
            return value.Replace("'", "''");
        }

        public static string EscapeChar(char value)
        {
            if (value == '\'')
                return "''";

            return value.ToString();
        }

        public static string EscapeChars(char[] values)
        {
            var builder = new StringBuilder();

            foreach (var c in values)
            {
                var text = EscapeChar(c);

                builder.Append(text);
            }

            return builder.ToString();
        }

        #endregion

        #region Table definition

        static Dictionary<Type, EfTableDefinition> s_cache = new Dictionary<Type, EfTableDefinition>();

        public static EfTableDefinition GetTableDefinition(this DbContext db, Type type)
        {
            EfTableDefinition tableDefinition = null;

            lock (s_cache)
            {
                if (!s_cache.TryGetValue(type, out tableDefinition))
                {
                    foreach (var entityType in db.Model.GetEntityTypes())
                    {
                        if (entityType.ClrType != type)
                            continue;

                        var tableName         = "[" + entityType.Relational().TableName + "]";

                        var primaryKeyMembers = entityType.FindPrimaryKey().Properties.ToArray();
                        var primaryKeyMember  = primaryKeyMembers[0];
                        var primaryKeyName    = primaryKeyMember.PropertyInfo.Name;

                        var properties        = entityType.GetProperties().ToArray();

                        tableDefinition       = new EfTableDefinition(tableName, primaryKeyName, properties, primaryKeyMembers);

                        s_cache[type]         = tableDefinition;

                        break;
                    }
                }

                return tableDefinition;
            }
        }

        #endregion

        #region To SQL parameter

        public static SqlParameter NewSqlParameter(this DbContext db, string parameterName, object value)
        {
            if (value != null)
                return new SqlParameter(parameterName, value);

            return new SqlParameter(parameterName, DBNull.Value);
        }

        public static SqlParameter NewSqlParameter<T>(this DbContext db, string parameterName, T? value)
            where T : struct
        {
            if (value.HasValue)
                return new SqlParameter(parameterName, value.Value);

            return new SqlParameter(parameterName, DBNull.Value);
        }

        #endregion
    }

    #region Ef table definition

    public class EfTableDefinition
    {
        public string      TableName         { get; private set; }
        public string      PrimaryKey        { get; private set; }
        public IProperty[] Properties        { get; private set; }
        public IProperty[] PrimaryKeyMembers { get; private set; }

        public bool IsPrimaryKey(IProperty property)
        {
            foreach (var member in PrimaryKeyMembers)
            {
                if (member == property)
                    return true;
            }

            return false;
        }

        public EfTableDefinition(string tableName, string primaryKey, IProperty[] properties, IProperty[] primaryKeyMembers)
        {
            TableName         = tableName;
            PrimaryKey        = primaryKey;
            Properties        = properties;
            PrimaryKeyMembers = primaryKeyMembers;
        }
    }

    #endregion
}
