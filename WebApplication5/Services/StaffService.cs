using SchoolAdministration.Utils;

namespace SchoolAdministration.Services
{


    public class StaffService : IStaffService
    {
        private readonly DataAccess _Execute;

        public StaffService(DataAccess Execute)
        {
            _Execute = Execute;

        }

        public async Task<string> GetStaffDetails()
        {

            string query = @"select * from sql9619976.Staff";
            string tabledata = await _Execute.ExecuteQuery(query);
            return tabledata;
        }

    }
}