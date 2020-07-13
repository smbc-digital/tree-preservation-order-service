using Microsoft.Extensions.Configuration;
using StockportGovUK.NetStandard.Gateways.VerintService;
using StockportGovUK.NetStandard.Models.Verint;
using System;
using System.Text;
using System.Threading.Tasks;
using tree_preservation_order_service.Models;

namespace tree_preservation_order_service.Services
{
    public class TreePreservationOrderService : ITreePreservationOrderService
    {
        private readonly IConfiguration _configuration;
        private readonly IVerintServiceGateway _verintServiceGateway;

        public TreePreservationOrderService(IVerintServiceGateway verintServiceGateway)
        {
            _verintServiceGateway = verintServiceGateway;
        }

        public async Task<string> CreateTreePreservationOrderCase(TreePreservationOrderRequest treePreservationOrderRequest)
        {
            var crmCase = new Case();

            var response = await _verintServiceGateway.CreateCase(crmCase);

            return "123456";
            //Case crmCase = CreateCrmCaseObject(treePreservationOrderRequest);

            //try
            //{
            //    Person person = new Person
            //    {
            //        FirstName = treePreservationOrderRequest.FirstName,
            //        LastName = treePreservationOrderRequest.LastName,
            //        Email = treePreservationOrderRequest.Email,
            //        Phone = treePreservationOrderRequest.Phone,
            //    };

            //    var randomReference = "123456";
            //    // _mailHelper.SendEmail(person, EMailTemplate.AbandonedVehicleReport, randomReference.ToString(), treePreservationOrderRequest.StreetAddress);
            //    return randomReference;
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception($"CRMService CreateCase an exception has occured while creating the case in verint service", ex);
            //}
        }

        //    private Case CreateCrmCaseObject(TreePreservationOrderRequest treePreservationOrderRequest)
        //    {
        //        Case crmCase = new Case
        //        {
        //            EventCode = Int32.Parse(_configuration.GetSection("CrmCaseSettings").GetSection("EventCode").Value),
        //            EventTitle = _configuration.GetSection("CrmCaseSettings").GetSection("EventTitle").Value,
        //            Classification = _configuration.GetSection("CrmCaseSettings").GetSection("Classification").Value,
        //            Description = GenerateDescription(treePreservationOrderRequest),
        //            Street = new Street
        //            {
        //                Reference = treePreservationOrderRequest.StreetAddress?.PlaceRef
        //            }
        //        };

        //        if (!string.IsNullOrEmpty(treePreservationOrderRequest.FirstName) && !string.IsNullOrEmpty(treePreservationOrderRequest.LastName))
        //        {
        //            crmCase.Customer = new Customer
        //            {
        //                Forename = treePreservationOrderRequest.FirstName,
        //                Surname = treePreservationOrderRequest.LastName
        //            };

        //            if (!string.IsNullOrEmpty(treePreservationOrderRequest.Email))
        //            {
        //                crmCase.Customer.Email = treePreservationOrderRequest.Email;
        //            }

        //            if (!string.IsNullOrEmpty(treePreservationOrderRequest.Phone))
        //            {
        //                crmCase.Customer.Telephone = treePreservationOrderRequest.Phone;
        //            }

        //            if (string.IsNullOrEmpty(treePreservationOrderRequest.CustomersAddress.PlaceRef))
        //            {
        //                crmCase.Customer.Address = new Address
        //                {
        //                    AddressLine1 = treePreservationOrderRequest.CustomersAddress.AddressLine1,
        //                    AddressLine2 = treePreservationOrderRequest.CustomersAddress.AddressLine2,
        //                    AddressLine3 = treePreservationOrderRequest.CustomersAddress.Town,
        //                    Postcode = treePreservationOrderRequest.CustomersAddress.Postcode,
        //                };
        //            }
        //            else
        //            {
        //                crmCase.Customer.Address = new Address
        //                {
        //                    Reference = treePreservationOrderRequest.CustomersAddress.PlaceRef,
        //                    UPRN = treePreservationOrderRequest.CustomersAddress.PlaceRef
        //                };
        //            }
        //        }

        //        return crmCase;
        //    }

        //    private string GenerateDescription(TreePreservationOrderRequest treePreservationOrderRequest)
        //    {
        //        StringBuilder description = new StringBuilder();

        //        if (!string.IsNullOrEmpty(treePreservationOrderRequest.MoreDetails))
        //        {
        //            description.Append($"More details: {treePreservationOrderRequest.MoreDetails}");
        //            description.Append(Environment.NewLine);
        //        }

        //        if (!string.IsNullOrEmpty(treePreservationOrderRequest.ReasonForRequest))
        //        {
        //            description.Append($"Abandoned Reason: {treePreservationOrderRequest.ReasonForRequest}");
        //            description.Append(Environment.NewLine);
        //        }

        //        return description.ToString();

        //    }
        //}
    }
}


