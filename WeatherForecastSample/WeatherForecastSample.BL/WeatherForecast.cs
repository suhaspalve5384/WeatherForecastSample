using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecastSample.DAL;
using WeatherForecastSample.BLContract;
using System.Net.Http;
using System.Data;
using Newtonsoft.Json;
using WeatherForecastSample.RepositoryContract;

namespace WeatherForecastSample.BL
{
    public class WeatherForecast : IWeatherForecast
    {
        private IWeatherForecastRepository weatherForecastRepository;

        public WeatherForecast(IWeatherForecastRepository p_weatherForecastRepository)
        {
            this.weatherForecastRepository = p_weatherForecastRepository;
        }

        public Location GetWheatherForecast(decimal latitude, decimal longitude)
        {
            Location locationWheatherForecast = weatherForecastRepository.GetByLatitudeLongitude(latitude, longitude);

            if (locationWheatherForecast == null)
            {
                locationWheatherForecast = GetWheatherForecastFromAPI(latitude, longitude);

                if (weatherForecastRepository.GetByLatitudeLongitude(locationWheatherForecast.latitude, locationWheatherForecast.longitude) == null)
                {
                    weatherForecastRepository.Add(locationWheatherForecast);
                }
            }

            return locationWheatherForecast;
        }

        private Location GetWheatherForecastFromAPI(decimal latitude, decimal longitude)
        {
            Location locationWheatherForecast = new Location();

            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage responceMessge = httpClient.GetAsync($"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&hourly=temperature_2m").Result;

                if (responceMessge.IsSuccessStatusCode)
                {
                    string result = responceMessge.Content.ReadAsStringAsync().Result;

                    locationWheatherForecast = JsonConvert.DeserializeObject<Location>(result);

                    dynamic data = JsonConvert.DeserializeObject(result);

                    GetLocationObjectFromDataTable(ref locationWheatherForecast, data);
                }
            }

            return locationWheatherForecast;
        }

        private void GetLocationObjectFromDataTable(ref Location locationWheatherForecast, dynamic data)
        {
            if (data.hourly != null && data.hourly.time != null && data.hourly.time.Count > 0)
            {
                locationWheatherForecast.HourlyWeathers = new List<HourlyWeather>();

                for (int i = 0; i < data.hourly.time.Count; i++)
                {
                    locationWheatherForecast.HourlyWeathers.Add(new HourlyWeather() { time = data.hourly.time[i], wheathertype = "temperature_2m", wheatherTypeValue = data.hourly.temperature_2m[i] });
                }
            }
        }
    }
}
