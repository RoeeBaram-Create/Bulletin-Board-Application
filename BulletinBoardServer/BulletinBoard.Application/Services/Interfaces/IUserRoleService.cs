using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard_.Application.Services.Interfaces
{
    public interface IUserRoleService
    {
        Task<IList<string>> GetUserRolesAsync(string userId);
        Task InvalidationUserCacheAsync(string userId);
    }
}
