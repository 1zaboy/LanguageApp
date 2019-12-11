using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ML.Data;

namespace AiLan.AiModel
{
    class SqliteWordTableModel
    {
        [LoadColumn(0)]
        public string Word { get; set; }
        [LoadColumn(1)]
        public string Language { get; set; }
    }
}
