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
                        command.CommandText = @"SELECT Users.UserName as 'UserName', Count(*) as 'CountRequest', User.DateLogin as 'DateLogin', AVG(Requests.DTRequast) as 'avgDTRequast' FROM Requests INNER JOIN Users ON Requests.UserId = Users.IdTable";
                        command.CommandType = CommandType.Text;
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            tableModels.Add(new TableModel() { NameUser = (string)reader["UserName"], LastLogin = (DateTime)reader["DateLogin"], CountRequests = (int)reader["CountRequest"], AvgTimeRequests = (DateTime)reader["avgDTRequast"] });
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
