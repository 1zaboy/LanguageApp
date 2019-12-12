using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.IO.Compression;
using System.Net;
using AiLan.AiModel;

namespace AiLan
{
    //class Program
    //{
    //    public static void Main()
    //    {            
    //        DbProviderFactories.RegisterFactory("System.Data.SQLite", SQLiteFactory.Instance);

    //        var mlContext = new MLContext();

    //        // localdb SQL database connection string using a filepath to attach the database file into localdb
    //        string dbFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SqlLocalDb", "DbForApp.db3");
    //        string connectionString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={dbFilePath};Database=Criteo-100k-rows;Integrated Security = True";

    //        // ConnString Example: localdb SQL database connection string for 'localdb default location' (usually files located at /Users/YourUser/)
    //        //string connectionString = @"Data Source=(localdb)\MSSQLLocalDb;Initial Catalog=YOUR_DATABASE;Integrated Security=True;Pooling=False";
    //        //
    //        // ConnString Example: on-premises SQL Server Database (Integrated security)
    //        //string connectionString = @"Data Source=YOUR_SERVER;Initial Catalog=YOUR_DATABASE;Integrated Security=True;Pooling=False";
    //        //
    //        // ConnString Example:  Azure SQL Database connection string
    //        //string connectionString = @"Server=tcp:yourserver.database.windows.net,1433; Initial Catalog = YOUR_DATABASE; Persist Security Info = False; User ID = YOUR_USER; Password = YOUR_PASSWORD; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 60; ConnectRetryCount = 5; ConnectRetryInterval = 10;";

    //        string commandText = "SELECT Words.TextWord, Language.Namelanguage from Words INNER JOIN Language ON Words.IdLanguage = Language.IdTable";

    //        DatabaseLoader loader = mlContext.Data.CreateDatabaseLoader<Word>();

    //        DatabaseSource dbSource = new DatabaseSource(SQLiteFactory.Instance,
    //                                                     connectionString,
    //                                                     commandText);

    //        IDataView dataView = loader.Load(dbSource);

    //        var trainTestData = mlContext.Data.TrainTestSplit(dataView);

    //        //do the transformation in IDataView
    //        //Transform categorical features into binary
    //        var CatogoriesTranformer = mlContext.Transforms.Conversion.ConvertType(nameof(Word.IdTable), outputKind: Microsoft.ML.Data.DataKind.Boolean);

    //        var featuresTransformer = CatogoriesTranformer.Append(
    //            mlContext.Transforms.Text.FeaturizeText(outputColumnName: "TextWordFeaturized", inputColumnName: nameof(Word.TextWord)))
    //            .Append(mlContext.Transforms.Text.FeaturizeText(outputColumnName: "NamelanguageFeaturized", inputColumnName: nameof(Word.Namelanguage)));


    //        var finalTransformerPipeLine = featuresTransformer.Append(mlContext.Transforms.Concatenate("Features",
    //                        "TextWordFeaturized", "NamelanguageFeaturized"));

    //        // Apply the ML algorithm
    //        var trainingPipeLine = finalTransformerPipeLine.Append(mlContext.BinaryClassification.Trainers.FieldAwareFactorizationMachine(labelColumnName: "IdTable", featureColumnName: "Features"));

    //        Console.WriteLine("Training the ML model while streaming data from a SQL database...");
    //        Stopwatch watch = new Stopwatch();
    //        watch.Start();

    //        var model = trainingPipeLine.Fit(trainTestData.TrainSet);

    //        watch.Stop();
    //        Console.WriteLine("Elapsed time for training the model = {0} seconds", watch.ElapsedMilliseconds / 1000);

    //        Console.WriteLine("Evaluating the model...");
    //        Stopwatch watch2 = new Stopwatch();
    //        watch2.Start();

    //        var predictions = model.Transform(trainTestData.TestSet);
    //        // Now that we have the test predictions, calculate the metrics of those predictions and output the results.
    //        var metrics = mlContext.BinaryClassification.Evaluate(predictions);

    //        watch2.Stop();
    //        Console.WriteLine("Elapsed time for evaluating the model = {0} seconds", watch2.ElapsedMilliseconds / 1000);            
    //        // 
    //        Console.WriteLine("Trying a single prediction:");

