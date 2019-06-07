using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ZipcodeInfo.DomainClasses;

namespace Tests.UnitTests
{
    class WeatherInfoTests
    {
        [Test]
        public void TempInFahrenheit_300Kelvin_8point33Farenheit()
        {
            WeatherInfo weatherInfo = new WeatherInfo() {TempKelvin = 300};
            var tempFahrenheit = weatherInfo.TempFahrenheit;

            Assert.AreEqual(80.330000000000041d, tempFahrenheit);
        }
    }
}
