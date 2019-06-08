using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using ZipcodeInfo.DomainClasses;

namespace ZipcodeInfo.ApiClients
{
    public class GoogleMapsApiClient : IGoogleMapsApiClient
    {
        private IHttpClientFactory _httpClientFactory;

        public GoogleMapsApiClient(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }


        public async Task<ApiResponse<string>> GetTimeZoneAsync(double lon, double lat, double timeStamp)
        {
            var response = new ApiResponse<string>();

            var httpClient = _httpClientFactory.CreateClient("googleapis");
            //todo pull url and information from appsettings.json file instead of hard coding
            var httpResponseMessage = await httpClient.GetAsync($"timezone/json?location={lat},{lon}&timestamp={timeStamp}&key=AIzaSyCAM2CIqiAFehe1ewwRs9URo5B6BIT9XdM");
            var httpContent = await httpResponseMessage.Content.ReadAsStringAsync();

            string timezone;
            ConvertJsonToTimeZone(httpContent,out timezone);

            response.Data = timezone;
            response.IsSuccess = true;
            return response;
        }

        

        public async Task<ApiResponse<double>> GetElevationAsync(double lon, double lat)
        {
            var response = new ApiResponse<double>();

            var httpClient = _httpClientFactory.CreateClient("googleapis");
            //todo pull url and information from appsettings.json file instead of hard coding
            var httpResponseMessage = await httpClient.GetAsync($"elevation/json?locations={lat},{lon}&key=AIzaSyCAM2CIqiAFehe1ewwRs9URo5B6BIT9XdM");
            var httpContent = await httpResponseMessage.Content.ReadAsStringAsync();


            double elevation;
            ConvertJsonToElevation(httpContent, out elevation);
            
            response.Data = elevation;
            response.IsSuccess = true;
            return response;
        }

        /// <summary>
        /// Converts json (returned from GoogleApis.com) into a Timezone string.
        /// </summary>
        /// <param name="json"></param>
        /// <param name="timezone"></param>
        public void ConvertJsonToTimeZone(string json, out string timezone)
        {
            JObject jsonObject = JObject.Parse(json);
            timezone = (string)jsonObject["timeZoneName"];
        }


        /// <summary>
        /// Converts json (returned from GoogleApis.com) into an elevation string.
        /// </summary>
        /// <param name="json"></param>
        /// <param name="elevation"></param>
        public void ConvertJsonToElevation(string json, out double elevation)
        {
            JObject jsonObject = JObject.Parse(json);
            elevation = (double)jsonObject["results"][0]["elevation"];
        }


    }
}
