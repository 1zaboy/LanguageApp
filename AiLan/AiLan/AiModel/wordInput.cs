using Microsoft.ML.Data;

namespace AiLan.AiModel
{
    class wordInput
    {
        [LoadColumn(0)]
        public string Label { get; set; }
        [LoadColumn(1)]
        public string Message { get; set; }
    }
}
