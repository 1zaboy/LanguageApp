using Microsoft.ML.Data;

namespace AiLan.AiModel
{
    class wordPrediction
    {
        [ColumnName("label")]
        public string Label;
        //[ColumnName("PredictedLabel")]
        public float[] Score { get; set; }
    }
}
