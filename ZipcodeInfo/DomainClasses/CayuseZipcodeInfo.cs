using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZipcodeInfo.DomainClasses
{
    public class CayuseZipcodeInfo
    {
        public string CityName { get; set; }
        public double CurrentTempFahrenheit { get; set; }
        public string TimezoneName { get; set; }
        public double Elevation { get; set; }
        public string Message
        {
            get =>
                $"At the location {CityName}, the temperature is {CurrentTempFahrenheit}, the timezone is {TimezoneName}, and the elevation is {Elevation}.";
        } 
    }
}
