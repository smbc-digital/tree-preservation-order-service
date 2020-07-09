using tree_preservation_order_service.Controllers;
using Moq;
using Xunit;
using tree_preservation_order_service.Services;
using Microsoft.Extensions.Logging;
using tree_preservation_order_service.Models;
using System.Threading.Tasks;

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
                .Setup(_ => _.CreateCase(It.IsAny<TreePreservationOrder>()))
                .ReturnsAsync("test");

            var result = await _homeController.Post(null);

            _mockTreePreservationOrderService
                .Verify(_ => _.CreateCase(null), Times.Once);
        }

        [Fact]
        public async Task Post_ReturnOkActionResult()
        {
            _mockTreePreservationOrderService
                .Setup(_ => _.CreateCase(It.IsAny<TreePreservationOrder>()))
                .ReturnsAsync("test");

            var result = await _homeController.Post(null);

            Assert.Equal("OkObjectResult", result.GetType().Name);
        }
    }
}
