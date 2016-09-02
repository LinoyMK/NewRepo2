#define AVOID_SQL

using Search.Infrastructure.Configuration.Abstract.Providers;
using Search.Infrastructure.Security.Abstract.Cryptography;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Search.Infrastructure.Configuration.Providers
{
    public class DatabaseProvider : IDatabaseProvider
    {
        private static Lazy<Dictionary<string, string>> _settings;

        private readonly ICryptographicProvider _cryptographicProvider;

        public DatabaseProvider(ICryptographicProvider cryptographicProvider)
        {
            _cryptographicProvider = cryptographicProvider;
            _settings = new Lazy<Dictionary<string, string>>(ProcessAll);
        }

        public T Get<T>(string key)
        {
            try
            {
                var appSetting = _settings.Value[key];
                if (string.IsNullOrWhiteSpace(appSetting))
                {
                    throw new Exception(String.Format("The key {0} was found, but it was empty", key));
                }

                var converter = TypeDescriptor.GetConverter(typeof(T));
                return (T)(converter.ConvertFromInvariantString(appSetting));
            }
            catch (Exception)
            {
                // This is to allow to execute the application without connection to the SQL database. Only for debug purpuses.
#if AVOID_SQL
                return default(T);
#else
                    throw new KeyNotFoundException(key, ex);
#endif
            }
        }

        public Dictionary<string, string> GetAll()
        {
            return _settings.Value;
        }

        private string Password()
        {
            const string key = "passPhrase";
            return ConfigurationManager.AppSettings[key];
        }

        private string ConnectionString()
        {
            const string key = "FrontiersConfigConnectionString";
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

        private DataTable ReadAll()
        {
            const string key = "FSP_Configuration_Data";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(ConnectionString());
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(key)
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = sqlConnection
                };

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);


                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                sqlConnection.Close();

                return dataTable;
            }
            catch (Exception)
            {
                // This is to allow to execute the application without connection to the SQL database. Only for debug purpuses.
#if AVOID_SQL
                return new DataTable();
#else
                    throw new Exception("Unable to get the configuration from the database.", exception);
#endif
            }
        }

        private Dictionary<string, string> ProcessAll()
        {
            const string key = "Key";
            const string value = "Value";

            DataTable dataTable = ReadAll();
            string password = Password();

            Dictionary<string, string> appSettings = new Dictionary<string, string>();

            for (int Row = 0; Row < dataTable.Rows.Count; Row++)
            {
                appSettings.Add(Convert.ToString(dataTable.Rows[Row][key]), _cryptographicProvider.Decrypt(Convert.ToString(dataTable.Rows[Row][value]), password));
            }

            return appSettings;
        }
    }
}
