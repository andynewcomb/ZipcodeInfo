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
    class GoogleMapsApiClientTests
    {
        private HttpClient _googleHttpClient;
        [SetUp]
        public void SetUp()
        {
            _googleHttpClient = new HttpClient();
            _googleHttpClient.BaseAddress = new Uri("https://maps.googleapis.com/maps/api/");
            _googleHttpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        [Test]
        public async Task GetTimezoneAzync_ReturnsTimezone()
        {
            //arrange
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory.Setup(f => f.CreateClient("googleapis")).Returns(_googleHttpClient);
            var client = new GoogleMapsApiClient(mockHttpClientFactory.Object);
            var lon = -122.67d;
            var lat = 45.42d;
            var timeStamp = 1559931220d;

            //act
            ApiResponse<string> apiResponse = await client.GetTimeZoneAsync(lon, lat, timeStamp);

            //assert
            Assert.IsNotNull(apiResponse.Data);
            Assert.IsTrue(apiResponse.IsSuccess);
            StringAssert.AreEqualIgnoringCase("Pacific Daylight Time", apiResponse.Data);
        }

        //todo test when httpclient throws an exception
        //todo test when json returns and is not as expected (could be html content)
        //todo test for when API does not return a 200OK


        [Test]
        public async Task GetElevationAsync_ReturnsElevation()
        {
            //arrange
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory.Setup(f => f.CreateClient("googleapis")).Returns(_googleHttpClient);
            var client = new GoogleMapsApiClient(mockHttpClientFactory.Object);
            var lon = -122.67d;
            var lat = 45.42d;

            //act
            ApiResponse<double> apiResponse = await client.GetElevationAsync(lon, lat);

            //assert
            Assert.IsNotNull(apiResponse.Data);
            Assert.IsTrue(apiResponse.IsSuccess);
            Assert.AreEqual(52.14976119995117, apiResponse.Data);
        }
    }
}
