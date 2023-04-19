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
    using System;
    using System.Collections.Generic;
    
    public partial class Location
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Location()
        {
            this.CurrentWeathers = new HashSet<CurrentWeather>();
            this.DailyWeathers = new HashSet<DailyWeather>();
            this.HourlyWeathers = new HashSet<HourlyWeather>();
        }
    
        public int LocationId { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public string timezone { get; set; }
        public string timezone_abbreviation { get; set; }
        public Nullable<decimal> elevation { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CurrentWeather> CurrentWeathers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DailyWeather> DailyWeathers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HourlyWeather> HourlyWeathers { get; set; }
    }
}
