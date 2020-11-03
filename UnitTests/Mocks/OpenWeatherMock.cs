using NSubstitute;
using Repositories.Entities;
using Repositories.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Mocks
{
    internal class OpenWeatherMock : IOpenWeatherDAO
    {
        private readonly List<OpenWeather> _openWeatherDAOs = new List<OpenWeather>();

        public async Task AddASync(OpenWeather openWeather)
        {
            openWeather.Id = _openWeatherDAOs.Count + 1;
            await Task.Run(() => _openWeatherDAOs.Add(openWeather));
        }

        public async Task<OpenWeather> FindCityTemp(string city, DateTime date)
        {
            return await Task.Run(() => _openWeatherDAOs.Where(c => c.Name.Equals(city) && c.Date == date).FirstOrDefault());
        }

        public async Task<List<OpenWeather>> ListASync()
        {
            return await Task.Run(() => _openWeatherDAOs.FindAll(x => x.Id > 0));
        }

        public void Remove(OpenWeather openWeather)
        {
            throw new NotImplementedException();
        }

        public void Update(OpenWeather openWeather)
        {
            var entity = _openWeatherDAOs.Where(x => x.Id == openWeather.Id).FirstOrDefault();
            
            if (entity != null)
            {
                _openWeatherDAOs.Remove(entity);
                _openWeatherDAOs.Add(entity);
            }
        }
    }
}
