using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using ZipcodeInfo.ApiClients;
using ZipcodeInfo.DomainClasses;
using ZipcodeInfo.Processors;

namespace Tests.UnitTests
{
    class ZipcodeInfoProcessorTests
    {
        private Mock<IValidator<Zipcode>> _mockValidator;
        private Mock<IOpenWeatherApiClient> _mockOpenWeatherApiClient;
        private Mock<IGoogleMapsApiClient> _mockGoogleMapsApiClient;
        [SetUp]
        public void SetUp()
        {
            _mockValidator = new Mock<IValidator<Zipcode>>();
            _mockOpenWeatherApiClient = new Mock<IOpenWeatherApiClient>();
            _mockGoogleMapsApiClient = new Mock<IGoogleMapsApiClient>();
        }


        [Test]
        public async Task GenerateZipcodeInfoAsync_ValidatorReturnsFalse_ResultUnsuccessful()
        {
            var failedValidationResponse = new ValidationResponse
            {
                IsValid = false,
                Message = "Some explanation"
            };
            _mockValidator.Setup(v => v.Validate(It.IsAny<Zipcode>())).Returns(failedValidationResponse);

            var processor = new ZipcodeInfoProcessor(_mockValidator.Object, _mockGoogleMapsApiClient.Object,
                _mockOpenWeatherApiClient.Object);
            var apiResponse = await processor.GenerateZipcodeInfoAsync(new Zipcode());
            Assert.IsFalse(apiResponse.IsSuccess);
            StringAssert.AreEqualIgnoringCase("Some explanation",apiResponse.Message);
        }

    }
}
