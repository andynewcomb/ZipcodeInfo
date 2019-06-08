using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using ZipcodeInfo.DomainClasses;

namespace ZipcodeInfo.ApiClients
{
    public class GoogleMapsApiClient
    {
        private IHttpClientFactory _httpClientFactory;

        public GoogleMapsApiClient(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }


        public async Task<ApiResponse<string>> GetTimeZoneAsync(double lon, double lat, long timeStamp)
        {
            var response = new ApiResponse<string>();

            var httpClient = _httpClientFactory.CreateClient("googleapis");
            //todo pull url and information from appsettings.json file instead of hard coding
            httpClient.BaseAddress = new Uri("https://maps.googleapis.com/maps/api/");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            var httpResponseMessage = await httpClient.GetAsync($"timezone/json?location={lat},{lon}&timestamp={timeStamp}&key=AIzaSyCAM2CIqiAFehe1ewwRs9URo5B6BIT9XdM");
            var httpContent = await httpResponseMessage.Content.ReadAsStringAsync();


            string timezone;
            ConvertJsonToTimeZone(httpContent,out timezone);


            if (timezone == null)
            {
                response.IsSuccess = false;
                response.Message = "Failed to retrieve timezone information";
                return response;
            }

            response.Data = timezone;
            response.IsSuccess = true;
            return response;
        }

        /// <summary>
        /// Converts json (returned from OpenWeatherMap.org) into a WeatherInfo object.
        /// </summary>
        /// <param name="json"></param>
        /// <param name="weatherInfo"></param>
        public void ConvertJsonToTimeZone(string json, out string timezone)
        {
            JObject jsonObject = JObject.Parse(json);
            timezone = (string)jsonObject["timeZoneName"];
        }

    }
}
