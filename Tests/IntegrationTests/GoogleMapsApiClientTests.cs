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
        [Test]
        public async Task GetTimezone_ReturnsTimezone()
        {
            //arrange
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory.Setup(f => f.CreateClient("googleapis")).Returns(new HttpClient());
            var client = new GoogleMapsApiClient(mockHttpClientFactory.Object);
            var lon = -122.67d;
            var lat = 45.42d;
            var timeStamp = 1559931220;

            //act
            ApiResponse<string> apiResponse = await client.GetTimeZoneAsync(lon, lat, timeStamp);

            //assert
            Assert.IsNotNull(apiResponse.Data);
            Assert.IsTrue(apiResponse.IsSuccess);
            StringAssert.AreEqualIgnoringCase("Pacific Daylight Time", apiResponse.Data.ToString());
        }

        //todo test when httpclient throws an exception
        //todo test when json returns and is not as expected (could be html content)
        //todo test for when API does not return a 200OK
    }
}
