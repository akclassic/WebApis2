using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyApis.Models
{
    public class EmployeeDetailModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Nullable<int> DepartmentId { get; set; }

        public int ProjectCount { get; set; }
    }
}