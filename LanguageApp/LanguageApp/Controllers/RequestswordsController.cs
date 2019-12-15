using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LanguageApp.WorkWithDb;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LanguageApp.Controllers
{
    [Route("api/[controller]")]
    public class RequestswordsController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        
        public DataForTable dataForTable = new DataForTable();

        [Route("AddRequestsWord")]
        public bool AddRequestsWord(string userid, string wordid)
        {
            return dataForTable.addReqData(userid, wordid);
        }
    }
}
