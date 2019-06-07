using NUnit.Framework;
using Moq;
using ZipcodeInfo.DomainClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.UnitTests
{
    class ZipcodeValidatorTests
    {
        [Test]
        public void Validate_NullCode_ReturnFalse()
        {
            //arrange
            var zipcode = new Zipcode()
            {
                Code = null,
            };
            var zipcodeValidator = new ZipcodeValidator();

            //act
            var validationResponse = zipcodeValidator.Validate(zipcode);

            //assert
            Assert.IsFalse(validationResponse.IsValid);
            Assert.IsNotNull(validationResponse.Message);
        }


        [TestCase("")]
        [TestCase(" ")]
        [TestCase("     ")]
        public void Validate_EmptyCode_ReturnFalse(string code)
        {
            //arrange
            var zipcode = new Zipcode(code);
            var zipcodeValidator = new ZipcodeValidator();

            //act
            var validationResponse = zipcodeValidator.Validate(zipcode);

            //assert
            Assert.IsFalse(validationResponse.IsValid);
            Assert.IsNotNull(validationResponse.Message);
        }


        [TestCase("123456")]
        [TestCase("1234")]
        [TestCase("1")]
        [TestCase("1234567890")]
        public void Validate_OtherThanFiveCharactersInCode_ReturnFalse(string code)
        {
            //arrange
            var zipcode = new Zipcode(code);
            var zipcodeValidator = new ZipcodeValidator();

            //act
            var validationResponse = zipcodeValidator.Validate(zipcode);

            //assert
            Assert.IsFalse(validationResponse.IsValid);
            Assert.IsNotNull(validationResponse.Message);
        }

        [TestCase("1234A")]
        [TestCase("ABCDE")]
        [TestCase("&*123")]
        [TestCase("12-36")]
        public void Validate_CodeContainsNonDigits_ReturnFalse(string code)
        {
            //arrange
            var zipcode = new Zipcode(code);
            var zipcodeValidator = new ZipcodeValidator();

            //act
            var validationResponse = zipcodeValidator.Validate(zipcode);

            //assert
            Assert.IsFalse(validationResponse.IsValid);
            Assert.IsNotNull(validationResponse.Message);
        }

        [TestCase("12345")]
        [TestCase("12398")]
        [TestCase("98765")]
        public void Validate_CodeContains5Digits_ReturnTrue(string code)
        {
            //arrange
            var zipcode = new Zipcode(code);
            var zipcodeValidator = new ZipcodeValidator();

            //act
            var validationResponse = zipcodeValidator.Validate(zipcode);

            //assert
            Assert.IsTrue(validationResponse.IsValid);
            Assert.IsNull(validationResponse.Message);
        }

    }
}