    //        var predictionEngine = mlContext.Model.CreatePredictionEngine<Word, ClickPrediction>(model);

    //        Word sampleData = new Word()
    //        {
    //            IdTable = 0,
    //            TextWord = "Привэт",
    //            Namelanguage = "Russian"

    //        };

    //        var clickPrediction = predictionEngine.Predict(sampleData);

    //        Console.WriteLine($"Predicted Label: {clickPrediction.PredictedLabel} - Score:{Sigmoid(clickPrediction.Score)}", Color.YellowGreen);
    //        Console.WriteLine();

    //        //*** Detach database from localdb only if you used a conn-string with a filepath to attach the database file into localdb ***
    //        Console.WriteLine("... Detaching database from SQL localdb ...");
    //        //DetachDatabase(connectionString);

    //        Console.WriteLine("=============== Press any key ===============");
    //        Console.ReadKey();
    //    }

    //    public static float Sigmoid(float x)
    //    {
    //        return (float)(100 / (1 + Math.Exp(-x)));
    //    }

    //    //public static void DetachDatabase(string userConnectionString) //DELETE PARAM *************
    //    //{
    //    //    string dbName = string.Empty;
    //    //    using (SqlConnection userSqlDatabaseConnection = new SqlConnection(userConnectionString))
    //    //    {
    //    //        userSqlDatabaseConnection.Open();
    //    //        dbName = userSqlDatabaseConnection.Database;
    //    //    }

    //    //    string masterConnString = $"Data Source = (LocalDB)\\MSSQLLocalDB;Integrated Security = True";
    //    //    using (SqlConnection sqlDatabaseConnection = new SqlConnection(masterConnString))
    //    //    {
    //    //        sqlDatabaseConnection.Open();

    //    //        string prepareDbcommandString = $"ALTER DATABASE [{dbName}] SET OFFLINE WITH ROLLBACK IMMEDIATE ALTER DATABASE [{dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
    //    //        //(ALTERNATIVE) string prepareDbcommandString = $"ALTER DATABASE [{dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
    //    //        SqlCommand sqlPrepareCommand = new SqlCommand(prepareDbcommandString, sqlDatabaseConnection);
    //    //        sqlPrepareCommand.ExecuteNonQuery();

    //    //        string detachCommandString = "sp_detach_db";
    //    //        SqlCommand sqlDetachCommand = new SqlCommand(detachCommandString, sqlDatabaseConnection);
    //    //        sqlDetachCommand.CommandType = CommandType.StoredProcedure;
    //    //        sqlDetachCommand.Parameters.AddWithValue("@dbname", dbName);
    //    //        sqlDetachCommand.ExecuteNonQuery();
    //    //    }
    //    //}
    //}





    // class Program
    //{
    //    private static string AppPath => Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
    //    private static string DataDirectoryPath => Path.Combine(AppPath, "..", "..", "..", "Data", "spamfolder");
    //    private static string TrainDataPath => Path.Combine(AppPath, "..", "..", "..", "Data", "spamfolder", "SMSSpamCollection");

    //    static void Main(string[] args)
    //    {
    //        // Download the dataset if it doesn't exist.
    //        if (!File.Exists(TrainDataPath))
    //        {
    //            using (var client = new WebClient())
    //            {
    //                //The code below will download a dataset from a third-party, UCI (link), and may be governed by separate third-party terms. 
    //                //By proceeding, you agree to those separate terms.
    //                client.DownloadFile("https://archive.ics.uci.edu/ml/machine-learning-databases/00228/smsspamcollection.zip", "spam.zip");
    //            }

    //            ZipFile.ExtractToDirectory("spam.zip", DataDirectoryPath);
    //        }



    //        // Set the training algorithm 
    //        InputOutputColumnPair[] IOColumnPair = new InputOutputColumnPair[5];
    //        IOColumnPair[0] = new InputOutputColumnPair("RuLabel", "RuLabel");
    //        IOColumnPair[1] = new InputOutputColumnPair("EnLabel", "EnLabel");
    //        IOColumnPair[2] = new InputOutputColumnPair("EsLabel", "EsLabel");
    //        IOColumnPair[3] = new InputOutputColumnPair("PtLabel", "PtLabel");
    //        IOColumnPair[4] = new InputOutputColumnPair("BgLabel", "BgLabel");


