using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZipcodeInfo.DomainClasses;

namespace ZipcodeInfo.Processors
{
    public interface IZipcodeInfoProcessor
    {
        Task<ApiResponse<CayuseZipcodeInfo>> GenerateZipcodeInfoAsync(Zipcode zipcode);
    }
}
