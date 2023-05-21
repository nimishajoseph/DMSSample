using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinewood.DMSSample.Business.Services.Interfaces
{
    public interface IPartAvailabilityAdaptor : IDisposable
    {
        Task<int> GetAvailability(string stockCode);
    }
}
