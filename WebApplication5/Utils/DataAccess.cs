using Microsoft.AspNetCore.Mvc;
using SchoolAdministration.Services;
using Newtonsoft.Json;
using System.Data;

using MySql.Data.MySqlClient;


namespace SchoolAdministration.Utils
{
    public class DataAccess
    {
        private readonly string configuration;
        
        public DataAccess(string _configuration)
        {
            configuration = _configuration;
            
        }
        public async Task<string> ExecuteQuery(string Query)
        {
            DataTable table = new DataTable();
            //string sqldatasource = configuration.GetConnectionString("MyConnectionString");
            MySqlDataReader reader;
            using (MySqlConnection connection = new MySqlConnection(configuration))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(Query, connection))
                {
                    reader =  command.ExecuteReader();
                    table.Load(reader);
                    reader.Close();
                    connection.Close();
                }
            }
            
            if(table.Rows.Count > 0)
            {
                return (JsonConvert.SerializeObject(table));
            }
            return null;

        }
    }

}
