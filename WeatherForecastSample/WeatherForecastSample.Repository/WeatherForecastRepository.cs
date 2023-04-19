//-----------------------------------------------------------------------
// <copyright file="WeatherForecastRepository.cs" company="Sample Company" >
// Sample company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace WeatherForecastSample.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WeatherForecastSample.DAL;
    using WeatherForecastSample.RepositoryContract;

    /// <summary>
    /// Weather Forecast Repository
    /// </summary>
    public class WeatherForecastRepository : IWeatherForecastRepository, IDisposable
    {
        /// <summary>
        /// Weather Forecast Sample Entities object
        /// </summary>
        private WeatherForecastSampleEntities weatherForecastSampleEntities = new WeatherForecastSampleEntities();

        /// <summary>
        /// Get All Location
        /// </summary>
        /// <returns>All Location list</returns>
        public IEnumerable<Location> GetAll()
        {
            return this.weatherForecastSampleEntities.Locations;
        }

        /// <summary>
        /// Get Location By Id
        /// </summary>
        /// <param name="locationId">Location Id</param>
        /// <returns>Location object</returns>
        public Location GetById(int locationId)
        {
            return this.weatherForecastSampleEntities.Locations.FirstOrDefault(l => l.LocationId == locationId);
        }

        /// <summary>
        /// Get Location by Latitude and Longitude
        /// </summary>
        /// <param name="latitude">Latitude of location</param>
        /// <param name="longitude">Longitude of location</param>
        /// <returns>Location object</returns>
        public Location GetByLatitudeLongitude(decimal latitude, decimal longitude)
        {
            return this.weatherForecastSampleEntities.Locations.FirstOrDefault(l => l.latitude == latitude && l.longitude == longitude);
        }

        /// <summary>
        /// Add Location object to DB
        /// </summary>
        /// <param name="location">Location object</param>
        public void Add(Location location)
        {
            this.weatherForecastSampleEntities.Locations.Add(location);
            this.weatherForecastSampleEntities.SaveChanges();
        }

        /// <summary>
        /// Delete Location object from DB
        /// </summary>
        /// <param name="location">Location object</param>
        public void Delete(Location location)
        {
            this.weatherForecastSampleEntities.Locations.Remove(location);
            this.weatherForecastSampleEntities.SaveChanges();
        }

        /// <summary>
        /// Update Location object from DB
        /// </summary>
        /// <param name="locationId">Location Id</param>
        /// <param name="location">Location object</param>
        public void Update(int locationId, Location location)
        {
            Location existingLocation = this.weatherForecastSampleEntities.Locations.FirstOrDefault(l => l.LocationId == locationId);

            if (existingLocation != null)
            {
                this.weatherForecastSampleEntities.Entry<Location>(existingLocation).CurrentValues.SetValues(location);

                this.weatherForecastSampleEntities.SaveChanges();
            }
        }

        /// <summary>
        /// Dispose Weather Forecast Sample Entities object
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose Weather Forecast Sample Entities object
        /// </summary>
        /// <param name="disposing">Is Disposing</param>
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.weatherForecastSampleEntities != null)
                {
                    this.weatherForecastSampleEntities.Dispose();
                    this.weatherForecastSampleEntities = null;
                }
            }
        }
    }
}
