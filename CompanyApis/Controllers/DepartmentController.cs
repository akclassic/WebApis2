﻿using CompanyApis.DbOperations;
using CompanyApis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CompanyApis.Controllers
{
    public class DepartmentController : ApiController
    {
        CompanyDbOperations companyDbOperations;

        DepartmentController()
        {
            companyDbOperations = new CompanyDbOperations();
        }
        // GET: api/Department
        public async Task<List<DepartmentListModel>> Get()
        {
            return await companyDbOperations.GetDepartmentList();
        }

        // GET: api/Department/5
        public async Task<SingleDepartmentModel> Get(int id)
        {
            return await companyDbOperations.GetSingleDepartment(id);
        }

        // POST: api/Department
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Department/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Department/5
        public void Delete(int id)
        {
        }
    }
}
