using Microsoft.ML.Data;

namespace AiLan.AiModel
{
    class wordInput
    {
        [ColumnName("Label"), LoadColumn(0)]
        public string Label { get; set; }
        [ColumnName("Message"), LoadColumn(1)]
        public string Message { get; set; }
    }
}
