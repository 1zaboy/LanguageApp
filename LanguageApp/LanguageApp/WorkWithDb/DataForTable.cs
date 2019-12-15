using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageApp.WorkWithDb.DbCardClass;
using System.Data.Common;
using System.Data;
using System.Data.SQLite;
using LanguageApp.WorkWithDb.Models;

namespace LanguageApp.WorkWithDb
{
    public class DataForTable: DbCon
    {
        public DataForTable()
        {
            DbProviderFactories.RegisterFactory("System.Data.SQLite", SQLiteFactory.Instance);
        }
        public List<TableModel> getDataWithDb()
        {
            List<TableModel> tableModels = new List<TableModel>();
            try
            {
                SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
                {
                    connection.ConnectionString = "Data Source = " + ConnectionString;
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"SELECT 
                            Users.UserName, 
                            count(*) as 'countReq',
                            max(Users.DTLogin) as 'LastLog',
                            (julianday( max(DTRequest))- julianday(min(DTRequest)))/count(*) as 'dtime'
                            FROM Request
                            INNER JOIN Users ON Request.IdUser = Users.IdTable
                            GROUP by IdUser
                            order by count(*) DESC LIMIT 10";
                        command.CommandType = CommandType.Text;
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            var avgReq = new TimeSpan((long)((double)reader["dtime"] / new TimeSpan(1).TotalDays));
                            tableModels.Add(new TableModel() { NameUser = reader["UserName"].ToString(), CountRequests = (int)(long)reader["countReq"], LastLogin = DateTime.Parse(reader["LastLog"].ToString()), AvgTimeRequests = avgReq });
                        }

                    }
                }
                return tableModels;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }

        public bool addReqData(string userid, string wordid)
        {
            try
            {
                SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
                {
                    connection.ConnectionString = "Data Source = " + ConnectionString;
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"INSERT INTO Request (                        
                        IdUser,
                        IdWord,
                        DTRequest
                    )
                    VALUES (                        
                        '"+userid+@"',
                        '" + wordid + @"',
                        datetime('now')
                    );";
                        command.CommandType = CommandType.Text;
                        var reader = command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }
    }
}
