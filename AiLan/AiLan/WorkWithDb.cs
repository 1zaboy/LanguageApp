using System.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using Microsoft.ML;
using Microsoft.ML.Data;
using System.Data.Common;
using AiLan.AiModel;

namespace AiLan
{
    class WorkWithDb
    {
        private string ConnectionString { get; set; }
        public WorkWithDb()
        {
            // Register the factory
            DbProviderFactories.RegisterFactory("System.Data.SQLite", SQLiteFactory.Instance);
            ConnectionString = "Data Source = " + GetAbsolutePath("DbForApp.db3");
        }

        public IDataView GetDataFromSQLite(MLContext mlContext)
        {
            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");            

            string sqlCommand = "SELECT Words.TextWord as 'Label', Language.Namelanguage as 'Message' from Words INNER JOIN Language ON Words.IdLanguage = Language.IdTable ORDER BY RANDOM() LIMIT 100000";

            DatabaseSource dbSource = new DatabaseSource(factory, ConnectionString, sqlCommand);

            DatabaseLoader loader = mlContext.Data.CreateDatabaseLoader<wordInput>();
            IDataView data = loader.Load(dbSource);
            return data;
        }
        public string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }
    }
}
