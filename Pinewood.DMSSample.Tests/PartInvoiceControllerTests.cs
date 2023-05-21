using Moq;
using Pinewood.DMSSample.Business;
using Pinewood.DMSSample.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Pinewood.DMSSample.Tests
{
    [TestFixture]
    public class PartInvoiceControllerTests
    {
        private PartInvoiceController _controller;
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IPartAvailabilityAdaptor> _partAvailabilityAdaptorMock;
        private Mock<IPartInvoiceRepository> _partInvoiceRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _partAvailabilityAdaptorMock = new Mock<IPartAvailabilityAdaptor>();
            _partInvoiceRepositoryMock = new Mock<IPartInvoiceRepository>();

            _controller = new PartInvoiceController(
                _customerRepositoryMock.Object,
                _partAvailabilityAdaptorMock.Object,
                _partInvoiceRepositoryMock.Object
            );
        }


        [Test]
        public async Task CreatePartInvoiceAsync_ValidInput_ReturnsSuccessResult()
        {
            // Arrange
            string stockCode = "ABC123";
            int quantity = 5;
            string customerName = "John Doe";

            var customer = new Customer(1, customerName, "Address");

            _customerRepositoryMock.Setup(repo => repo.GetByName(customerName))
                .Returns(customer);

            _partAvailabilityAdaptorMock.Setup(adaptor => adaptor.GetAvailability(stockCode))
                .ReturnsAsync(10);

            // Act
            var result = await _controller.CreatePartInvoiceAsync(stockCode, quantity, customerName);
        
            // Assert
            Assert.IsTrue(result.Success);

            _partInvoiceRepositoryMock.Verify(repo => repo.Add(It.IsAny<PartInvoice>()), Times.Once);
        }

        [Test]
        public async Task CreatePartInvoiceAsync_InvalidStockCode_ReturnsFailureResult()
        {
            // Arrange
            string stockCode = "";
            int quantity = 5;
            string customerName = "John Doe";

            // Act
            var result = await _controller.CreatePartInvoiceAsync(stockCode, quantity, customerName);

            // Assert
            Assert.IsFalse(result.Success);
            _customerRepositoryMock.Verify(repo => repo.GetByName(It.IsAny<string>()), Times.Never);
            _partAvailabilityAdaptorMock.Verify(adaptor => adaptor.GetAvailability(It.IsAny<string>()), Times.Never);
            _partInvoiceRepositoryMock.Verify(repo => repo.Add(It.IsAny<PartInvoice>()), Times.Never);
        }

        [Test]
        public async Task CreatePartInvoiceAsync_InvalidQuantity_ReturnsFailureResult()
        {
            // Arrange
            string stockCode = "ABC123";
            int quantity = 0;
            string customerName = "John Doe";

            // Act
            var result = await _controller.CreatePartInvoiceAsync(stockCode, quantity, customerName);

            // Assert
            Assert.IsFalse(result.Success);
            _customerRepositoryMock.Verify(repo => repo.GetByName(It.IsAny<string>()), Times.Never);
            _partAvailabilityAdaptorMock.Verify(adaptor => adaptor.GetAvailability(It.IsAny<string>()), Times.Never);
            _partInvoiceRepositoryMock.Verify(repo => repo.Add(It.IsAny<PartInvoice>()), Times.Never);
        }

        [Test]
        public async Task CreatePartInvoiceAsync_CustomerNotFound_ReturnsFailureResult()
        {
            // Arrange
            string stockCode = "ABC123";
            int quantity = 5;
            string customerName = "John Doe";

            _customerRepositoryMock.Setup(repo => repo.GetByName(customerName))
                .Returns((Customer?)null);

            // Act
            var result = await _controller.CreatePartInvoiceAsync(stockCode, quantity, customerName);

            // Assert
            Assert.IsFalse(result.Success);
            _partAvailabilityAdaptorMock.Verify(adaptor => adaptor.GetAvailability(It.IsAny<string>()), Times.Never);
            _partInvoiceRepositoryMock.Verify(repo => repo.Add(It.IsAny<PartInvoice>()), Times.Never);
        }

        [Test]
        public async Task CreatePartInvoiceAsync_PartAvailabilityZero_ReturnsFailureResult()
        {
            // Arrange
            string stockCode = "ABC123";
            int quantity = 5;
            string customerName = "John Doe";

            var customer = new Customer(1, customerName, "Address");

            _customerRepositoryMock.Setup(repo => repo.GetByName(customerName))
                .Returns(customer);

            _partAvailabilityAdaptorMock.Setup(adaptor => adaptor.GetAvailability(stockCode))
                .ReturnsAsync(0);

            // Act
            var result = await _controller.CreatePartInvoiceAsync(stockCode, quantity, customerName);

            // Assert
            Assert.IsFalse(result.Success);
            _partInvoiceRepositoryMock.Verify(repo => repo.Add(It.IsAny<PartInvoice>()), Times.Never);
        }


    }
}
