using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageApp.WorkWithDb.DbCardClass
{
    public class Words
    {
        public int Table { get; set; }
        public string TextWord { get; set; }
        public int IdLanguage { get; set; }
        public int UserIdAddThisWord { get; set; }
    }
}
