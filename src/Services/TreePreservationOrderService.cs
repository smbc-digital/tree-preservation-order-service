using Microsoft.Extensions.Configuration;
using StockportGovUK.NetStandard.Gateways.VerintService;
using StockportGovUK.NetStandard.Models.Enums;
using StockportGovUK.NetStandard.Models.Models.Verint.VerintOnlineForm;
using StockportGovUK.NetStandard.Models.Verint;
using System;
using System.Text;
using System.Threading.Tasks;
using tree_preservation_order_service.Helpers;
using tree_preservation_order_service.Models;

namespace tree_preservation_order_service.Services
{
    public class TreePreservationOrderService : ITreePreservationOrderService
    {
        private readonly IVerintServiceGateway _verintServiceGateway;
        private readonly IMailHelper _mailHelper;

        public TreePreservationOrderService(IVerintServiceGateway verintServiceGateway, IMailHelper mailHelper)
        {
            _verintServiceGateway = verintServiceGateway;
            _mailHelper = mailHelper;
        }

        public async Task<string> CreateTreePreservationOrderCase(TreePreservationOrderRequest treePreservationOrderRequest)
        {
            //var verintOnlineFormRequest = new VerintOnlineFormRequest();

            //var response = await _verintServiceGateway.CreateVerintOnlineFormCase(verintOnlineFormRequest);

            try
            {
                Person person = new Person
                {
                    FirstName = treePreservationOrderRequest.FirstName,
                    LastName = treePreservationOrderRequest.LastName,
                    Email = treePreservationOrderRequest.Email,
                    Phone = treePreservationOrderRequest.Phone,
                };


                _mailHelper.SendEmail(person, EMailTemplate.TreePreservationOrderRequest, "123456", treePreservationOrderRequest.StreetAddress);
                return "123456";
            }
            catch (Exception ex)
            {
                throw new Exception($"CRMService CreateCase an exception has occured while creating the case in verint service", ex);
            }




        }
    }
}


