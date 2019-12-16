using System;
using System.Collections.Generic;
using LanguageApp.WorkWithDb.DbCardClass;
using System.Data.Common;
using System.Data;
using System.Data.SQLite;


namespace LanguageApp.WorkWithDb
{
    public class ActionWithLan: DbCon
    {
        public ActionWithLan()
        {
            // Register the factory
            DbProviderFactories.RegisterFactory("System.Data.SQLite", SQLiteFactory.Instance);
        }

        public bool InsertLan(string NameLan)
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
                        command.CommandText = @"INSERT INTO Language(Namelanguage) VALUES ('" + NameLan + "');";
                        command.CommandType = CommandType.Text;
                        var reader = command.ExecuteNonQuery();
                        if (reader == 1)
                            return true;
                        else
                            throw new Exception("error");
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

        public bool UPDATELan(string NameLan, string NewNameLan)
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
                        command.CommandText = @"UPDATE Language SET Namelanguage = '" + NewNameLan + "' WHERE Language.Namelanguage = '" + NameLan + "'";
                        command.CommandType = CommandType.Text;//https://localhost:44327/api/LanSetting/UPDATELan?NameLan=English&NewNameLan=En
                        var reader = command.ExecuteNonQuery();
                        if (reader == 1)
                            return true;
                        else
                            throw new Exception("error");
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }


        public List<Language> GetLan()
        {
            List<Language> LLan = new List<Language>();
            try
            {
                SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
                {
                    connection.ConnectionString = "Data Source = " + ConnectionString;
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"select * from Language";
                        command.CommandType = CommandType.Text;
                        var reader = command.ExecuteReader();
                        while(reader.Read())
                        {
                            LLan.Add(new Language() { IdTable = (Int64)reader["IdTable"], NameLanguage = (string)reader["Namelanguage"] });
                        }
                        return LLan;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return new List<Language>();
            }
        }
    }
}
