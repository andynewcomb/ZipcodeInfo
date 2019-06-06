using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZipcodeInfo.DomainClasses
{
    public interface IValidator<T>
    {
        ValidationResponse Validate(T Model);
    }
}
