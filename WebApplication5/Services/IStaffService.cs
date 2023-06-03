﻿using SchoolAdministration.DTO;
namespace SchoolAdministration.Services
{
    public interface IStaffService
    {
        Task<string> GetStaffDetails();
        void ConvertBase64ToFile(Payload payload);
        Task<List<Staff>> ConvertFileToList();
        Task<List<string>> InsertExcelStaffData(List<Staff> staff);
    }
}
