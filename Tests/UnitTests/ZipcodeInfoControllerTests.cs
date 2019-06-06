using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ZipcodeInfo.Controllers;
using ZipcodeInfo.DomainClasses;
using System;

namespace Tests.UnitTests
{
    public class ZipcodeInfoControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Get_ExceptionOccurs_HandleTheException()
        {
            //arrange
            var mockValidator = new Mock<IValidator<Zipcode>>();
            mockValidator.Setup(v => v.Validate(It.IsAny<Zipcode>())).Throws<Exception>();
            var controller = new ZipcodeInfoController(mockValidator.Object);
            IActionResult actionResult=null;

            

            //act and Assert
            Assert.DoesNotThrow(() => actionResult = controller.Get(new Zipcode()));
            Assert.AreEqual(typeof(StatusCodeResult), actionResult.GetType());
            Assert.AreEqual(500, ((StatusCodeResult)actionResult).StatusCode);            
        }

        //[TestCase("1234")]
        //[TestCase("134")]
        //[TestCase("2")]        
        //public void Get_ZipCodeLessThan5digits_Return400Response(string code)
        //{
        //    //arrange 
        //    var result = new List<ValidationResult>();
        //    var zipcode = new Zipcode() { Code = code };
        //    var controller = new ZipcodeInfoController();

        //    //act
        //    var zipcodeIsValid = Validator.TryValidateObject(zipcode, new ValidationContext(zipcode), result);
        //    var result = controller.Get(zipcode);


        //    //assert
        //    Assert.IsFalse(zipcodeIsValid);
        //    Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());



        //}


    }
}