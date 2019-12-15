using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageApp.WorkWithDb.DbCardClass;
using System.Data.Common;
using System.Data;
using System.Data.SQLite;

namespace LanguageApp.WorkWithDb
{
    public class ActionWithWords : DbCon
    {
        public ActionWithWords()
        {
            DbProviderFactories.RegisterFactory("System.Data.SQLite", SQLiteFactory.Instance);
        }
        public List<string> GetLanguageWord(string textword)
        {
            return _getLanWord(textword);
        }
        private bool IsWordInDb(string textword)
        {
            try
            {
                List<Users> LUsers_return = new List<Users>();
                SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
                {
                    connection.ConnectionString = "Data Source = " + ConnectionString;
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"select count(*) as 'count' from Words WHERE TextWord = '" + textword + "'";
                        command.CommandType = CommandType.Text;
                        var reader = command.ExecuteReader();
                        int countItem = 0;
                        while (reader.Read())
                        {
                            countItem = (int)reader["count"];
                        }
                        if (countItem == 0)
                            throw new Exception("Error");

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

        private List<string> _getLanWord(string textword)
        {
            try
            {
                List<string> strItem = new List<string>();
                SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
                {
                    connection.ConnectionString = "Data Source = " + ConnectionString;
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"select Language.Namelanguage as 'Namelanguage' from Words INNER JOIN Language ON Words.IdLanguage = Language.IdTable WHERE TextWord = '" + textword + "'";
                        command.CommandType = CommandType.Text;
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            strItem.Add((string)reader["Namelanguage"]);
                        }
                        return strItem;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return new List<string>();
            }
        }

        public bool setSteteRequasts(int userid, string wordtext)
        {
            try
            {
                List<Users> LUsers_return = new List<Users>();
                SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
                {
                    connection.ConnectionString = "Data Source = " + ConnectionString;
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = "select Words.IdTable as 'IdTable' from Words where Words.TextWord = " + wordtext;
                        command.CommandType = CommandType.Text;
                        var reader1 = command.ExecuteReader();
                        int wordid = -1;
                        while (reader1.Read())
                        {
                            wordid = (int)reader1["IdTable"];
                        }

                        command.CommandText = "INSERT INTO Requasts(UserId, DTRequast, WordId) VALUES('" + userid + "', '" + DateTime.Now.ToString() + "', '" + wordid + "'); ";
                        command.CommandType = CommandType.Text;
                        var reader2 = command.ExecuteNonQuery();
                        if (reader2 != 1)
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

        public bool AddResultWord(string userid, string textword, Dictionary<int, float> keyValueRes)
        {
            try
            {
                string strRes = "";

                foreach (var item in keyValueRes)
                {
                    strRes = @"INSERT INTO Result (                       
                       Text,
                       Proc,
                       IdLan,
                       DTAdd
                   )
                   VALUES (                       
                       '" + textword + @"',
                       '" + (Math.Round(item.Value, 1) * 10).ToString() + @"',
                       '" + item.Key + @"',
                       DateTime('now')
                   );";
                    ExeReq(strRes);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AddRequeststWord(string userid, string textword)
        {
            try
            {
                string strRes = "";

                strRes = @"INSERT INTO Request (                        
                        IdUser,
                        Text,
                        DTRequest
                    )
                    VALUES (                        
                        '" + userid + @"',
                        '" + textword + @"',
                        DateTime('now')
                    );";
                ExeReq(strRes);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Result> GetSteteRequastsDB(string wordtext)
        {
            try
            {
                List<Result> LUsers_return = new List<Result>();
                SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
                {
                    connection.ConnectionString = "Data Source = " + ConnectionString;
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"SELECT
                            Text,
                            Proc,
                            language.Namelanguage       
                            FROM Result
                            INNER JOIN Language ON Result.IdLan = Language.IdTable
                            where Result.Text = '" + wordtext+"';";
                        command.CommandType = CommandType.Text;
                        var reader1 = command.ExecuteReader();                        
                        while (reader1.Read())
                        {
                            var s = reader1.GetDouble("Proc").ToString();
                            LUsers_return.Add(new Result() { Lan = (string)reader1["Namelanguage"], Proc = reader1.GetInt32("Proc") / 10.0, Text = (string)reader1["Text"] });                            
                        }   
                    }
                }
                return LUsers_return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return new List<Result>();
            }
        }
        public bool addWord(string textWord, int LanId)
        {
            try
            {
                List<Users> LUsers_return = new List<Users>();
                SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
                {
                    connection.ConnectionString = "Data Source = " + ConnectionString;
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = "INSERT INTO Words(TextWord, IdLanguage) VALUES('" + textWord + "', '" + LanId + "'); ";
                        command.CommandType = CommandType.Text;
                        var reader2 = command.ExecuteNonQuery();
                        if (reader2 != 1)
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

        public int getIdByNameLan(string name)
        {
            try
            {
                int y = 0;
                SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
                {
                    connection.ConnectionString = "Data Source = " + ConnectionString;
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"SELECT IdTable
                            FROM language
                            where Namelanguage = '" + name + "';";
                        command.CommandType = CommandType.Text;
                        var reader1 = command.ExecuteReader();
                        while (reader1.Read())
                        {
                            y = Convert.ToInt32(reader1["IdTable"].ToString());
                        }
                    }
                }
                return y;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return -1;
            }
        }

        public bool ExeReq(string reqStr)
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
                        command.CommandText = reqStr;
                        command.CommandType = CommandType.Text;
                        var reader = command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }


}
