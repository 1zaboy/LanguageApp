using Microsoft.ML.Data;

namespace AiLan
{
    class SpamPrediction
    {
        [ColumnName("RuLabel")]
        public float isRu { get; set; }

        [ColumnName("EnLabel")]
        public float isEn { get; set; }

        [ColumnName("EsLabel")]
        public float isEs { get; set; }

        [ColumnName("PtLabel")]
        public float isPt { get; set; }

        [ColumnName("BgLabel")]
        public float isBg { get; set; }
    }
}
