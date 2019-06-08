using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZipcodeInfo.DomainClasses;

namespace ZipcodeInfo.ApiClients
{
    public interface IGoogleMapsApiClient
    {
        Task<ApiResponse<string>> GetTimeZoneAsync(double lon, double lat, double timeStamp);
        Task<ApiResponse<double>> GetElevationAsync(double lon, double lat);
    }
}
