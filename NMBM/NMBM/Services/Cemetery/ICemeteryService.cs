using System.Threading.Tasks;
using NMBM.Helpers;

namespace NMBM.Services.Cemetery
{
    public interface ICemeteryService
    {
        Task<ReturnResult> GetCemeteryAsync(long id);
    }
}
