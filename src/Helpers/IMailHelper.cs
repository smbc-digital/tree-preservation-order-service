using StockportGovUK.NetStandard.Models.Enums;
using tree_preservation_order_service.Models;

namespace tree_preservation_order_service.Helpers
{
    public interface IMailHelper
    {
        void SendEmail(Person person, EMailTemplate template, string caseReference, StockportGovUK.NetStandard.Models.Addresses.Address street);
    }
}
