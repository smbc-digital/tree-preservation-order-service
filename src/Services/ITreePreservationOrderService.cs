using StockportGovUK.NetStandard.Models.Models.Verint.VerintOnlineForm;
using System.Threading.Tasks;

namespace tree_preservation_order_service.Services
{
    public interface ITreePreservationOrderService
    {
        Task<string> CreateVOFCase(VerintOnlineFormRequest model);
    }
}
