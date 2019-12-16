using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using LanguageApp.AiFolder.AiModel;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace LanguageApp.AiFolder
{
    public class AiControl
    {
        private ITransformer mlModel;
        private MLContext mlContext = new MLContext();
        public void LoadModel()
        {            
            //string modelPath = AppDomain.CurrentDomain.BaseDirectory + "MLModel.zip";
            string modelPath = "./AllDb/MLModel.zip";         
            mlModel = mlContext.Model.Load(modelPath, out var modelInputSchema);
        }

        public ModelOutput Predict(ModelInput input)
        {                      
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            // Use model to make prediction on input data
            ModelOutput result = predEngine.Predict(input);
            return result;
        }
    }
}
