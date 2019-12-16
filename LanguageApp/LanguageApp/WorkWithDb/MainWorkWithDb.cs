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
    public class MainWorkWithDb : DbCon
    {
        public MainWorkWithDb()
        {
            // Register the factory
            DbProviderFactories.RegisterFactory("System.Data.SQLite", SQLiteFactory.Instance);
            factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
        }
        SQLiteFactory factory = new SQLiteFactory();

        public bool LoginUser(string name, string password)
        {
            var t = IsUserAvailability(name, password);
            if (t)
                return UserLoginDTUpdate(name, password);
            return false;

        }

        public string getUserIdByName(string name, string password)
        {
            try
            {
                string t = "-1";                
                using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
                {
                    connection.ConnectionString = "Data Source = " + ConnectionString;
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"SELECT IdTable FROM Users where Users.UserName = '" + name+ "' and Users.Password = '"+password+"';";
                        command.CommandType = CommandType.Text;
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            t = reader["IdTable"].ToString();                            
                        }
                    }
                }
                return t;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return "-1";
            }
        }

        private bool UserLoginDTUpdate(string name, string password)
        {
            try
            {
                List<Users> LUsers_return = new List<Users>();                
                using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
                {
                    connection.ConnectionString = "Data Source = " + ConnectionString;
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {                        
                        command.CommandText = @"UPDATE Users SET DTLogin = datetime('now','localtime') WHERE UserName = '" + name + @"' AND Password = '" + password + @"'";
                        command.CommandType = CommandType.Text;
                        var reader = command.ExecuteNonQuery();
                        if (reader != 1)
                            throw new Exception("Error");                        
                    }
                    connection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }
        private bool IsUserAvailability(string name, string password)
        {
            try
            {
                List<Users> LUsers_return = new List<Users>();
                
                using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
                {
                    connection.ConnectionString = "Data Source = " + ConnectionString;
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"SELECT * FROM Users WHERE UserName = '" + name + "' AND Password = '" + password + "'";
                        command.CommandType = CommandType.Text;
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            LUsers_return.Add(new Users() { IdTable = (Int64)reader["IdTable"], Password = (string)reader["Password"], UserName = (string)reader["UserName"] });
                            break;
                        }
                        reader.Close();
                    }
                    connection.Close();
                }
                return LUsers_return.Count() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }
        public bool AddUser(string name, string password)
        {
            try
            {
                if (IsUserAvailability(name, password))
                    return false;
                
                using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
                {
                    connection.ConnectionString = "Data Source = " + ConnectionString;
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"INSERT INTO Users(UserName, Password, DTLogin) VALUES ('" + name + "','" + password + "',datetime('now','localtime'));";
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

        public List<Users> GetAllUsers()
        {
            try
            {
                List<Users> LUsers_return = new List<Users>();                
                using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
                {
                    connection.ConnectionString = "Data Source = " + ConnectionString;
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"SELECT * FROM Users";
                        command.CommandType = CommandType.Text;
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {                            
                            LUsers_return.Add(new Users() { IdTable = (Int64)reader["IdTable"], Password = (string)reader["Password"], UserName = (string)reader["UserName"], /*DateLogin = (DateTime)reader["DateLogin"]*/ });                            
                        }
                    }
                }
                return LUsers_return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return new List<Users>();
            }
        }
    }
}
