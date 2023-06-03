using Microsoft.AspNetCore.Mvc;
using SchoolAdministration.DTO;
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

        public async Task<bool> ExecuteQueryWithParamsStudents(string Query, List<Student> Students)
        {
            try
            {
                foreach (var student in Students)
                {
                    using (var connection = new MySqlConnection(configuration))
                    {
                        var command = new MySqlCommand(Query, connection);
                        command.Parameters.AddWithValue("@Student_Id", student.Student_Id);
                        command.Parameters.AddWithValue("@Student_Grade", student.Student_Grade);
                        command.Parameters.AddWithValue("@Student_Address", student.Student_Address);
                        command.Parameters.AddWithValue("@Student_Name", student.Student_Name);
                        command.Parameters.AddWithValue("@Student_ZipCode", student.Student_ZipCode);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<bool> ExecuteQueryWithParamsStaff(string Query, List<Staff> Staff)
        {
            try
            {
                foreach (var staff in Staff)
                {
                    using (var connection = new MySqlConnection(configuration))
                    {
                        var command = new MySqlCommand(Query, connection);
                        command.Parameters.AddWithValue("@Staff_Id", staff.Staff_Id);
                        command.Parameters.AddWithValue("@Staff_Id", staff.Staff_Id);
                        command.Parameters.AddWithValue("@Staff_Address", staff.Staff_Address);
                        command.Parameters.AddWithValue("@Staff_Type", staff.Staff_Type);
                        command.Parameters.AddWithValue("@Staff_ZipCode", staff.Staff_ZipCode);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }

            
            catch (Exception ex)
            {
                return false;
            }

        }
        
    }

}
