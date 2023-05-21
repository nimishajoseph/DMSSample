using Pinewood.DMSSample.Business.Services.Interfaces;

namespace Pinewood.DMSSample.Business
{
    public class PartInvoiceController
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IPartAvailabilityAdaptor _partAvailabilityClient;
        private readonly IPartInvoiceRepository _partInvoiceRepository;

        public PartInvoiceController(ICustomerRepository customerRepository, IPartAvailabilityAdaptor partAvailabilityClient, IPartInvoiceRepository partInvoiceRepository)
        {
            _customerRepository = customerRepository;
            _partAvailabilityClient = partAvailabilityClient;
            _partInvoiceRepository = partInvoiceRepository;
        }

        public async Task<CreatePartInvoiceResult> CreatePartInvoiceAsync(string stockCode, int quantity, string customerName)
        {
            if (string.IsNullOrEmpty(stockCode) || (quantity <= 0))
            {
                return new CreatePartInvoiceResult(false);
            }

            Customer? customer = _customerRepository.GetByName(customerName);

            if (customer == null)
            {
                return new CreatePartInvoiceResult(false);
            }

            using (_partAvailabilityClient)
            {
                int _Availability = await _partAvailabilityClient.GetAvailability(stockCode);
                if (_Availability <= 0)
                {
                    return new CreatePartInvoiceResult(false);
                }
            }

            PartInvoice _PartInvoice = new PartInvoice(
               stockCode,
                quantity,
              customer.ID
            );


            _partInvoiceRepository.Add(_PartInvoice);

            return new CreatePartInvoiceResult(true);
        }
    }
}
