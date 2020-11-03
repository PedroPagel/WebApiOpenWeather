using Newtonsoft.Json;
using Repositories.Base;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Requests
{
    public class OpenWeatherRequest
    {
        private List<OpenWeather> ResponseList { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }

        public OpenWeatherRequest()
        {
            ResponseList = new List<OpenWeather>();
        }

        public List<OpenWeather> Response()
        {
            return this.ResponseList;
        }

        public void Request(string city)
        {
            DateTime date = this.InitialDate;
            List<Root> tempList = new List<Root>();
            this.ResponseList.Clear();

            while (date <= this.FinalDate)
            {
                long epochTicks = new DateTime(1970, 1, 1).Ticks;
                long unixTime = ((date.Ticks - epochTicks) / TimeSpan.TicksPerSecond);

                var root = SearchCities(city, unixTime);
                root.Date = date;

                tempList.Add(root);

                date = date.AddDays(1);
            }

            foreach (Root root in tempList)
            {
                OpenWeather entity = new OpenWeather()
                {
                    Id = root.Id,
                    Name = root.Name,
                    Temperature = root.Main.Temp,
                    Date = root.Date
                };

                this.ResponseList.Add(entity);
            }
        }

        private Root SearchCities(string city, long date)
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/")
            };

            try
            {
                HttpResponseMessage response = client.GetAsync($"weather?q={city}&dt={date}&units=metric&appid=6c5c167cd5981883fd5bc448a0a69811").Result;

                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<Root>(json);
                }
                else
                {
                    return null;
                }

            } catch (Exception e)
            {
                throw e;
            }
        }
    }
}
