using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repositories.Base;

namespace WebApiSwagger.Controllers
{
    [Route("api/[controller]")]
    public class OpenWeatherController : ControllerBase
    {
        private readonly ILogger<OpenWeatherController> _logger;

        public OpenWeatherController(ILogger<OpenWeatherController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Retorna a temperatura de uma cidade
        /// dentro de um período de data(s) (formato americano).
        /// </summary>
        /// <param name="city">Cidade</param>
        /// <param initialDate="initialDate">Data inicial</param>
        /// <param finalDate="finalDate">Data final</param>
        /// <returns>Objeto contendo valores de temperatura
        /// das cidade.</returns>
        [HttpGet]
        public object Get(string city, DateTime? initialDate, DateTime? finalDate)
        {
            if (initialDate > finalDate)
            {
                return new Response() { Code = "002", Message = "Data inicial maior que a final"};
            }

            var rng = new Random();

            if (initialDate.HasValue && finalDate.HasValue)
            {
                if (string.IsNullOrEmpty(city))
                {
                    return new Response() { Code = "002", Message = "Cidade não informada!" };
                }

                List<OpenWeather> list = new List<OpenWeather>();

                while (initialDate <= finalDate)
                {
                    OpenWeather openWeather = new OpenWeather
                    {
                        Id = list.Count + 1,
                        Date = initialDate.Value,
                        Temperature = rng.Next(-20, 55),
                        Name = city
                    };

                    initialDate = initialDate.Value.AddDays(1);
                    list.Add(openWeather);
                }

                return list.ToArray();
            }

            //Exemplo padrão
            return Enumerable.Range(1, 5).Select(index => new OpenWeather
            {
                Id = index,
                Date = DateTime.Now.AddDays(index),
                Temperature = rng.Next(-20, 55),
                Name = Utils.GetAllCities()[rng.Next(Utils.GetAllCities().Length)]
            })
            .ToArray();
        }
    }
}
