using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<MailHelper> _logger;
        public MailHelper(IMailingServiceGateway mailingServiceGateway,
                          ILogger<MailHelper> logger)
        {
            _mailingServiceGateway = mailingServiceGateway;
            _logger = logger;
        }

        public void SendEmail(Person person, EMailTemplate template, string caseReference, StockportGovUK.NetStandard.Models.Addresses.Address street)
        {
            TreePreservationOrderMailModel submissionDetails = new TreePreservationOrderMailModel();
            _logger.LogInformation(caseReference, street, person);
            submissionDetails.Subject = $"Tree Preservation Order Request - submission";
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
