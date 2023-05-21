// See https://aka.ms/new-console-template for more information
using Pinewood.DMSSample.Business;
using Pinewood.DMSSample.Business.Services.Interfaces;

try
{
    ICustomerRepository customerRepository = new CustomerRepositoryDB();
    PartAvailabilityClient partAvailabilityClient = new PartAvailabilityClient();

    IPartAvailabilityAdaptor partAvailabilityAdapter = new PartAvailabilityAdapter(partAvailabilityClient);
    IPartInvoiceRepository partInvoiceRepository = new PartInvoiceRepositoryDB();


    DMSClient dmsClient = new DMSClient(customerRepository, partAvailabilityAdapter, partInvoiceRepository);

    await dmsClient.CreatePartInvoiceAsync("1234", 10, "John Doe");
}
catch (Exception ex)
{
    Console.WriteLine("An error occured: " + ex.Message);
}
