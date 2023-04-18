using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecastSample.DAL;

namespace WeatherForecastSample.BLContract
{
    public interface IWeatherForecast
    {
        Location GetWheatherForecast(decimal latitude, decimal longitude);
    }
}
