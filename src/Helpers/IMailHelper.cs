using StockportGovUK.NetStandard.Gateways.Enums;
using tree_preservation_order_service.Models;
using Address = StockportGovUK.NetStandard.Gateways.Models.Addresses.Address;

namespace tree_preservation_order_service.Helpers
{
    public interface IMailHelper
    {
        void SendEmail(Person person, EMailTemplate template, string caseReference, Address street);
    }
}