    //        // Set up the MLContext, which is a catalog of components in ML.NET.
    //        MLContext mlContext = new MLContext();

    //        // Specify the schema for spam data and read it into DataView.
    //        var data = mlContext.Data.LoadFromTextFile<SpamInput>(path: TrainDataPath, hasHeader: true, separatorChar: '\t');

    //        // Create the estimator which converts the text label to boolean, featurizes the text, and adds a linear trainer.
    //        // Data process configuration with pipeline data transformations 
    //        //var dataProcessPipeline = mlContext.Transforms.Conversion.MapValueToKey(IOColumnPair)
    //        //                          .Append(mlContext.Transforms.Text.FeaturizeText("FeaturesText", new Microsoft.ML.Transforms.Text.TextFeaturizingEstimator.Options
    //        //                          {
    //        //                              WordFeatureExtractor = new Microsoft.ML.Transforms.Text.WordBagEstimator.Options { NgramLength = 2, UseAllLengths = true },
    //        //                              CharFeatureExtractor = new Microsoft.ML.Transforms.Text.WordBagEstimator.Options { NgramLength = 3, UseAllLengths = false },
    //        //                          }, "Message"))
    //        //                          .Append(mlContext.Transforms.CopyColumns("Features", "FeaturesText"))
    //        //                          .Append(mlContext.Transforms.NormalizeLpNorm("Features", "Features"))
    //        //                          .AppendCacheCheckpoint(mlContext);
    //        var dataProcessPipeline = mlContext.Transforms.Concatenate("Features", new[] { "Size" })



    //        var trainer = mlContext.MulticlassClassification.Trainers.OneVersusAll(mlContext.BinaryClassification.Trainers.AveragedPerceptron(labelColumnName: "Label", numberOfIterations: 10, featureColumnName: "Features"), labelColumnName: "Label")            
    //        .Append(mlContext.Transforms.Conversion.MapKeyToValue(IOColumnPair));
    //        var trainingPipeLine = dataProcessPipeline.Append(trainer);

    //        // Evaluate the model using cross-validation.
    //        // Cross-validation splits our dataset into 'folds', trains a model on some folds and 
    //        // evaluates it on the remaining fold. We are using 5 folds so we get back 5 sets of scores.
    //        // Let's compute the average AUC, which should be between 0.5 and 1 (higher is better).
    //        Console.WriteLine("=============== Cross-validating to get model's accuracy metrics ===============");
    //        var crossValidationResults = mlContext.MulticlassClassification.CrossValidate(data: data, estimator: trainingPipeLine, numberOfFolds: 5);
    //        ConsoleHelper.PrintMulticlassClassificationFoldsAverageMetrics(trainer.ToString(), crossValidationResults);

    //        // Now let's train a model on the full dataset to help us get better results
    //        var model = trainingPipeLine.Fit(data);

    //        //Create a PredictionFunction from our model 
    //        var predictor = mlContext.Model.CreatePredictionEngine<SpamInput, SpamPrediction>(model);






    //        Console.WriteLine("=============== Predictions for below data===============");
    //        // Test a few examples
    //        ClassifyMessage(predictor, "PRIVTE! Your 2004 Acount Statemnt for 07742676969 shows 786 unredeemed Bonus Points. To claim call 08719180248 Identifier Code: 45239 Expires");
    //        ClassifyMessage(predictor, "free medicine winner! congratulations");
    //        ClassifyMessage(predictor, "Yes we should meet over the weekend!");
    //        ClassifyMessage(predictor, "you win pills and free entry vouchers");
    //        ClassifyMessage(predictor, "аводпыдлоар");

    //        Console.WriteLine("=============== End of process, hit any key to finish =============== ");
    //        Console.ReadLine();
    //    }

    //    public static void ClassifyMessage(PredictionEngine<SpamInput, SpamPrediction> predictor, string message)
    //    {
    //        var input = new SpamInput { Message = message };
    //        var prediction = predictor.Predict(input);

