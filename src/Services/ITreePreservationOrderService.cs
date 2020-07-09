using System.Threading.Tasks;
using tree_preservation_order_service.Models;

namespace tree_preservation_order_service.Services
{
    public interface ITreePreservationOrderService
    {
        Task<string> CreateCase(TreePreservationOrder treePreservationOrder);
    }
}
