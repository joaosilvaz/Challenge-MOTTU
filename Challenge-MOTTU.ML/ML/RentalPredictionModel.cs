using Microsoft.ML.Data;

namespace Challenge_MOTTU.ML
{
    public class RentalData
    {
        [LoadColumn(0)]
        public float BikeYear { get; set; }

        [LoadColumn(1)]
        public float UsageHours { get; set; }

        [LoadColumn(2)]
        public float IsUrban { get; set; }
    }

    public class RentalPrediction
    {
        [ColumnName("Score")]
        public float PredictedDuration { get; set; }
    }
}
