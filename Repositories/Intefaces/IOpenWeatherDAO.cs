using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Intefaces
{
    public interface IOpenWeatherDAO
    {
        Task<List<OpenWeather>> ListASync();
        Task AddASync(OpenWeather openWeather);
        Task<OpenWeather> FindCityTemp(string city, DateTime date);
        void Update(OpenWeather openWeather);
        void Remove(OpenWeather openWeather);
    }
}
