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
    public class WorkwordsController : Controller
    {


        List<string> LanChar = new List<string>();
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };

            LanChar.Add("abcdefghijklmnopqrstuvwxyz");
            LanChar.Add("абвгдеёжзийклмнопрстуфхцчшщъыьэюя");
            LanChar.Add("bkscltdmuenvfñwgoxhpyiqz");
            LanChar.Add("abcdefghijklmnopqrstuvwxyz");
            LanChar.Add("абвгдежзийклмнопрстуфхцчшщъьюя");
        }


        private ActionWithWords AWW = new ActionWithWords();
        [Route("GetLanguageWords")]
        public string GetLanguageWords(int userid, string str)
        {
            var LanItem = AWW.getLanguageWord(str);
            if (LanItem != "")
            {
                AWW.setSteteRequasts(userid, str);
                return LanItem;
            }

            var arrayWord = str.Split(' ');
            foreach(var item in arrayWord)
            {
                if(item.Length > 3)
                {
                    int[] count = new int[5];
                    foreach(var one_char in item)
                    {
                        for(int i = 0; i < 5; i++)
                        {
                            for (int ii = 0; ii < LanChar[i].Length; ii++)
                            {
                                if(one_char == LanChar[i][ii])
                                {
                                    count[i] += 1;
                                }
                            }
                        }
                    }
                }
            }
            return "";
        }
        
        
        [Route("InsertWordInDB")]
        public bool InsertWordInDB(string word, int LanId)
        {
            return AWW.addWord(word, LanId);
        }
    }
}
