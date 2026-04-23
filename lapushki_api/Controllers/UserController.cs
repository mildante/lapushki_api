using lapushki_api.Interfaces;
using lapushki_api.Requests;
using Microsoft.AspNetCore.Mvc;

namespace lapushki_api.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("authUserByToken")]
        public async Task<IActionResult> AuthUserByToken()
        {
            return await _userService.AuthByToken(User);
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> RegistrationUser([FromBody] UserModel userModel)
        {
            return await _userService.RegistrationUser(userModel);
        }

        [HttpPost]
        [Route("authorize")]
        public async Task<IActionResult> AuthUser([FromBody] AuthModel authModel)
        {
            return await _userService.AuthUser(authModel);
        }

        [HttpGet]
        [Route("getAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            return await _userService.GetAllUser();
        }

        [HttpPut]
        [Route("updateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserModel userModel)
        {
            return await _userService.UpdateUser(userModel);
        }

        [HttpDelete]
        [Route("deleteUser")]
        public async Task<IActionResult> DeleteUser(int user_id)
        {
            return await _userService.DeleteUser(user_id);
        }
    }
}
