using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;
using System.Data.SQLite;

namespace ParserData.Db
{
    class WorkWithWord: DbCon
    {
        public WorkWithWord()
        {
            DbProviderFactories.RegisterFactory("System.Data.SQLite", SQLiteFactory.Instance);
        }



        public async Task<bool> addWord(string requests)
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
                        command.CommandText = requests;
                        command.CommandType = CommandType.Text;
                        var reader2 = await command.ExecuteNonQueryAsync();
                        Console.WriteLine(reader2);
                        if (reader2 < 1)
                            throw new Exception("Error");
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }
    }
}
