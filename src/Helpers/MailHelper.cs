using Newtonsoft.Json;
using StockportGovUK.NetStandard.Gateways.MailingService;
using StockportGovUK.NetStandard.Models.Enums;
using StockportGovUK.NetStandard.Models.Mail;
using StockportGovUK.NetStandard.Models.Models.TreePreservationOrder;
using tree_preservation_order_service.Models;

namespace tree_preservation_order_service.Helpers
{
    public class MailHelper : IMailHelper
    {
        private readonly IMailingServiceGateway _mailingServiceGateway;
        public MailHelper(IMailingServiceGateway mailingServiceGateway)
        {
            _mailingServiceGateway = mailingServiceGateway;
        }

        public void SendEmail(Person person, EMailTemplate template, string caseReference, StockportGovUK.NetStandard.Models.Addresses.Address street)
        {
            var submissionDetails = new TreePreservationOrderMailModel();
            submissionDetails.Subject = $"Request a Tree Preservation Order : {caseReference}";
            submissionDetails.Reference = caseReference;
            submissionDetails.StreetInput = street.SelectedAddress;
            submissionDetails.FirstName = person.FirstName;
            submissionDetails.LastName = person.LastName;
            submissionDetails.RecipientAddress = person.Email;

            _mailingServiceGateway.Send(new Mail
            {
                Payload = JsonConvert.SerializeObject(submissionDetails),
                Template = template
            });
        }
    }
}
