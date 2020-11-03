using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Base
{
    public static class Utils
    {
        public static string[] GetAllCities()
        {
            List<string> citiesList = new List<string>
            {
                "Florianopolis",
                "Sao Paulo",
                "Rio de Janeiro"
            };

            return citiesList.ToArray();
        }

        public static bool CheckCity(string city)
        {
            return new List<string>(GetAllCities()).Contains(city);
        }
    }
}
