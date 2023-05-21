using Pinewood.DMSSample.Business.Services.Interfaces;

namespace Pinewood.DMSSample.Business
{
    public class DMSClient
    {
        private PartInvoiceController __Controller;
        public DMSClient(ICustomerRepository customerRepository,
            IPartAvailabilityAdaptor partAvailabilityAdaptor,
            IPartInvoiceRepository partInvoiceRepository)
        {
            __Controller = new PartInvoiceController(customerRepository, partAvailabilityAdaptor, partInvoiceRepository);
        }

        public async Task<CreatePartInvoiceResult> CreatePartInvoiceAsync(string stockCode, int quantity, string customerName)
        {
            return await __Controller.CreatePartInvoiceAsync(stockCode, quantity, customerName);
        }
    }
}