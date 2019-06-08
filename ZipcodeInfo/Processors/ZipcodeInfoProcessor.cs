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
                apiResponse.Message = validationResponse.Message;
                apiResponse.IsValidationError = true;
                return apiResponse;
            }

            return apiResponse;
        }
    }
}
