﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WeatherForecastSample.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WeatherForecastSampleEntities : DbContext
    {
        public WeatherForecastSampleEntities()
            : base("name=WeatherForecastSampleEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CurrentWeather> CurrentWeathers { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<DailyWeather> DailyWeathers { get; set; }
        public virtual DbSet<HourlyWeather> HourlyWeathers { get; set; }
    }
}