using CompanyApis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;

namespace CompanyApis.DbOperations
{
    public class CompanyDbOperations
    {
        public async Task<List<EmployeeDetailModel>> GetEmployeesList()
        {
            using (var context = new EntityFrameworkDbEntities())
            {
                var employees = await context.Employee
                                .Include(e => e.Department)
                                .Include(e => e.Laptop)
                                .Include(e => e.EmployeeProject)
                                .Select(e => new EmployeeDetailModel()
                                {
                                    Id = e.Id,
                                    FirstName = e.FirstName,
                                    LastName = e.LastName,
                                    Email = e.LastName,
                                    DepartmentId = e.DepartmentId,
                                    ProjectCount = e.EmployeeProject.Count
                                })
                                .ToListAsync();
                return employees;
            }
        }

        public async Task<List<DepartmentModel>> GetDepartmentList()
        {
            using (var context = new EntityFrameworkDbEntities())
            {
                var department = await context.Department
                                        .Include(d => d.Employee)
                                        .Select(d => new DepartmentModel(){
                                            Id = d.Id,
                                            DepartmentName = d.DepartmentName
                                         })
                                        .ToListAsync();

                return department;
            }
        }
    }
}