    //        Console.WriteLine("The message '{0}' is {1}", input.Message, /*prediction.isSpam == "spam" ? "spam" : */"not spam");
    //    } 
    class Program
    {

        private static string AppPath => Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);

        private static string BaseDatasetsRelativePath = @"../../../../Data";
        private static string TrainDataRelativePath = $"{BaseDatasetsRelativePath}/iris-train.txt";
        private static string TestDataRelativePath = $"{BaseDatasetsRelativePath}/iris-test.txt";

        private static string TrainDataPath = GetAbsolutePath(TrainDataRelativePath);
        private static string TestDataPath = GetAbsolutePath(TestDataRelativePath);

        private static string BaseModelsRelativePath = @"MLModels";
        private static string ModelRelativePath = $"{BaseModelsRelativePath}\\WordClassificationModel.zip";

        private static string ModelPath = GetAbsolutePath(ModelRelativePath);
        private static void Main(string[] args)
        {
            // Create MLContext to be shared across the model creation workflow objects 
            // Set a random seed for repeatable/deterministic results across multiple trainings.
            var mlContext = new MLContext(seed: 0);
            var strpath = ModelPath;
            //1.
            BuildTrainEvaluateAndSaveModel(mlContext);

            //2.
            TestSomePredictions(mlContext);

            Console.WriteLine("=============== End of process, hit any key to finish ===============");
            Console.ReadKey();
        }

        private static void BuildTrainEvaluateAndSaveModel(MLContext mlContext)
        {
            // STEP 1: Common data loading configuration
            WorkWithDb workWithDb = new WorkWithDb();
            var trainingDataView = workWithDb.GetDataFromSQLite(mlContext);//mlContext.Data.LoadFromTextFile<wordInput>(TrainDataPath, hasHeader: true);
            //var trainingDataView = mlContext.Data.LoadFromTextFile<wordInput>("E:/GitHub/LanguageApp/AiLan/AiLan/SMSSpamCollection.tsv", hasHeader: true, separatorChar: '\t');            
            //var testDataView = mlContext.Data.LoadFromTextFile<wordInput>(TestDataPath, hasHeader: true);

            var trainTestData = mlContext.Data.TrainTestSplit(trainingDataView);



            var CatogoriesTranformer = mlContext.Transforms.Conversion.ConvertType(nameof(wordInput.Label), outputKind: Microsoft.ML.Data.DataKind.Double);




            // STEP 2: Common data process configuration with pipeline data transformations
            var dataProcessPipeline = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "KeyColumn", inputColumnName: "Label")
                
                // Required 
                //Create a TextFeaturizingEstimator, which transforms a text column 
                //into a featurized vector of Single that represents normalized 
                //counts of n-grams and char-grams.
                .Append(mlContext.Transforms.Text.FeaturizeText("FeaturesText", new Microsoft.ML.Transforms.Text.TextFeaturizingEstimator.Options
                {
                    WordFeatureExtractor = new Microsoft.ML.Transforms.Text.WordBagEstimator.Options { NgramLength = 2, UseAllLengths = true },
                    CharFeatureExtractor = new Microsoft.ML.Transforms.Text.WordBagEstimator.Options { NgramLength = 3, UseAllLengths = false },
                }, "Message"))               
                //copy column
                //Create a ColumnCopyingEstimator, which copies the data from the 
                //column specified in inputColumnName to a new column: outputColumnName.
                .Append(mlContext.Transforms.CopyColumns(outputColumnName: "Features", inputColumnName: "FeaturesText"))

                .Append(mlContext.Transforms.NormalizeLpNorm("Features", "Features"))
                //.Append(mlContext.Transforms.Conversion.MapKeyToValue("label", "PredictedLabel"))
                //.Append(mlContext.Transforms.Concatenate("Features", nameof(wordInput.Message)))
                .AppendCacheCheckpoint(mlContext);
                
                //.AppendCacheCheckpoint(mlContext));

            // Use in-memory cache for small/medium datasets to lower training time. 
            // Do NOT use it (remove .AppendCacheCheckpoint()) when handling very large datasets. 

