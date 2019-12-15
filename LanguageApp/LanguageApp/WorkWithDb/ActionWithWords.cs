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
        
        public bool setSteteRequasts(int userid,string wordtext)
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
    }


}
