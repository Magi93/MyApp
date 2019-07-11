using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public interface IUserActions
    {
        Task<ApiResponse> GetAllUser();
        Task<ApiResponse> Authenticate(string username, string password);
        string ValidatePassword(string password);
        string GeneratePassword(string password);
        Task<ApiResponse> Insert(PostUser user);
    }
}
