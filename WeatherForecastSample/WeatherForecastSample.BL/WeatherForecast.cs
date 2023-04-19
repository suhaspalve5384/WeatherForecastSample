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
using System.Configuration;

namespace WeatherForecastSample.BL
{
    public class WeatherForecast : IWeatherForecast
    {
        private IWeatherForecastRepository weatherForecastRepository;

        public WeatherForecast(IWeatherForecastRepository p_weatherForecastRepository)
        {
            this.weatherForecastRepository = p_weatherForecastRepository;
        }

        public IEnumerable<Location> GetAllLocationWheatherForecast()
        {
            return weatherForecastRepository.GetAll();
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
                HttpResponseMessage responceMessge = httpClient.GetAsync($"{ConfigurationManager.AppSettings["WheatherForecastWebAPIUrl"]}latitude={latitude}&longitude={longitude}&hourly={ConfigurationManager.AppSettings["HourlyWheatherVariables"]}&daily={ConfigurationManager.AppSettings["DailyWheatherVariables"]}&current_weather={ConfigurationManager.AppSettings["CurrentWeather"]}&timezone={ConfigurationManager.AppSettings["Timezone"]}").Result;

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

                foreach (string v in ConfigurationManager.AppSettings["HourlyWheatherVariables"].Split(','))
                {
                    for (int i = 0; i < data.hourly.time.Count; i++)
                    {
                        locationWheatherForecast.HourlyWeathers.Add(new HourlyWeather() { time = data.hourly.time[i], wheathertype = v, wheatherTypeValue = data.hourly.v[i] });
                    }
                }
            }

            if (data.daily != null && data.daily.time != null && data.daily.time.Count > 0)
            {
                locationWheatherForecast.DailyWeathers = new List<DailyWeather>();

                foreach (string v in ConfigurationManager.AppSettings["DailyWheatherVariables"].Split(','))
                {
                    for (int i = 0; i < data.daily.time.Count; i++)
                    {
                        locationWheatherForecast.DailyWeathers.Add(new DailyWeather() { time = data.daily.time[i], wheathertype = v, wheatherTypeValue = data.daily.v[i] });
                    }
                }
            }

            if (data.current_weather != null)
            {
                locationWheatherForecast.CurrentWeathers = new List<CurrentWeather>();

                locationWheatherForecast.CurrentWeathers.Add(new CurrentWeather() { temperature = data.current_weather.temperature, windspeed = data.current_weather.windspeed, winddirection = data.current_weather.winddirection, weathercode = data.current_weather.weathercode, is_day = data.current_weather.is_day, time = data.current_weather.time });
            }
        }
    }
}
