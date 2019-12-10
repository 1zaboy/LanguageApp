using System;
using System.Collections.Generic;
using System.Text;

namespace ParserData.Db
{
    class DbCon
    {
        protected string ConnectionString { get; set; }

        public DbCon()
        {
            ConnectionString = "./DbForApp.db3";
        }
    }
}
