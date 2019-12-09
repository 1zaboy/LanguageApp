using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LanguageApp.WorkWithDb;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LanguageApp.Controllers
{
    [Route("api/[controller]")]
    public class LanSettingController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        ActionWithLan AWL = new ActionWithLan();

        [Route("InsertLan")]
        public bool InsertLan(string textname)
        {
            return AWL.InsertLan(textname);
        }

        [Route("UpdateLan")]
        public bool UpdateLan(string textname, string newtextname)
        {
            return AWL.UPDATELan(textname, newtextname);
        }

        [Route("GetJsonLan")]
        public string GetJsonLan()
        {            
            return JsonConvert.SerializeObject(AWL.GetLan());
        }
    }
}
