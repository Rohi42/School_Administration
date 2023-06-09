using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolAdministration.DTO;
using SchoolAdministration.Services;


namespace SchoolAdministration.Controllers
{
    [ApiController]
    
    public class SchoolController : Controller
    {
        
        private readonly IStudentService _student;
        private readonly IStaffService _staff;

        public SchoolController(IStudentService student, IStaffService staff)
        {
            _student = student;
            _staff = staff;
            
        }
        
        [HttpGet]
        [AllowAnonymous]
        [Route("api/Student_Details")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Get_Student_Details()
        {
            try
            {
                string Student_Data = await _student.GetStudentDetails();
                return Ok((Student_Data.Length)==0 ? "No Student Data Found!!!!" : Student_Data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/Staff_Details")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get_Staff_Details()
        {
            try
            {
                string Staff_Data = await _staff.GetStaffDetails();
                return Ok(Staff_Data.Length!=0?Staff_Data:"No Staff Data Found!!!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("api/ParseBase64")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Insert_Excel_Data([FromBody]Payload payload)
        {
            try
            {
                if(payload.Category=="Students")
                {
                    _student.ConvertBase64ToFile(payload);
                    var Student_List = await _student.ConvertFileToList();
                    bool invalid_template = Student_List.All(x => x.Student_Id == 0);
                    List<string> duplicates = await _student.InsertExcelStudentData(Student_List);
                    if (invalid_template)
                    {
                        return BadRequest("Error!!! Please upload a valid Student template.");
                    }
                    if (duplicates.Count > 0)
                    {
                        return BadRequest("Error!!! Duplicate entries for records with existing Id's " + string.Join(", ", duplicates));
                    }
                    return Ok(Student_List);
                }
                else if(payload.Category=="Staff")
                {
                    _staff.ConvertBase64ToFile(payload);
                    var Staff_List = await _staff.ConvertFileToList();
                    bool invalid_template = Staff_List.All(x => x.Staff_Id == 0);
                    if(invalid_template)
                    {
                        return BadRequest("Error!!! Please upload a valid Staff template.");
                    }
                    List<string> duplicates = await _staff.InsertExcelStaffData(Staff_List);
                    if (duplicates.Count>0)
                    {
                        return BadRequest("Error!!! Duplicate entries for records with existing Id's "+ string.Join(", ", duplicates) +"and rest were inserted" );
                  }
                    return Ok(Staff_List);
                }
                else return BadRequest("Error something went wrong!!!!");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



    }
}
