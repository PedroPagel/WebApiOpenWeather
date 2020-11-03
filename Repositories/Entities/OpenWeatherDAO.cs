using Microsoft.EntityFrameworkCore;
using Repositories.Base;
using Repositories.Context;
using Repositories.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class OpenWeatherDAO : ContextBase, IOpenWeatherDAO
    {
        public OpenWeatherDAO(WebAppContext context) : base(context)
        {
        }

        public async Task AddASync(OpenWeather openWeather)
        {
            await _context.AddAsync(openWeather);
            _context.SaveChanges();
        }

        public async Task<OpenWeather> FindCityTemp(string city, DateTime date)
        {
            return await _context.OpenWeather.Where(c => c.Name.Equals(city) && c.Date == date).FirstOrDefaultAsync();
        }

        public async Task<List<OpenWeather>> ListASync()
        {
            return await _context.OpenWeather.ToListAsync();
        }

        public void Remove(OpenWeather openWeather)
        {
            _context.Remove(openWeather);
        }

        public void Update(OpenWeather openWeather)
        {
            _context.Update(openWeather);
            _context.SaveChanges();
        }
    }
}
