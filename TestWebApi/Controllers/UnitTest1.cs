using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SchoolAdministration.Controllers;
using SchoolAdministration.DTO;
using SchoolAdministration.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdministration.Controllers.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        private readonly IStudentService _student;
        private readonly IStaffService _staff;
        string path = "C:\\Users\\rohit\\source\\repos\\WebApplication5\\WebApplication5";
        /*[TestMethod()]
        public void Get_Staff_DetailsTest()
        {
            
        }*/

        [TestMethod()]
        public void Get_Student_DetailsTest()
        {

            SchoolController controller = new SchoolController(_student, _staff);
            var output =  controller.Get_Student_Details();
            Assert.AreEqual("",output.Result);

        }
    }
}