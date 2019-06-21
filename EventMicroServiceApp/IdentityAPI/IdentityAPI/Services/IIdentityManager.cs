using IdentityAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAPI.Services
{
    public interface IIdentityManager
    {
        Task<dynamic> AddUserAsync(User user);

        string ValidateUsers(LoginModel user);
    }
}
