using LandmarkRemark.CrossCutting;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SqlClient;

namespace LandmarkRemark.Infrastructure
{
    class ConnectionFactory : IConnectionFactory
    {
        private readonly IOptions<DatabaseSettings> connectionSettings;

        public ConnectionFactory(IOptions<DatabaseSettings> connectionSettings)
        {
            this.connectionSettings = connectionSettings;
        }


        public IDbConnection CreateConnection()
        {
             string connectionString = connectionSettings.Value.ConnectionString;

            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
