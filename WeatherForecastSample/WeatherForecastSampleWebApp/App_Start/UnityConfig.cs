// <auto-generated />

using System.Web.Http;
using Unity;
using Unity.WebApi;
using WeatherForecastSample.BL;
using WeatherForecastSample.BLContract;
using WeatherForecastSample.Repository;
using WeatherForecastSample.RepositoryContract;

namespace WeatherForecastSampleWebApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IWeatherForecastRepository, WeatherForecastRepository>();
            container.RegisterType<IWeatherForecast, WeatherForecast>();

            //config.DependencyResolver = new UnityResolver(container);



            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        //public static void Register(HttpConfiguration config)
        //{
        //    var container = new UnityContainer();

        //    // Your mappings here

        //    config.DependencyResolver = new UnityDependencyResolver(container);
        //}
    }
}