using System;
using System.Text;
using tree_preservation_order_service.Models;
using StockportGovUK.NetStandard.Extensions.VerintExtensions.VerintOnlineFormsExtensions.ConfirmIntegrationFromExtensions;
using StockportGovUK.NetStandard.Gateways.Models.Verint;

namespace tree_preservation_order_service.Mappers
{
    public static class TreePreservationOrderRequestMapper
    {
        public static Case ToCase(this TreePreservationOrderRequest model,
            ConfirmIntegrationFormOptions _VOFConfiguration,
            VerintOptions _verintOptions)
        {
            var crmCase = new Case
            {
                EventCode = _VOFConfiguration.EventId,
                EventTitle = _verintOptions.EventTitle,
                Classification = _verintOptions.Classification,
                FurtherLocationInformation = model.MoreDetails,
                RaisedByBehaviour = RaisedByBehaviourEnum.Individual,
                Description = GenerateDescription(model),
                Customer = new Customer
                {
                    Forename = model.FirstName,
                    Surname = model.LastName,
                    Email = model.Email,
                    Telephone = model.Phone,
                    Address = new Address
                    {
                        AddressLine1 = model.CustomersAddress.AddressLine1,
                        AddressLine2 = model.CustomersAddress.AddressLine2,
                        City = model.CustomersAddress.Town,
                        Postcode = model.CustomersAddress.Postcode,
                        UPRN = model.CustomersAddress.PlaceRef,
                        Description = model.CustomersAddress.ToString()
                    }
                }
            };

            if (!string.IsNullOrWhiteSpace(model.StreetAddress?.PlaceRef))
            {
                crmCase.AssociatedWithBehaviour = AssociatedWithBehaviourEnum.Street;
                crmCase.RaisedByBehaviour = RaisedByBehaviourEnum.Individual;
                crmCase.Street = new Street
                {
                    Reference = model.StreetAddress.PlaceRef,
                    Description = model.StreetAddress.ToString()
                };
            }

            return crmCase;
        }

        private static string GenerateDescription(TreePreservationOrderRequest treePreservationOrderRequest)
        {
            StringBuilder description = new StringBuilder();

            if (!string.IsNullOrEmpty(treePreservationOrderRequest.ReasonForRequest))
                description.Append($"Reason for request: {treePreservationOrderRequest.ReasonForRequest}{Environment.NewLine}");

            return description.ToString();
        }
    }
}
