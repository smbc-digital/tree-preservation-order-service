using Moq;
using StockportGovUK.NetStandard.Gateways.VerintService;
using StockportGovUK.NetStandard.Models.Addresses;
using StockportGovUK.NetStandard.Models.Enums;
using System.Threading.Tasks;
using tree_preservation_order_service.Helpers;
using tree_preservation_order_service.Models;
using tree_preservation_order_service.Services;
using Xunit;

namespace tree_preservation_order_service_tests.Services
{
    public class TreePreservationOrderServiceTests
    {
        private readonly TreePreservationOrderService _treePreservationOrderService;
        private readonly Mock<IMailHelper> _mockMailHelper = new Mock<IMailHelper>();
        private readonly Mock<IVerintServiceGateway> _mockVerintServiceGateway = new Mock<IVerintServiceGateway>();

        public TreePreservationOrderServiceTests()
        {
           _treePreservationOrderService = new TreePreservationOrderService (_mockVerintServiceGateway.Object, _mockMailHelper.Object);
        }

        [Fact]
        public async Task CreateTreePreservationOrderCase_ShouldCallMailHelperToSendEmail()
        {
            var treePreservationorderRequest = new TreePreservationOrderRequest
            {
                StreetAddress = new Address 
                {
                    SelectedAddress = "green"
                },
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "test@gmail.com",
                Phone = "025642555"
            };

            _mockMailHelper.Setup(_ => _.SendEmail(It.IsAny<Person>(), EMailTemplate.TreePreservationOrderRequest, It.IsAny<string>(), treePreservationorderRequest.StreetAddress));

            await _treePreservationOrderService.CreateTreePreservationOrderCase(treePreservationorderRequest);

            _mockMailHelper.Verify(_ => _.SendEmail(It.IsAny<Person>(), EMailTemplate.TreePreservationOrderRequest, It.IsAny<string>(), treePreservationorderRequest.StreetAddress), Times.Once);
        }
    }
}
