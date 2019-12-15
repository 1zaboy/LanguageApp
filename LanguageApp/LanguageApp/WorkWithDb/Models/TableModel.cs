using System;

namespace LanguageApp.WorkWithDb.Models
{
    public class TableModel
    {
        public string NameUser { get; set; }
        public int CountRequests { get; set; }
        public DateTime LastLogin { get; set; } 
        public TimeSpan AvgTimeRequests { get; set; }
    }
}
