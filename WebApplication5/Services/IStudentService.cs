using SchoolAdministration.DTO;

namespace SchoolAdministration.Services
{
    public interface IStudentService
    {
         Task<string> GetStudentDetails();
         void ConvertBase64ToFile(Payload payload);
         Task<List<Student>> ConvertFileToList();
         Task<bool> InsertExcelStudentData(List<Student> student);
    }
}
