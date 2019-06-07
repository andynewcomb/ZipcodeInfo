using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZipcodeInfo.DomainClasses
{
    public class Zipcode
    {

        public Zipcode()
        {}

        public Zipcode(string code)
        {
            Code = code;
        }

        
        public string Country { get; set; }
        public string Code { get; set; }
    }
}
