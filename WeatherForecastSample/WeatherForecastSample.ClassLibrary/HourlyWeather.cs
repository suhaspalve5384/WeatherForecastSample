//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WeatherForecastSample.DAL
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class HourlyWeather
    {
        public int HourlyWeatherId { get; set; }
        public int LocationId { get; set; }
        public Nullable<System.DateTime> time { get; set; }
        public string wheathertype { get; set; }
        public Nullable<decimal> wheatherTypeValue { get; set; }
    
        [JsonIgnore]
        public virtual Location Location { get; set; }
    }
}
