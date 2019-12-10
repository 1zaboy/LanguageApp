using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ML.Data;

namespace AiLan.AiModel
{
    class outputData
    {
        [ColumnName("pRu")]
        public float pRu { get; set; }
        [ColumnName("pEn")]
        public float pEn { get; set; }
        [ColumnName("pBg")]
        public float pBg { get; set; }
        [ColumnName("pEs")]
        public float pEs { get; set; }
        [ColumnName("pPt")]
        public float pPt { get; set; }
    }
}
