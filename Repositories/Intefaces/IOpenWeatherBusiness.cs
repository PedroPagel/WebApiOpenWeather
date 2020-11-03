using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Intefaces
{
    public interface IOpenWeatherBusiness
    {
        Task<IEnumerable<OpenWeather>> ListASync();
        Task Execute(List<OpenWeather> list);
        Task StartTask(string name, DateTime initialDate, DateTime finalDate);
    }
}
