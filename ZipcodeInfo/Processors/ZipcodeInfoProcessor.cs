using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZipcodeInfo.ApiClients;
using ZipcodeInfo.DomainClasses;

namespace ZipcodeInfo.Processors
{
    public class ZipcodeInfoProcessor: IZipcodeInfoProcessor
    {
        private IValidator<Zipcode> _zipcodeValidator;
        private IGoogleMapsApiClient _googleMapsApiClient;
        private IOpenWeatherApiClient _openWeatherApiClient;

        public ZipcodeInfoProcessor(IValidator<Zipcode> zipcodeValidator, IGoogleMapsApiClient googleMapsApiClient,
            IOpenWeatherApiClient openWeatherApiClient)
        {
            _zipcodeValidator = zipcodeValidator;
            _googleMapsApiClient = googleMapsApiClient;
            _openWeatherApiClient = openWeatherApiClient;

        }

        public async Task<ApiResponse<CayuseZipcodeInfo>> GenerateZipcodeInfoAsync(Zipcode zipcode)
        {
            var apiResponse = new ApiResponse<CayuseZipcodeInfo>();
            var validationResponse = _zipcodeValidator.Validate(zipcode);
            if (!validationResponse.IsValid)
            {
                apiResponse.IsSuccess = false;
                apiResponse.ErrorMessage = validationResponse.Message;
                apiResponse.IsValidationError = true;
                return apiResponse;
            }

            var weatherApiResponse = await _openWeatherApiClient.GetWeatherInfoForZipcodeAsync(zipcode);
            var latitude = weatherApiResponse.Data.Latitude;
            var longitude = weatherApiResponse.Data.Longitude;
            var timestamp = ConvertToUnixTimestamp(DateTime.UtcNow);

            var elevationApiResponse = await _googleMapsApiClient.GetElevationAsync(longitude, latitude);
            var timezoneApiResponse = await _googleMapsApiClient.GetTimeZoneAsync(longitude, latitude, timestamp);

            var cayuseZipcodeInfo = new CayuseZipcodeInfo()
            {
                CityName = weatherApiResponse.Data.City,
                CurrentTempFahrenheit = weatherApiResponse.Data.TempFahrenheit,
                TimezoneName = timezoneApiResponse.Data,
                Elevation = elevationApiResponse.Data
            };

            apiResponse.IsSuccess = true;
            apiResponse.Data = cayuseZipcodeInfo;

            return apiResponse;
        }

        

        private double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }

    }
}
