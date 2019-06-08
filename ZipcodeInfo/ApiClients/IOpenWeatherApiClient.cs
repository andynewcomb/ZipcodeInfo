using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZipcodeInfo.DomainClasses;

namespace ZipcodeInfo.ApiClients
{
    public interface IOpenWeatherApiClient
    {
        Task<ApiResponse<WeatherInfo>> GetWeatherInfoForZipcodeAsync(Zipcode zipcode);
    }
}
