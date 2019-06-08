using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZipcodeInfo.DomainClasses
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public bool IsValidationError { get; set; }
    }
}
