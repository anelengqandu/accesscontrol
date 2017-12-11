
using System.Linq;
using System.Threading.Tasks;
using ApiServices.EntityFramework.EntityModel;
using ApiServices.Models;

namespace ApiServices.EntityFramework.Authentication
{
    public class Authentication
    {
        public static async Task<SupervisorModel> AuthenticateUser(string username, string password)
        {
            SupervisorModel result = null;
            var q = DataAccess.Context.CEM_LIST_CEMETERY_SUPERVISOR.Where(row => row.UserName == username && row.SupervisorPassword == password).Select(row => new
            {
                Id = row.Id,
                Name = row.Name,
                Surname = row.Surname,
                CemeteryId = row.CemeteryId,
                UserName = row.UserName,
                SupervisorPassword = row.SupervisorPassword
            });
            var qUser = q.FirstOrDefault();
            var user = qUser;

            if (user != null)
            {
                result = new SupervisorModel { SupervisorId = user.Id, Name = user.Name, Surname = user.Surname, CemeteryId = user.CemeteryId, UserName = user.UserName, SupervisorPassword = user.SupervisorPassword };
            }
            return result;
        }
    }
}