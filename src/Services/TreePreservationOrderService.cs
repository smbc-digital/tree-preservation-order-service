using StockportGovUK.NetStandard.Gateways.VerintService;
using StockportGovUK.NetStandard.Models.Models.Verint.VerintOnlineForm;
using System;
using System.Threading.Tasks;

namespace tree_preservation_order_service.Services
{
    public class TreePreservationOrderService : ITreePreservationOrderService
    {
        private readonly IVerintServiceGateway _verintServiceGateway;

        public TreePreservationOrderService(IVerintServiceGateway verintServiceGateway)
        {
            _verintServiceGateway = verintServiceGateway;
        }

        public async Task<string> CreateVOFCase(VerintOnlineFormRequest model)
        {
            // model from json
            // verint case
            // data to go in verint online form request
            try
            {
                var response = await _verintServiceGateway.CreateVerintOnlineFormCase(model);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Status code not successful");
                }

                return response.ResponseContent.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception($"CRMService CreateVerintOnlineFormCase an exception has occured while creating the case in verint service", ex);
            }
        }
    }
}
