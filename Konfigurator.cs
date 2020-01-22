using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Dapper;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace KASA
{
    class Konfigurator
    {
        private static string databaseScript = File.ReadAllText(@"KASA.sql");
        public static string ConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
        public static bool CheckConnection()
        {
            IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionString("cnKASA"));
            try
            {
                connection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void InstallDatabase()
        {
            IEnumerable<string> commandStrings = Regex.Split(databaseScript, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionString("initializer"));
            connection.Open();

            foreach (string commandString in commandStrings)
            {
                if (commandString.Trim() != "")
                {
                    connection.Execute(commandString);
                }
            }

            connection.Close();
        }
    }
}
