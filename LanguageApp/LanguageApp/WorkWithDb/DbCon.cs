using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageApp.WorkWithDb
{
    public class DbCon
    {
        protected string ConnectionString { get; set; }

        public DbCon()
        {
            ConnectionString = "./AllDb/DbForApp.db3";
        }
    }
}
