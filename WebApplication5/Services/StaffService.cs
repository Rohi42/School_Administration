﻿using SchoolAdministration.Utils;
using SchoolAdministration.DTO;
using Aspose.Cells;
using LoadOptions = Aspose.Cells.LoadOptions;
using Newtonsoft.Json;
namespace SchoolAdministration.Services
{


    public class StaffService : IStaffService
    {
        private readonly DataAccess _Execute;

        public StaffService(DataAccess Execute)
        {
            _Execute = Execute;

        }

        string path = "C:\\Users\\rohit\\source\\repos\\WebApplication5\\WebApplication5";

        public async Task<string> GetStaffDetails()
        {

            string query = @"select * from School_Administration.staff";
            string tabledata = await _Execute.ExecuteQuery(query);
            return tabledata;
        }

        public void ConvertBase64ToFile(Payload payload)
        {
            string fileBase64 = "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";

            byte[] fileAsBytes = Convert.FromBase64String(fileBase64);

            //Convert byte[] into stream
            Stream stream = new MemoryStream(fileAsBytes, 0, fileAsBytes.Length);

            //Specify load options.
            LoadOptions loadOptions = new LoadOptions(LoadFormat.Xlsx);

            //Create workbook from stream
            Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook(stream, loadOptions);

            workbook.Save(path + "\\Staff_Details.xlsx");
        }

        public async Task<List<Staff>> ConvertFileToList()
        {

            var workbook = new Aspose.Cells.Workbook(path + "\\Staff_Details.xlsx");
            workbook.Worksheets.RemoveAt("Evaluation Warning");
            workbook.Worksheets.RemoveAt("Evaluation Warning (1)");
            workbook.Save(path + "\\OutputStaff.json");
            string text = File.ReadAllText(path + "\\OutputStaff.json");

            var Staff_List = JsonConvert.DeserializeObject<List<Staff>>(text);
            return Staff_List;

        }
        public async Task<List<string>> InsertExcelStaffData(List<Staff> staff)
        {
            staff = RemoveDuplicates(staff);
            string query = @"INSERT INTO School_Administration.staff (Staff_Id, Staff_Name, Staff_Type, Staff_Address, Staff_ZipCode)
            VALUES (@Staff_Id, @Staff_Name, @Staff_Type, @Staff_Address, @Staff_ZipCode);";
            List<string> Inserted = await _Execute.ExecuteQueryWithParamsStaff(query, staff);
            return (Inserted);
        }

        public List<Staff> RemoveDuplicates(List<Staff> staff)
        {
            return staff.DistinctBy(x => new { x.Staff_Id, x.Staff_Name }).ToList();
        }

        public async Task<string> GetStaffDataByID(int id)
        {
            string query = @"SELECT * FROM school_administration.staff where Staff_Id=45;";
            string tabledata = await _Execute.ExecuteQuery(query);
            return tabledata;
        }
    }
}