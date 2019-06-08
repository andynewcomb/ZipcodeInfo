using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZipcodeInfo.DomainClasses;


namespace ZipcodeInfo.ApiClients
{
    /// <summary>
    /// Client for accessing the API provided by OpenWeatherMap.org
    /// </summary>
    public class OpenWeatherApiClient
    {
        private IHttpClientFactory _httpClientFactory;
        public OpenWeatherApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ApiResponse<WeatherInfo>> GetWeatherInfoForZipcodeAsync(Zipcode zipcode)
        {
            var response = new ApiResponse<WeatherInfo>();

            var httpClient = _httpClientFactory.CreateClient("openweather");
            //todo pull url and information from appsettings.json file instead of hard coding
            httpClient.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            var httpResponseMessage = await httpClient.GetAsync("weather?zip=97219&APPID=2565827e7fc0a0e5ee94b37c0a8be672");
            var httpContent = await httpResponseMessage.Content.ReadAsStringAsync();

            
            WeatherInfo weatherInfo;
            ConvertJsonToWeatherInfo(httpContent, out weatherInfo);

            
            if (weatherInfo == null)
            {
                response.IsSuccess = false;
                response.Message = "Failed to retrieve weather information";
                return response;
            }



            response.Data = weatherInfo;
            response.IsSuccess = true;
            return response;
        }

        /// <summary>
        /// Converts json (returned from OpenWeatherMap.org) into a WeatherInfo object.
        /// </summary>
        /// <param name="json"></param>
        /// <param name="weatherInfo"></param>
        public void ConvertJsonToWeatherInfo(string json, out WeatherInfo weatherInfo)
        {
            weatherInfo = new WeatherInfo();
            JObject jsonObject = JObject.Parse(json);
            weatherInfo.City = (string)jsonObject["name"];
            weatherInfo.TempKelvin = (double)jsonObject["main"]["temp"];
            weatherInfo.Latitude = (double) jsonObject["coord"]["lat"];
            weatherInfo.Longitude = (double)jsonObject["coord"]["lon"];
        }

    }
}
