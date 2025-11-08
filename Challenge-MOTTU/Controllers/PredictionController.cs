using Challenge_MOTTU.ML;
using Challenge_MOTTU.Services;
using Microsoft.AspNetCore.Mvc;

namespace Challenge_MOTTU.Controllers
{
    /// <summary>
    /// Controller para gerenciamento da Prediction
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/predict")]
    public class PredictionController : ControllerBase
    {
        private readonly RentalPredictionService _predictionService;

        public PredictionController(RentalPredictionService predictionService)
        {
            _predictionService = predictionService;
        }

        /// <summary>
        /// Faz uma previsão da duração esperada de um aluguel com base nos dados informados.
        /// </summary>
        /// <param name="input">Dados da moto e uso</param>
        /// <returns>Previsão em horas</returns>
        [HttpPost]
        [ProducesResponseType(typeof(object), 200)]
        public IActionResult Predict([FromBody] RentalData input)
        {
            var prediction = _predictionService.Predict(input);
            return Ok(new
            {
                bikeYear = input.BikeYear,
                usageHours = input.UsageHours,
                isUrban = input.IsUrban == 1 ? "Urbano" : "Rural",
                predictedDuration = $"{prediction:F2} horas"
            });
        }
    }
}
