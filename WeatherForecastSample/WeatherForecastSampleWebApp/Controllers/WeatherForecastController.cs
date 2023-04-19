//-----------------------------------------------------------------------
// <copyright file="WeatherForecastController.cs" company="Sample Company" >
// Sample company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace WeatherForecastSampleWebApp.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using WeatherForecastSample.BLContract;
    using WeatherForecastSample.DAL;

    /// <summary>
    /// Weather Forecast Controller
    /// </summary>
    public class WeatherForecastController : ApiController
    {
        /// <summary>
        /// Weather Forecast BL object
        /// </summary>
        private IWeatherForecast weatherForecast;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherForecastController" /> class
        /// </summary>
        /// <param name="p_weatherForecast">Weather Forecast BL Object</param>
        public WeatherForecastController(IWeatherForecast p_weatherForecast)
        {
            this.weatherForecast = p_weatherForecast;
        }

        /// <summary>
        /// API to Get All Locations Weather Forecast
        /// </summary>
        /// <returns>List of Locations</returns>
        public IEnumerable<Location> GetAllLocationsWeatherForecast()
        {
            return this.weatherForecast.GetAllLocationWeatherForecast();
        }

        /// <summary>
        /// API to Get Weather Forecast for latitude and longitude
        /// </summary>
        /// <param name="latitude">Latitude of location</param>
        /// <param name="longitude">Longitude of location</param>
        /// <returns>Location object</returns>
        public Location Get(decimal latitude, decimal longitude)
        {
            return this.weatherForecast.GetWeatherForecast(latitude, longitude);
        }

        /// <summary>
        /// Add Location API
        /// </summary>
        /// <param name="value">Location object</param>
        public void Post(Location value)
        {
            this.weatherForecast.AddLocation(value);
        }

        /// <summary>
        /// Update existing Location API
        /// </summary>
        /// <param name="locationId">Location id</param>
        /// <param name="value">Location object</param>
        public void Put(int locationId, Location value)
        {
            this.weatherForecast.UpdateLocation(locationId, value);
        }

        /// <summary>
        /// Delete Location API
        /// </summary>
        /// <param name="locationId">Location id</param>
        public void Delete(int locationId)
        {
            this.weatherForecast.DeleteLocation(locationId);
        }
    }
}