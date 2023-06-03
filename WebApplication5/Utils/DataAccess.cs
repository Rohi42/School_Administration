
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

        public async Task<List<string>> ExecuteQueryWithParamsStudents(string Query, List<Student> Students)
        {
            List<string> duplicates = new List<string>();
            
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
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch(Exception ex)
                    {
                        duplicates.Add(student.Student_Id.ToString());
                        continue;
                    }
                    }
                }
                return duplicates;
            
            

        }

    public async Task<List<string>> ExecuteQueryWithParamsStaff(string Query, List<Staff> Staff)
    {
        List<string> duplicates = new List<string>();

        foreach (var staff in Staff)
        {
            using (var connection = new MySqlConnection(configuration))
            {
                var command = new MySqlCommand(Query, connection);
                command.Parameters.AddWithValue("@Staff_Id", staff.Staff_Id);
                command.Parameters.AddWithValue("@Staff_Name", staff.Staff_Name);
                command.Parameters.AddWithValue("@Staff_Address", staff.Staff_Address);
                command.Parameters.AddWithValue("@Staff_Type", staff.Staff_Type);
                command.Parameters.AddWithValue("@Staff_ZipCode", staff.Staff_ZipCode);
                connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    duplicates.Add(staff.Staff_Id.ToString());
                    continue;
                }
            }
        }
        return duplicates;




    
        }
        
    }

}
