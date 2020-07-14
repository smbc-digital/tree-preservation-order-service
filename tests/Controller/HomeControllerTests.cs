using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using tree_preservation_order_service.Controllers;
using tree_preservation_order_service.Models;
using tree_preservation_order_service.Services;
using Xunit;

namespace tree_preservation_order_service_tests.Controllers
{
    public class HomeControllerTests
    {
        private readonly HomeController _homeController;
        private readonly Mock<ITreePreservationOrderService> _mockTreePreservationOrderService = new Mock<ITreePreservationOrderService>();

        public HomeControllerTests()
        {
            _homeController = new HomeController(Mock.Of<ILogger<HomeController>>(), _mockTreePreservationOrderService.Object);
        }

        [Fact]
        public async Task Post_ShouldCallCreateCase()
        {
            _mockTreePreservationOrderService
                .Setup(_ => _.CreateTreePreservationOrderCase(It.IsAny<TreePreservationOrderRequest>()))
                .ReturnsAsync("test");

            var result = await _homeController.Post(null);

            _mockTreePreservationOrderService
                .Verify(_ => _.CreateTreePreservationOrderCase(null), Times.Once);
        }

        [Fact]
        public async Task Post_ReturnOkActionResult()
        {
            _mockTreePreservationOrderService
                .Setup(_ => _.CreateTreePreservationOrderCase(It.IsAny<TreePreservationOrderRequest>()))
                .ReturnsAsync("test");

            var result = await _homeController.Post(null);

            Assert.Equal("OkObjectResult", result.GetType().Name);
        }
    }
}
