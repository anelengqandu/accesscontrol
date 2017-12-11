using System.Threading.Tasks;
using NMBM.Helpers;

namespace NMBM.Services.Authentication
{
    public interface IAuthService
    {
        Task<ReturnResult> LoginAsync(string userName, string password);
    }
}
