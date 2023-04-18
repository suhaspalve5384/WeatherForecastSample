using System.Collections.Generic;
using System.Web.Http;
using WeatherForecastSample.BLContract;
using WeatherForecastSample.DAL;

namespace WeatherForecastSampleWebApp.Controllers
{
    public class WheatherForecastController : ApiController
    {

        IWeatherForecast weatherForecast;

        public WheatherForecastController(IWeatherForecast p_weatherForecast)
        {
            this.weatherForecast = p_weatherForecast;
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public Location Get(decimal latitude, decimal longitude)
        {
            return weatherForecast.GetWheatherForecast(latitude, longitude);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int LocationId, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int LocationId)
        {
        }
    }
}