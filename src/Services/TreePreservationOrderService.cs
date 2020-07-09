using StockportGovUK.NetStandard.Gateways.VerintService;
using StockportGovUK.NetStandard.Models.Verint;
using System;
using System.Threading.Tasks;
using tree_preservation_order_service.Models;

namespace tree_preservation_order_service.Services
{
    public class TreePreservationOrderService : ITreePreservationOrderService
    {
        private readonly IVerintServiceGateway _verintServiceGateway;

        public TreePreservationOrderService(IVerintServiceGateway verintServiceGateway)
        {
            _verintServiceGateway = verintServiceGateway;
        }

        public async Task<string> CreateCase(TreePreservationOrder treePreservationOrder)
        {
            var crmCase = new Case();
            try
            {
                var response = await _verintServiceGateway.CreateCase(crmCase);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Status code not successful");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"CRMService CreateAbandonedVehicleService an exception has occured while creating the case in verint service", ex);
            }

            return "123456";
        }
    }
}
