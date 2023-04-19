//-----------------------------------------------------------------------
// <copyright file="IWeatherForecastRepository.cs" company="Sample Company" >
// Sample company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace WeatherForecastSample.RepositoryContract
{
    using System.Collections.Generic;
    using WeatherForecastSample.DAL;

    /// <summary>
    /// Weather Forecast Repository Contract
    /// </summary>
    public interface IWeatherForecastRepository
    {
        /// <summary>
        /// Get All Location
        /// </summary>
        /// <returns>All Location list</returns>
        IEnumerable<Location> GetAll();

        /// <summary>
        /// Get Location By Id
        /// </summary>
        /// <param name="id">Location Id</param>
        /// <returns>Location object</returns>
        Location GetById(int id);

        /// <summary>
        /// Get Location by Latitude and Longitude
        /// </summary>
        /// <param name="latitude">Latitude of location</param>
        /// <param name="longitude">Longitude of location</param>
        /// <returns>Location object</returns>
        Location GetByLatitudeLongitude(decimal latitude, decimal longitude);

        /// <summary>
        /// Add Location object to DB
        /// </summary>
        /// <param name="location">Location object</param>
        void Add(Location location);

        /// <summary>
        /// Delete Location object from DB
        /// </summary>
        /// <param name="location">Location object</param>
        void Delete(Location location);

        /// <summary>
        /// Update Location object from DB
        /// </summary>
        /// <param name="locationId">Location Id</param>
        /// <param name="location">Location object</param>
        void Update(int locationId, Location location);
    }
}
