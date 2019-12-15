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
    public class LoginController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        MainWorkWithDb mainWorkWithDb = new MainWorkWithDb();

        [Route("IsLoginCompleted")]
        public string IsLoginCompleted(string name, string password)
        {            
            mainWorkWithDb.LoginUser(name, password);
            var r = mainWorkWithDb.getUserIdByName(name, password);            
            return r.ToString();
        }

        [Route("AddUser")]
        public string AddUser(string name, string password)
        {
            try
            {
                var j = mainWorkWithDb.AddUser(name, password);
                var r = mainWorkWithDb.getUserIdByName(name, password);                
                return r.ToString();
            }
            catch(Exception ex)
            {
                return false.ToString();
            }
        }

        [Route("GetUsers")]
        public string GetUsers()
        {
            return JsonConvert.SerializeObject(mainWorkWithDb.GetAllUsers());            
        }
    }
}
