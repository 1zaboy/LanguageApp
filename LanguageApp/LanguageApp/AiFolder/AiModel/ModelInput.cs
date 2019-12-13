using Microsoft.ML.Data;

namespace LanguageApp.AiFolder.AiModel
{
    public class ModelInput
    {
        [ColumnName("Lan"), LoadColumn(0)]
        public float Lan { get; set; }


        [ColumnName("Word"), LoadColumn(1)]
        public string Word { get; set; }
    }
}
