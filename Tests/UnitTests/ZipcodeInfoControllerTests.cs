using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ZipcodeInfo.Controllers;
using ZipcodeInfo.DomainClasses;


namespace Tests.UnitTests
{
    public class ZipcodeInfoControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        //[Test]
        //public void Get_ExceptionOccurs_HandleTheException()
        //{
        //    //arrange


        //    Assert.Pass();
        //}

        [TestCase("1234")]
        [TestCase("134")]
        [TestCase("2")]        
        public void Get_ZipCodeLessThan5digits_Return400Response(string code)
        {
            //arrange 
            var result = new List<ValidationResult>();
            var zipcode = new Zipcode() { Code = code };
            var controller = new ZipcodeInfoController();

            //act
            var zipcodeIsValid = Validator.TryValidateObject(zipcode, new ValidationContext(zipcode), result);
            var result = controller.Get(zipcode);


            //assert
            Assert.IsFalse(zipcodeIsValid);
            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());


            
        }


    }
}