            // STEP 3: Set the training algorithm, then append the trainer to the pipeline  
            var trainer = mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy(labelColumnName: "KeyColumn", featureColumnName: "Features")
            .Append(mlContext.Transforms.Conversion.MapKeyToValue(outputColumnName: nameof(wordInput.Label), inputColumnName: "KeyColumn"));

            var trainingPipeline = dataProcessPipeline.Append(trainer);

            // STEP 4: Train the model fitting to the DataSet
            Console.WriteLine("=============== Training the model ===============");
            ITransformer trainedModel = trainingPipeline.Fit(trainingDataView);

            // STEP 5: Evaluate the model and show accuracy stats
            //Console.WriteLine("===== Evaluating Model's accuracy with Test data =====");
            //var predictions = trainedModel.Transform(testDataView);
            //var metrics = mlContext.MulticlassClassification.Evaluate(predictions, "Label", "Score");            

            // STEP 6: Save/persist the trained model to a .ZIP file
            mlContext.Model.Save(trainedModel, trainingDataView.Schema, ModelPath);
            Console.WriteLine("The model is saved to {0}", ModelPath);
        }



        private static void TestSomePredictions(MLContext mlContext)
        {
            //Test Classification Predictions with some hard-coded samples 
            ITransformer trainedModel = mlContext.Model.Load(ModelPath, out var modelInputSchema);

            // Create prediction engine related to the loaded trained model
            var predEngine = mlContext.Model.CreatePredictionEngine<wordInput, wordPrediction>(trainedModel);

            // During prediction we will get Score column with 3 float values.
            // We need to find way to map each score to original label.
            // In order to do that we need to get TrainingLabelValues from Score column.
            // TrainingLabelValues on top of Score column represent original labels for i-th value in Score array.
            // Let's look how we can convert key value for PredictedLabel to original labels.
            // We need to read KeyValues for "PredictedLabel" column.

            VBuffer<float> keys = default;
            predEngine.OutputSchema["PredictedLabel"].GetKeyValues(ref keys);
            var labelsArray = keys.DenseValues().ToArray();

            // Since we apply MapValueToKey estimator with default parameters, key values
            // depends on order of occurence in data file. Which is "Iris-setosa", "Iris-versicolor", "Iris-virginica"
            // So if we have Score column equal to [0.2, 0.3, 0.5] that's mean what score for
            // Iris-setosa is 0.2
            // Iris-versicolor is 0.3
            // Iris-virginica is 0.5.
            //Add a dictionary to map the above float values to strings. 
            Dictionary<float, string> IrisFlowers = new Dictionary<float, string>();
            IrisFlowers.Add(0, "ru");
            IrisFlowers.Add(1, "en");
            IrisFlowers.Add(2, "es");
            IrisFlowers.Add(3, "dg");
            IrisFlowers.Add(4, "ept");

            Console.WriteLine("=====Predicting using model====");
            //Score sample 1
            wordInput wordInput = new wordInput() { Message = "хорошо" };
            var resultprediction1 = predEngine.Predict(wordInput);

            Console.WriteLine($"Actual: setosa.     Predicted label and score:  : {resultprediction1.Score[0]:0.####}");
            Console.WriteLine($"                                                : {resultprediction1.Score[1]:0.####}");
            Console.WriteLine($"                                                : {resultprediction1.Score[2]:0.####}");
            Console.WriteLine($"                                                : {resultprediction1.Score[3]:0.####}");
            Console.WriteLine($"                                                : {resultprediction1.Score[4]:0.####}");
            Console.WriteLine();

            //Console.WriteLine($"Actual: setosa.     Predicted label and score:  {IrisFlowers[labelsArray[0]]}: {resultprediction1.Score[0]:0.####}");
            //Console.WriteLine($"                                                {IrisFlowers[labelsArray[1]]}: {resultprediction1.Score[1]:0.####}");
            //Console.WriteLine($"                                                {IrisFlowers[labelsArray[2]]}: {resultprediction1.Score[2]:0.####}");
            //Console.WriteLine($"                                                {IrisFlowers[labelsArray[3]]}: {resultprediction1.Score[3]:0.####}");
            //Console.WriteLine($"                                                {IrisFlowers[labelsArray[4]]}: {resultprediction1.Score[4]:0.####}");
            //Console.WriteLine();

        }

        public static string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }
    }
}

