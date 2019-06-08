using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ZipcodeInfo.DomainClasses
{
    public class WeatherInfo
    {
        public double TempKelvin { get; set; }
        public string City { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }


        public double TempFahrenheit => ((TempKelvin-273.15)*9/5)+32; 
        
    }
}
