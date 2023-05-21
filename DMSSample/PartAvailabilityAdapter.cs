using Pinewood.DMSSample.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinewood.DMSSample.Business
{
    public class PartAvailabilityAdapter : IPartAvailabilityAdaptor
    {
        private readonly PartAvailabilityClient _partAvailabilityClient;

        public PartAvailabilityAdapter(PartAvailabilityClient partAvailabilityClient)
        {
            _partAvailabilityClient = partAvailabilityClient;
        }

        public async Task<int> GetAvailability(string stockCode)
        {
            return await _partAvailabilityClient.GetAvailability(stockCode);
        }

        public void Dispose()
        {
            _partAvailabilityClient.Dispose();
        }
    }
}
