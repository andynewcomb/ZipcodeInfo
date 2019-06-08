using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ZipcodeInfo.Controllers;
using ZipcodeInfo.DomainClasses;
using System;
using System.Threading.Tasks;
using ZipcodeInfo.Processors;

namespace Tests.UnitTests
{
    public class ZipcodeInfoControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Get_ExceptionOccurs_HandleTheException()
        {
            //arrange
            var mockProcessor = new Mock<IZipcodeInfoProcessor>();
            mockProcessor.Setup( p => p.GenerateZipcodeInfoAsync(It.IsAny<Zipcode>())).ThrowsAsync(new Exception());
            var controller = new ZipcodeInfoController(mockProcessor.Object);
            IActionResult actionResult=null;


            //act and Assert
            Assert.DoesNotThrowAsync(async () => actionResult = await controller.Get(new Zipcode()));
            mockProcessor.Verify(p=>p.GenerateZipcodeInfoAsync(It.IsAny<Zipcode>()),Times.AtLeastOnce);
            Assert.AreEqual(typeof(StatusCodeResult), actionResult.GetType());
            Assert.AreEqual(500, ((StatusCodeResult)actionResult).StatusCode);            
        }

        //Todo test for logging when exception occurs.

        [Test]
        public async Task Get_InvalidZipcode_ReturnBadRequest()
        {
            var apiResponse = new ApiResponse<CayuseZipcodeInfo>
            {
                IsValidationError = true,
                ErrorMessage = "some validation error"
            };
            var mockProcessor = new Mock<IZipcodeInfoProcessor>();

            mockProcessor.Setup(p => p.GenerateZipcodeInfoAsync(It.IsAny<Zipcode>())).ReturnsAsync(apiResponse);
            var controller = new ZipcodeInfoController(mockProcessor.Object);
            


            //act and Assert
            var actionResult = await controller.Get(new Zipcode());
            Assert.AreEqual(typeof(BadRequestObjectResult), actionResult.GetType());
            Assert.AreEqual(400, ((BadRequestObjectResult)actionResult).StatusCode);
            StringAssert.AreEqualIgnoringCase("some validation error", 
                ((ApiResponse<CayuseZipcodeInfo>)((BadRequestObjectResult)actionResult).Value).ErrorMessage);
        }
        

    }
}