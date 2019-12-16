using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using LanguageApp.WorkWithDb;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LanguageApp.Controllers
{
    [Route("api/[controller]")]
    public class DatatableController : Controller
    {
        DataForTable DFT = new DataForTable();
       

        [Route("GetJsonDataTopTan")]
        public string GetJsonDataTopTan()
        {
            var LData = DFT.getDataWithDb();            
            return JsonConvert.SerializeObject(LData);
        }
    }
}
