using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ZipcodeInfo.ApiClients;
using ZipcodeInfo.DomainClasses;

namespace Tests.IntegrationTests
{
    class OpenWeatherApiClientTests
    {
        [Test]
        public async Task GetWeather_ReturnsWeather()
        {
            //arrange
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory.Setup(f => f.CreateClient("openweather")).Returns(new HttpClient());
            var client = new OpenWeatherApiClient(mockHttpClientFactory.Object);
            var zipcode = new Zipcode("97219");

            //act
            ApiResponse<WeatherInfo> apiResponse = await client.GetWeatherInfoForZipcodeAsync(zipcode);

            //assert
            Assert.IsNotNull(apiResponse.Data);
            Assert.IsTrue(apiResponse.IsSuccess);
            StringAssert.AreEqualIgnoringCase("Portland",apiResponse.Data.City);
            Assert.IsNotNull(apiResponse.Data.TempKelvin);
        }

        //todo test when httpclient throws an exception
        //todo test when json returns and is not as expected (could be html content)
        //todo test for when API does not return a 200OK

        
        
    }


}
