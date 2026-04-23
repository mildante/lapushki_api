using lapushki_api.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace lapushki_api.Interfaces
{
    public interface IUserService
    {
        Task<IActionResult> RegistrationUser(UserModel userModel);
        Task<IActionResult> AuthUser(AuthModel authModel);
        Task<IActionResult> GetAllUser();
        Task<IActionResult> UpdateUser(UserModel userModel);
        Task<IActionResult> DeleteUser(int user_id);
        Task<IActionResult> AuthByToken(ClaimsPrincipal claims);
    }
}
