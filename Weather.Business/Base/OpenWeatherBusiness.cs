using Repositories.Base;
using Repositories.Entities;
using Repositories.Intefaces;
using Repositories.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Weather.Repositories.Base;
using Weather.Repositories.Scheduling;

namespace Weather.Business.Base
{
    public class OpenWeatherBusiness : IOpenWeatherBusiness
    {
        private readonly IOpenWeatherDAO _openWeatherDAO;

        public OpenWeatherBusiness(IOpenWeatherDAO openWeatherDAO)
        {
            this._openWeatherDAO = openWeatherDAO;
        }

        public async Task<IEnumerable<OpenWeather>> ListASync()
        {
            return await _openWeatherDAO.ListASync();
        }

        public async Task StartTask(string name, DateTime initialDate, DateTime finalDate)
        {
            this.Checks(initialDate, finalDate, name);

            if (SchedulingBase.Task == null)
            {
                SchedulingBase.Task = new WeatherScheduling(15)
                {
                    InitialDate = initialDate,
                    FinalDate = finalDate,
                    Name = name
                };

                await SchedulingBase.Task.Start();
            }
        }

        //Executa sem gerar um agendamento
        public List<OpenWeather> Request(string name, DateTime initialDate, DateTime finalDate)
        {
            this.Checks(initialDate, finalDate, name);

            OpenWeatherRequest openWeatherRequest = new OpenWeatherRequest()
            {
                InitialDate = initialDate,
                FinalDate = finalDate
            };

            openWeatherRequest.Request(name);
            return openWeatherRequest.Response();
        }

        public void Checks(DateTime initialDate, DateTime finalDate, string name)
        {
            if (finalDate < initialDate)
            {
                throw new System.InvalidOperationException("Data inicial não pode ser maior que a final");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new System.InvalidOperationException("Cidade não informada");
            }

            //Como foi dito que eram apenas 3 cidades, fica determinado a busca somente dessas 3
            if (!Utils.CheckCity(name))
            {
                throw new System.InvalidOperationException("Cidade não compatível com a lista de cidades");
            }
        }

        public async Task Execute(List<OpenWeather> list)
        { 
            foreach (var item in list)
            {
                var openWeatherDAO = _openWeatherDAO.FindCityTemp(item.Name, item.Date).Result;

                if (openWeatherDAO == null)
                {
                    OpenWeather openWeather = new OpenWeather()
                    {
                        Name = item.Name,
                        Date = item.Date,
                        Temperature = item.Temperature
                    };

                    await _openWeatherDAO.AddASync(openWeather);
                }
                else
                {
                    openWeatherDAO.Temperature = item.Temperature;
                    _openWeatherDAO.Update(openWeatherDAO);
                }
            }
        }
    }
}
