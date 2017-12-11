using System.Threading.Tasks;
using NMBM.Helpers;

namespace NMBM.Services.Burials
{
   public interface IBurialService
   {
       Task<ReturnResult> GetBurialAsync(long id);
       Task<ReturnResult> UpdateBurialCounterAsync(long burialApplication);
       Task<ReturnResult> BurialConfirmationAsync(long burialApplication);
   }
}
