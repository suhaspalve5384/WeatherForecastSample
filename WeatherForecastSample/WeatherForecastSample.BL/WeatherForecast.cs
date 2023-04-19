//-----------------------------------------------------------------------
// <copyright file="WeatherForecast.cs" company="Sample Company" >
// Sample company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace WeatherForecastSample.BL
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Net.Http;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using WeatherForecastSample.BLContract;
    using WeatherForecastSample.DAL;
    using WeatherForecastSample.RepositoryContract;

    /// <summary>
    /// Weather Forecast BL
    /// </summary>
    public class WeatherForecast : IWeatherForecast
    {
        /// <summary>
        /// Weather Forecast Repository object
        /// </summary>
        private IWeatherForecastRepository weatherForecastRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherForecast" /> class
        /// </summary>
        /// <param name="p_weatherForecastRepository">Weather Forecast Repository</param>
        public WeatherForecast(IWeatherForecastRepository p_weatherForecastRepository)
        {
            this.weatherForecastRepository = p_weatherForecastRepository;
        }

        /// <summary>
        /// Get All Location Weather Forecast
        /// </summary>
        /// <returns>All location list</returns>
        public IEnumerable<Location> GetAllLocationWeatherForecast()
        {
            return this.weatherForecastRepository.GetAll();
        }

        /// <summary>
        /// Get Weather Forecast
        /// </summary>
        /// <param name="latitude">Latitude of location</param>
        /// <param name="longitude">Longitude of location</param>
        /// <returns>Location object</returns>
        public Location GetWeatherForecast(decimal latitude, decimal longitude)
        {
            //// Get location of given latitude & longitude in DB
            Location locationWeatherForecast = this.weatherForecastRepository.GetByLatitudeLongitude(latitude, longitude);

            ////If location not found in DB
            if (locationWeatherForecast == null)
            {
                //// Get Location Weather forecast from Web API
                locationWeatherForecast = this.GetWeatherForecastFromAPI(latitude, longitude);

                //// Check if location present in DB for Web API output latitude & longitude, as there might be som change in latitude & longitude of Web API output
                if (this.weatherForecastRepository.GetByLatitudeLongitude(locationWeatherForecast.latitude, locationWeatherForecast.longitude) == null)
                {
                    //// Add location in DB from Web API output
                    this.weatherForecastRepository.Add(locationWeatherForecast);
                }
            }

            return locationWeatherForecast;
        }

        /// <summary>
        /// Add location
        /// </summary>
        /// <param name="location">Location object</param>
        public void AddLocation(Location location)
        {
            this.weatherForecastRepository.Add(location);
        }

        /// <summary>
        /// Delete Location
        /// </summary>
        /// <param name="locationId">Location Id</param>
        public void DeleteLocation(int locationId)
        {
            this.weatherForecastRepository.Delete(this.weatherForecastRepository.GetById(locationId));
        }

        /// <summary>
        /// Update Location
        /// </summary>
        /// <param name="locationId">Location id</param>
        /// <param name="location">Location object</param>
        public void UpdateLocation(int locationId, Location location)
        {
            this.weatherForecastRepository.Update(locationId, location);
        }

        /// <summary>
        /// Get Weather Forecast From Web API
        /// </summary>
        /// <param name="latitude">Latitude of location</param>
        /// <param name="longitude">Longitude of location</param>
        /// <returns>Location object </returns>
        private Location GetWeatherForecastFromAPI(decimal latitude, decimal longitude)
        {
            Location locationWeatherForecast = new Location();

            //// HTTP Client to invoke Web API
            using (HttpClient httpClient = new HttpClient())
            {
                //// Get We API out for Weather forecast of given latitude & longitude 
                HttpResponseMessage responceMessge = httpClient.GetAsync($"{ConfigurationManager.AppSettings["WeatherForecastWebAPIUrl"]}latitude={latitude}&longitude={longitude}&hourly={ConfigurationManager.AppSettings["HourlyWeatherVariables"]}&daily={ConfigurationManager.AppSettings["DailyWeatherVariables"]}&current_weather={ConfigurationManager.AppSettings["CurrentWeather"]}&timezone={ConfigurationManager.AppSettings["Timezone"]}").Result;

                //// Check if response is successful
                if (responceMessge.IsSuccessStatusCode)
                {
                    //// Get Web API output string
                    string result = responceMessge.Content.ReadAsStringAsync().Result;

                    //// Read output in Location object
                    locationWeatherForecast = JsonConvert.DeserializeObject<Location>(result);

                    dynamic data = JsonConvert.DeserializeObject(result);

                    //// Get Child list object in Location
                    this.GetLocationObjectFromDataTable(ref locationWeatherForecast, data);
                }
            }

            return locationWeatherForecast;
        }

        /// <summary>
        /// Get Location Object From DataTable
        /// </summary>
        /// <param name="locationWeatherForecast">Reference Location object in which need to set values</param>
        /// <param name="data">Dynamic data from Web API</param>
        private void GetLocationObjectFromDataTable(ref Location locationWeatherForecast, dynamic data)
        {
            //// If output object has hourly object
            if (data.hourly != null && data.hourly.time != null && data.hourly.time.Count > 0)
            {
                locationWeatherForecast.HourlyWeathers = new List<HourlyWeather>();

                foreach (string v in ConfigurationManager.AppSettings["HourlyWeatherVariables"].Split(','))
                {
                    for (int i = 0; i < data.hourly.time.Count; i++)
                    {
                        locationWeatherForecast.HourlyWeathers.Add(new HourlyWeather() { Time = data.hourly.time[i], Weathertype = v, WeatherTypeValue = this.GetObjectPropertyValue(data.hourly, v, i) });
                    }
                }
            }

            if (data.daily != null && data.daily.time != null && data.daily.time.Count > 0)
            {
                locationWeatherForecast.DailyWeathers = new List<DailyWeather>();

                foreach (string v in ConfigurationManager.AppSettings["DailyWeatherVariables"].Split(','))
                {
                    for (int i = 0; i < data.daily.time.Count; i++)
                    {
                        locationWeatherForecast.DailyWeathers.Add(new DailyWeather() { Time = data.daily.time[i], WeatherType = v, WeatherTypeValue = this.GetObjectPropertyValue(data.daily, v, i) });
                    }
                }
            }

            if (data.current_weather != null)
            {
                locationWeatherForecast.CurrentWeathers = new List<CurrentWeather>();

                locationWeatherForecast.CurrentWeathers.Add(new CurrentWeather() { temperature = data.current_weather.temperature, windspeed = data.current_weather.windspeed, winddirection = data.current_weather.winddirection, weathercode = data.current_weather.weathercode, is_day = data.current_weather.is_day, time = data.current_weather.time });
            }
        }

        /// <summary>
        /// Get Value of required property in dynamic object
        /// </summary>
        /// <param name="obj">Dynamic object</param>
        /// <param name="name">Name of Property</param>
        /// <param name="index">Index for Property value</param>
        /// <returns>Value of property</returns>
        private decimal GetObjectPropertyValue(dynamic obj, string name, int index)
        {
            ////int itemIndex = 0;

            //return ((JObject)obj).Values(name)[name][index].ToString();

            ////for (int i = 0; i < ((JObject)obj).Count; i++)
            ////{
            ////    if (((JObject)obj).Values(name))
            ////    {
            ////        itemIndex = i;
            ////        break;
            ////    }
            ////}

            ////return obj.ChildrenTokens.Items[itemIndex][index];
            ///
            switch (name)
            {
                case "temperature_2m":
                    return obj.temperature_2m[index];
                case "rain":
                    return obj.rain[index];
                case "surface_pressure":
                    return obj.surface_pressure[index];
                case "visibility":
                    return obj.visibility[index];
                case "temperature_2m_max":
                    return obj.temperature_2m_max[index];
                case "temperature_2m_min":
                    return obj.temperature_2m_min[index];
                case "rain_sum":
                    return obj.rain_sum[index];
            }

            return decimal.Zero;
        }
    }
}
