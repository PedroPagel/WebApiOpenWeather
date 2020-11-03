using Repositories.Base;
using Repositories.Entities;
using Repositories.Intefaces;
using Repositories.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Weather.Repositories.Scheduling
{
    public class WeatherScheduling
    {
        private OpenWeatherRequest OpenWeatherRequest { get; set; }
        private int Minutes { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
        public string Name { get; set; }

        public WeatherScheduling(int minutes)
        {
            this.Minutes = minutes;
            this.OpenWeatherRequest = new OpenWeatherRequest();
        }

        //Não é dito se existe um fim para a aplicação no teste, então na teoria ficaria rodando até finalizar a app.
        public async Task Start()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();

            await Task.Run(async () =>
            {
                while (!tokenSource.IsCancellationRequested)
                {
                    this.OpenWeatherRequest.InitialDate = this.InitialDate;
                    this.OpenWeatherRequest.FinalDate = this.FinalDate;
                    this.OpenWeatherRequest.Request(this.Name);
                    await Task.Delay(Minutes * 60000);
                }
            });
        }

        public List<OpenWeather> GetAllWeatherScheduling()
        {
            return this.OpenWeatherRequest.Response();
        }
    }
}
