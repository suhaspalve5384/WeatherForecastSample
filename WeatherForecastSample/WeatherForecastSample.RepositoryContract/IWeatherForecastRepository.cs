using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecastSample.DAL;

namespace WeatherForecastSample.RepositoryContract
{
    public interface IWeatherForecastRepository
    {
        IEnumerable<Location> GetAll();

        Location GetById(int id);

        Location GetByLatitudeLongitude(decimal latitude, decimal longitude);

        void Add(Location location);

        void Delete(Location location);
    }
}
