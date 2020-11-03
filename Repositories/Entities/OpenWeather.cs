using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Entities
{
    public class OpenWeather
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Temperature { get; set; }
        public DateTime Date { get; set; }
    }
}
