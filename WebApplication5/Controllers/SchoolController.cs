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
        [Route("api/Student_Details")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Get_Student_Details()
        {
            try
            {
                string Student_Data = await _student.GetStudentDetails();
                return Ok(Student_Data);
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
                return Ok(Staff_Data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("api/Parse")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Insert_Excel_Data([FromQuery]Payload payload)
        {
            try
            {
                if(payload.Category=="Students")
                {
                    _student.ConvertBase64ToFile(payload);
                    var Student_List = await _student.ConvertFileToList();
                    
                  

                    return Ok(Student_List);
                }
                else
                {
                    return Ok(payload);
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



    }
}
