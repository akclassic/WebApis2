using CompanyApis.DbOperations;
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
    public class LaptopController : ApiController
    {
        CompanyDbOperations companyDbOperations;

        LaptopController()
        {
            companyDbOperations = new CompanyDbOperations();
        }

        // GET: api/Laptop
        public async Task<IEnumerable<LaptopListModel>> Get()
        {
            return await companyDbOperations.GetLaptopList();
        }

        // GET: api/Laptop/5
        public async Task<SingleLaptopDetail> Get(int id)
        {
            return await companyDbOperations.GetSingleLaptop(id);
        }

        // POST: api/Laptop
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Laptop/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Laptop/5
        public void Delete(int id)
        {
        }
    }
}
