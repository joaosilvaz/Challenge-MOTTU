using Microsoft.ML;
using Challenge_MOTTU.ML;

namespace Challenge_MOTTU.Services
{
    public class RentalPredictionService
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;

        public RentalPredictionService()
        {
            _mlContext = new MLContext();

            // Dados simulados
            var data = new List<RentalData>
            {
                new RentalData { BikeYear = 2020, UsageHours = 100, IsUrban = 1 },
                new RentalData { BikeYear = 2019, UsageHours = 250, IsUrban = 0 },
                new RentalData { BikeYear = 2022, UsageHours = 50,  IsUrban = 1 },
                new RentalData { BikeYear = 2023, UsageHours = 10,  IsUrban = 1 }
            };

            var trainingData = _mlContext.Data.LoadFromEnumerable(data);

            // Pipeline simples: combinar colunas e usar regressão linear
            var pipeline = _mlContext.Transforms.Concatenate("Features", "BikeYear", "UsageHours", "IsUrban")
                .Append(_mlContext.Regression.Trainers.Sdca(labelColumnName: "BikeYear", maximumNumberOfIterations: 100));

            _model = pipeline.Fit(trainingData);
        }

        public float Predict(RentalData input)
        {
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<RentalData, RentalPrediction>(_model);
            var result = predictionEngine.Predict(input);
            return result.PredictedDuration;
        }
    }
}
