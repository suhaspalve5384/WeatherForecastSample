//-----------------------------------------------------------------------
// <copyright file="IWeatherForecast.cs" company="Sample Company" >
// Sample company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace WeatherForecastSample.BLContract
{
    using System.Collections.Generic;
    using WeatherForecastSample.DAL;

    /// <summary>
    /// Weather Forecast BL Contract
    /// </summary>
    public interface IWeatherForecast
    {
        /// <summary>
        /// Get All Location Weather Forecast
        /// </summary>
        /// <returns>List of Location</returns>
        IEnumerable<Location> GetAllLocationWeatherForecast();

        /// <summary>
        /// Get Weather Forecast
        /// </summary>
        /// <param name="latitude">Latitude of location</param>
        /// <param name="longitude">Longitude of location</param>
        /// <returns>Location object</returns>
        Location GetWeatherForecast(decimal latitude, decimal longitude);

        /// <summary>
        /// Add location
        /// </summary>
        /// <param name="location">Location object</param>
        void AddLocation(Location location);

        /// <summary>
        /// Delete Location
        /// </summary>
        /// <param name="locationId">Location Id</param>
        void DeleteLocation(int locationId);

        /// <summary>
        /// Update Location
        /// </summary>
        /// <param name="locationId">Location id</param>
        /// <param name="location">Location object</param>
        void UpdateLocation(int locationId, Location location);
    }
}
