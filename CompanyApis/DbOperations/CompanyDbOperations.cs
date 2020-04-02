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
        public async Task<List<EmployeeListModel>> GetEmployeesList()
        {
            using (var context = new EntityFrameworkDbEntities())
            {
                var employees = await context.Employee
                                .Include(e => e.Department)
                                .Include(e => e.Laptop)
                                .Include(e => e.EmployeeProject)
                                .Select(e => new EmployeeListModel()
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

        public async Task<List<DepartmentListModel>> GetDepartmentList()
        {
            using (var context = new EntityFrameworkDbEntities())
            {
                var department = await context.Department
                                        .Include(d => d.Employee)
                                        .Select(d => new DepartmentListModel(){
                                            Id = d.Id,
                                            DepartmentName = d.DepartmentName
                                         })
                                        .ToListAsync();

                return department;
            }
        }

        public async Task<List<LaptopListModel>> GetLaptopList()
        {
            using(var context = new EntityFrameworkDbEntities())
            {
                var laptops = await context.Laptop
                                        .Include(l => l.Employee)
                                        .Select(d => new LaptopListModel()
                                        {
                                            Id = d.Id,
                                            BrandName = d.BrandName,
                                            EmployeeName = context.Employee.FirstOrDefault(e => e.Id == d.EmployeeId).FirstName + " " + context.Employee.FirstOrDefault(e => e.Id == d.EmployeeId).LastName
                                        })
                                        .ToListAsync();

                return laptops;
            }
        }

        public async Task<SingleEmployeeModel> GetSingleEmployee(int id)
        {
            //SELECT e.FirstName, e.LastName, e.Email, e.DepartmentId, d.DepartmentName, l.BrandName as LaptopBrand
            //FROM Employee e
            //JOIN Department d ON e.DepartmentId = d.Id
            //JOIN Laptop l ON l.EmployeeId = e.Id
            //WHERE e.Id = 1

            using (var context = new EntityFrameworkDbEntities())
            {
                var employee = await context.Employee
                                .Where(e => e.Id == id)
                                .Include(e => e.Department)
                                .Include(e => e.Laptop)
                                .Select(e => new SingleEmployeeModel()
                                {
                                    EmployeeId = e.Id,
                                    Name = e.FirstName + " " + e.LastName,
                                    Email = e.Email,
                                    DepartmentId = e.DepartmentId,
                                    DepartmentName = e.Department.DepartmentName,
                                    LaptopId = e.Laptop.FirstOrDefault(l => l.EmployeeId == e.Id).Id,
                                    LaptopBrand = e.Laptop.FirstOrDefault(l => l.EmployeeId == e.Id).BrandName
                                }).ToListAsync();

                return employee[0];
            }
        }

        public async Task<SingleDepartmentModel> GetSingleDepartment(int id)
        {
            //SELECT d.Id, d.DepartmentName,  (FirstName + ' ' + LastName) AS Employeename, l.Id AS LaptopId, BrandName
            //FROM Department d
            //JOIN Employee e ON e.DepartmentId = d.Id
            //JOIN Laptop l ON l.EmployeeId = e.Id
            //WHERE d.Id = 1

            using (var context = new EntityFrameworkDbEntities())
            {
                var department = await context.Department
                                 .Where(d => d.Id == id)
                                 .Include(d => d.Employee)
                                 .Select(d => new SingleDepartmentModel()
                                 {
                                     DepartmentId = d.Id,
                                     DepartmentName = d.DepartmentName,
                                     Employees = d.Employee.Where(e => e.DepartmentId == d.Id).Select(e => new EmployeeForDepartment() {
                                         Id = e.Id,
                                         FirstName = e.FirstName,
                                         LastName = e.LastName,
                                         LaptopForDepartment = e.Laptop.Where(l => l.EmployeeId == e.Id).Select(l => new LaptopForDepartment()
                                         {
                                             Id = l.Id,
                                             BrandName = l.BrandName
                                         }).FirstOrDefault()
                                     }).ToList()

                                 }).ToListAsync();

                return department[0];
            }
        }

        public async Task<SingleLaptopDetail> GetSingleLaptop(int id)
        {
            //SELECT l.Id, l.BrandName, e.Id AS EmployeeId, (FirstName + ' ' + LastName) AS Employeename, d.Id AS DepartmentId, d.DepartmentName
            //FROM Laptop l
            //JOIN Employee e ON e.Id = l.EmployeeId
            //JOIN Department d ON d.Id = e.Id
            //WHERE l.Id = 102

            using (var context = new EntityFrameworkDbEntities())
            {
                var laptop = context.Laptop
                                .Where(l => l.Id == id)
                                .Select(l => new SingleLaptopDetail()
                                {
                                    LaptopId = l.Id,
                                    BrandName = l.BrandName,
                                    EmployeeId = l.EmployeeId,
                                    EmployeeName = l.Employee.FirstName + " " + l.Employee.LastName,
                                    DepartmentId = l.Employee.DepartmentId,
                                    DepartmentName = l.Employee.Department.DepartmentName
                                }).FirstOrDefault();

                return laptop;
            }
        }
    }
}