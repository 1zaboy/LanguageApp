﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LanguageApp.WorkWithDb;
using LanguageApp.AiFolder.AiModel;
using LanguageApp.AiFolder;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LanguageApp.Controllers
{
    [Route("api/[controller]")]
    public class WorkwordsController : Controller
    {
        string[] LanName = new string[5];
        string[] LanNameBD = new string[5];
        bool[] IsLanName = new bool[5] { false, false, false, false, false };

        public WorkwordsController()
        {
            LanName[0] = "Russian";
            LanName[3] = "English";
            LanName[1] = "Portuguese";
            LanName[2] = "Spanish";
            LanName[4] = "Bulgarian";

            LanNameBD[0] = "Russian";
            LanNameBD[1] = "English";
            LanNameBD[2] = "Portuguese";
            LanNameBD[3] = "Spanish";
            LanNameBD[4] = "Bulgarian";
        }
        

        private ActionWithWords AWW = new ActionWithWords();
        private AiControl AC = new AiControl();
        [Route("GetLanguageWords")]
        public string GetLanguageWords(int userid, string str)
        {
            str = str.ToLower();
            str = str.Trim();
            string fullStrAllLang = "";
            if (str.Length < 4)
                return "";

            AWW.AddRequeststWord(userid.ToString(), str);

            var itemsResult = AWW.GetSteteRequastsDB(str);
            if(itemsResult.Count == 5)
            {
                foreach(var item in itemsResult)
                {
                    fullStrAllLang += item.Lan + ": " + item.Proc + "%|";                    
                }
                return fullStrAllLang;
            }
            str = str.ToLower();
            var LLan = AWW.GetLanguageWord(str);
            Dictionary<int, float> keyValuePairs = new Dictionary<int, float>();

            if (LLan.Count != 0)
            {
                var proc = 100 / LLan.Count;
                for(int i = 0; i < LLan.Count; i++)
                {
                    var t = Array.IndexOf(LanNameBD, LLan[i]);
                    if (t != -1)
                    {
                        IsLanName[t] = true;
                    }
                }
                
                for (int i = 0; i < LanName.Length; i++)
                {
                    if(IsLanName[i])
                    {
                        fullStrAllLang += LanNameBD[i] + ": " + proc + "%|";
                        keyValuePairs.Add(AWW.getIdByNameLan(LanNameBD[i]), proc);
                    }
                    else
                    {
                        fullStrAllLang += LanNameBD[i] + ": 0%|";
                        keyValuePairs.Add(AWW.getIdByNameLan(LanNameBD[i]), 0);
                    }
                }
            }
            else
            {
                ModelInput MI = new ModelInput() { Word = str };
                AC.LoadModel();
                var modelOutput = AC.Predict(MI);
                
                for(int i = 0; i < modelOutput.Score.Length; i++)
                {
                    fullStrAllLang += LanName[i] + ": " + Math.Round(modelOutput.Score[i] * 100, 1) + "%|";
                    keyValuePairs.Add(AWW.getIdByNameLan(LanName[i]), modelOutput.Score[i] * 100);
                }
            }
            AWW.AddResultWord(userid.ToString(), str, keyValuePairs);
            return fullStrAllLang;
        }
        
        
        [Route("InsertWordInDB")]
        public bool InsertWordInDB(string word, int LanId)
        {
            return AWW.addWord(word, LanId);
        }
    }
}
