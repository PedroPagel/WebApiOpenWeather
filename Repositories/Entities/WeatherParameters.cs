using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Entities
{
    public class WeatherParameters
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
    }
}
