using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repositories.Entities;
using Repositories.Intefaces;
using Weather.Business.Base;
using Weather.Repositories.Scheduling;
using Weather.Repositories.Base;

namespace WebApiWeatherMap.Controllers
{
    [Route("api/[controller]")]
    public class OpenWeather : Controller
    {
        private readonly IOpenWeatherBusiness _openWeather;

        public OpenWeather(IOpenWeatherBusiness openWeather)
        {
            this._openWeather = openWeather;
            this._openWeather.StartTask(Parameters.Params.Name, Parameters.Params.InitialDate, Parameters.Params.FinalDate);
        }

        [HttpGet]
        public async Task<IEnumerable<Repositories.Entities.OpenWeather>> GetCitiesASync()
        {
            _openWeather.Execute(SchedulingBase.Task.GetAllWeatherScheduling()).Wait();
            var weather = await _openWeather.ListASync();
            return weather.ToArray();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
