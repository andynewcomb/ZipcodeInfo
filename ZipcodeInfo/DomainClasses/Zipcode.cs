using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZipcodeInfo.DomainClasses
{
    public class Zipcode
    {
        [Required]
        public string Country { get; set; }

        [MinLength(5)]        
        public string Code { get; set; }
    }
}
