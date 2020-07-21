using System;
using System.Threading.Tasks;
using StockportGovUK.NetStandard.Extensions.VerintExtensions.VerintOnlineFormsExtensions.ConfirmIntegrationFromExtensions;
using StockportGovUK.NetStandard.Gateways.VerintService;
using StockportGovUK.NetStandard.Models.Enums;
using Microsoft.Extensions.Options;
using tree_preservation_order_service.Helpers;
using tree_preservation_order_service.Mappers;
using tree_preservation_order_service.Models;

namespace tree_preservation_order_service.Services
{
    public class TreePreservationOrderService : ITreePreservationOrderService
    {
        private readonly IVerintServiceGateway _verintServiceGateway;
        private readonly IMailHelper _mailHelper;
        private readonly VerintOptions _verintOptions;
        private readonly ConfirmIntegrationFormOptions _VOFConfiguaration;

        public TreePreservationOrderService(IVerintServiceGateway verintServiceGateway, 
                                            IMailHelper mailHelper,
                                            IOptions<VerintOptions> VerintOptions,
                                            IOptions<ConfirmIntegrationFormOptions> VOFConfiguaration)
        {
            _verintServiceGateway = verintServiceGateway;
            _mailHelper = mailHelper;
            _verintOptions = VerintOptions.Value;
            _VOFConfiguaration = VOFConfiguaration.Value;
        }

        public async Task<string> CreateCase(TreePreservationOrderRequest treePreservationOrderRequest)
        {
            var crmCase = treePreservationOrderRequest
                .ToCase(_VOFConfiguaration, _verintOptions);

            var streetResult = await _verintServiceGateway.GetStreet(treePreservationOrderRequest.StreetAddress.PlaceRef);

            if(!streetResult.IsSuccessStatusCode)
                throw new Exception("TreePreservationOrderService.CreateCase: GetStreet status code not successful");

            // confirm uses the USRN for streets,
            // but verint uses verint-address-id (Reference) (abandonedVehicleReport.StreetAddress.PlaceRef)
            crmCase.Street.USRN = streetResult.ResponseContent.USRN;

            try
            {
                var response = await _verintServiceGateway.CreateVerintOnlineFormCase(crmCase.ToConfirmIntegrationFormCase(_VOFConfiguaration));
                if (!response.IsSuccessStatusCode)
                    throw new Exception("TreePreservationOrderServoce.CreateCase: CreateVerintOnlineFormCase status code not successful");

                var person = new Person
                {
                    FirstName = treePreservationOrderRequest.FirstName,
                    LastName = treePreservationOrderRequest.LastName,
                    Email = treePreservationOrderRequest.Email,
                    Phone = treePreservationOrderRequest.Phone
                };

                _mailHelper.SendEmail(
                    person,
                    EMailTemplate.TreePreservationOrderRequest,
                    response.ResponseContent.VerintCaseReference,
                    treePreservationOrderRequest.StreetAddress);

                return response.ResponseContent.VerintCaseReference;
            }
            catch (Exception ex)
            {
                throw new Exception($"CreateTreePreservationOrderCase thrown exception when sending the email", ex);
            }
        }
    }
}



