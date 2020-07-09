using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockportGovUK.AspNetCore.Attributes.TokenAuthentication;
using System.Threading.Tasks;
using tree_preservation_order_service.Models;
using tree_preservation_order_service.Services;

namespace tree_preservation_order_service.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[Controller]")]
    [ApiController]
    [TokenAuthentication]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITreePreservationOrderService _treePreservationOrderService;

        public HomeController(ILogger<HomeController> logger,
                              ITreePreservationOrderService treePreservationOrderService)
        {
            _logger = logger;
            _treePreservationOrderService = treePreservationOrderService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TreePreservationOrder treePreservationOrder)
            => Ok(await _treePreservationOrderService.CreateCase(treePreservationOrder));
    }
}