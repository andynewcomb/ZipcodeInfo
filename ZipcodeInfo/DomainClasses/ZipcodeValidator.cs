using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Routing;
using ZipcodeInfo.DomainClasses;

namespace ZipcodeInfo.DomainClasses
{
    public class ZipcodeValidator : IValidator<Zipcode>
    {
        public ValidationResponse Validate(Zipcode model)
        {
            var validationResponse=new ValidationResponse();
            if (String.IsNullOrWhiteSpace(model.Code))
            {
                validationResponse.MakeInvalid("zipcode was not supplied");
                return validationResponse;
            }




            string pattern = @"^\d{5}$";
            var isMatch = Regex.IsMatch(model.Code ?? "0", pattern);
            if (!isMatch)
            {
                validationResponse.MakeInvalid($"Zipcode supplied ({model.Code}) must be exactly 5 digits");
                return validationResponse;
            }

            validationResponse.IsValid = true;
            return validationResponse;

        }
    }
}
