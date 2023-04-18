using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecastSample.DAL;
using WeatherForecastSample.RepositoryContract;

namespace WeatherForecastSample.Repository
{
    public class WeatherForecastRepository : IWeatherForecastRepository, IDisposable
    {
        private WeatherForecastSampleEntities weatherForecastSampleEntities = new WeatherForecastSampleEntities();

        public IEnumerable<Location> GetAll()
        {
            return weatherForecastSampleEntities.Locations;
        }

        public Location GetById(int locationId)
        {
            return weatherForecastSampleEntities.Locations.FirstOrDefault(l => l.LocationId == locationId);
        }

        public Location GetByLatitudeLongitude(decimal latitude, decimal longitude)
        {
            return weatherForecastSampleEntities.Locations.FirstOrDefault(l => l.latitude == latitude && l.longitude == longitude);
        }

        public void Add(Location location)
        {
            weatherForecastSampleEntities.Locations.Add(location);
            weatherForecastSampleEntities.SaveChanges();
        }

        public void Delete(Location location)
        {
            weatherForecastSampleEntities.Locations.Remove(location);
            weatherForecastSampleEntities.SaveChanges();
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (weatherForecastSampleEntities != null)
                {
                    weatherForecastSampleEntities.Dispose();
                    weatherForecastSampleEntities = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
