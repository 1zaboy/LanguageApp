using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ParserData.Db;
using System.Data.Common;
using System.Data;
using System.Data.SQLite;


namespace ParserData
{
    class ParseLan
    {

        public async void SetDbWords(string path, int idlan)
        {
            string buildstr = "";
            WorkWithWord Wword = new WorkWithWord();
            using (StreamReader SR = new StreamReader(path))
            {
                string ln = "";
                int cou = 0;
                buildstr += "BEGIN; ";
                while ((ln = SR.ReadLine()) != null)
                {
                    var first_word = ln.Split(" ");
                    if (first_word[0].Length >= 4)
                    {
                        buildstr += "INSERT INTO Words(TextWord, IdLanguage) VALUES('" + first_word[0] + "', '" + idlan + "');";
                        
                        //Console.WriteLine(cou);
                    }
                    if(cou % 1000 == 0 && cou > 0)
                    {
                        buildstr += "COMMIT; ";
                        var b = await Wword.addWord(buildstr);
                        buildstr = "BEGIN; ";                        
                    }
                    cou += 1;
                }
                
            }
        }
    }
}
