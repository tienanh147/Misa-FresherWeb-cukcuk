using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Interfaces.Repository;
using MISA.Core.MISAAttribute;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MISA.Infrastructure.Repository
{
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity>
    {
        private readonly IConfiguration _configuration;
        protected readonly string _connectionString;
        private readonly string _tableName;

        #region Contructor
        public BaseRepository(IConfiguration configuration)
        {
            _tableName = typeof(MISAEntity).Name;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("CukCukDatabaseLocal");
        }
        #endregion

        public string GetNewCode()
        {
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var newCode = dbConnection.Query<string>($"Proc_GetNew{_tableName}Code", commandType: CommandType.StoredProcedure);
                return newCode.First();
            }
        }
        public int Add(MISAEntity entity)
        {

            var properties = entity.GetType().GetProperties().OfType<PropertyInfo>().ToList();
            var columnsName = new List<string>();
            var columnsParam = new List<string>();
            var parameters = new DynamicParameters();
            var propNotMap = GetMappingProperties<MISAEntity>(entity, typeof(MISANotMap));
            List<PropertyInfo> propMapDatabase = properties.Except(propNotMap).ToList();
            foreach (var prop in propMapDatabase)
            {
                var propValue = prop.GetValue(entity);
                var propName = prop.Name;
                columnsName.Add(propName);
                columnsParam.Add($"@{propName}");
                parameters.Add($"@{propName}", propValue);
            }

            var sqlQuery = $"INSERT INTO {_tableName}({String.Join(", ", columnsName.ToArray())}) " +
                            $"VALUES({String.Join(", ", columnsParam.ToArray())})";
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var rowAffects = dbConnection.Execute(sqlQuery, param: parameters);
                return rowAffects;
            }
            throw new NotImplementedException();
        }

        public int DeleteById(Guid entityId)
        {
            var parameter = new DynamicParameters();
            parameter.Add($"@{_tableName}Id", entityId);
            var sqlQuery = $"DELETE FROM {_tableName} WHERE {_tableName}Id = @{_tableName}Id";
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var rowAffect = dbConnection.Execute(sqlQuery, param: parameter);
                return rowAffect;
            }
            throw new NotImplementedException();
            throw new NotImplementedException();
        }

        public IEnumerable<MISAEntity> GetAll()
        {
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                //var sqlQuery = $"SELECT * FROM {typeof(MISAEntity).Name} ORDER BY CreatedDate DESC";
                var entities = dbConnection.Query<MISAEntity>($"Proc_Get{_tableName}s", commandType: CommandType.StoredProcedure);
                return entities;
            }
            throw new NotImplementedException();
        }

        public MISAEntity GetById(Guid entityId)
        {
            var parameter = new DynamicParameters();
            parameter.Add($"@{_tableName}Id", entityId);
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var entities = dbConnection.Query<MISAEntity>($"Proc_Get{_tableName}ById", param: parameter, commandType: CommandType.StoredProcedure);
                if (entities.Count<MISAEntity>() == 0)
                {
                    return default;
                }
                return entities.First();
            }
            throw new NotImplementedException();

        }
        public IEnumerable<MISAEntity> GetByColumn<ColumnType>(ColumnType columnValue, string columnName)
        {
            var parameter = new DynamicParameters();
            parameter.Add($"@{columnName}", columnValue);
            var sqlQuery = $"SELECT * FROM {_tableName} WHERE {columnName} = @{columnName}";
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var entities = dbConnection.Query<MISAEntity>(sqlQuery, param: parameter);
                if (entities.Count() == 0)
                {
                    return default;
                }
                return entities;
            }
            throw new NotImplementedException();

        }

        public int Update(MISAEntity entity, Guid entityId)
        {

            var properties = entity.GetType().GetProperties().OfType<PropertyInfo>().ToList();
            var columnsName = new List<string>();
            var columnsParam = new List<string>();
            var parameters = new DynamicParameters();
            var propNotMap = GetMappingProperties<MISAEntity>(entity, typeof(MISANotMap));
            var propNotUpdate = GetMappingProperties<MISAEntity>(entity, typeof(MISANotUpdate));

            List<PropertyInfo> propMapDatabase = properties.Except(propNotMap).Except(propNotUpdate).ToList();
            var sqlQuery = $"UPDATE {_tableName} entity SET ";
            foreach (var prop in propMapDatabase)
            {
                var propValue = prop.GetValue(entity);
                var propName = prop.Name;
                columnsName.Add(propName);
                columnsParam.Add($"@{propName}");
                sqlQuery += $"entity.{propName} = @{propName}, ";
                parameters.Add($"@{propName}", propValue);
            }
            parameters.Add($"{_tableName}Id", entityId);
            sqlQuery = sqlQuery.Remove(sqlQuery.Length - 2, 1);

            sqlQuery += $"WHERE entity.{_tableName}Id = @{_tableName}Id";

            
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var rowAffects = dbConnection.Execute(sqlQuery, param: parameters);
                return rowAffects;
            }
            throw new NotImplementedException();
        }

        private List<PropertyInfo> GetMappingProperties<Entity>(Entity entity, Type attribute)
        {
            List<PropertyInfo> returnProperties = new List<PropertyInfo>();
            var properties = entity.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propAttributeNotMap = property.GetCustomAttribute(attribute, true);
                if (propAttributeNotMap != null)
                {
                    returnProperties.Add(property);
                }
            }
            return returnProperties;
        }


    }
}
