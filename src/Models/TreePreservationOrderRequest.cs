using StockportGovUK.NetStandard.Gateways.Models.Addresses;

namespace tree_preservation_order_service.Models
{
    public class TreePreservationOrderRequest
    {
        public Address StreetAddress { get; set; }
        public string MoreDetails { get; set; }
        public string ReasonForRequest { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Address CustomersAddress { get; set; }
    }
}
