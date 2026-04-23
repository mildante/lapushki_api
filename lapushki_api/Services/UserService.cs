using lapushki_api.Data;
using lapushki_api.Interfaces;
using lapushki_api.Models;
using lapushki_api.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;

namespace lapushki_api.Services
{
    public class UserService : IUserService
    {
        private readonly ContextDb _ContextDb;

        public UserService(ContextDb ContextDb)
        {
            _ContextDb = ContextDb;
        }
        private string GenerateJwtToken(int userId)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("FBvVYnUa1JOqCGw8KjAS3XPRwjkqNSdpcOgkfKfNHT4d63DwbALx7PeVyrxe2Is4"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<IActionResult> AuthByToken(ClaimsPrincipal claims)
        {
            var userId = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return new UnauthorizedResult();
            }

            int id = int.Parse(userId);

            var user = await _ContextDb.Users.FirstOrDefaultAsync(x => x.id_user == id);

            if (user == null)
            {
                return new OkObjectResult(new { status = false, error = "не найден пользователь" });
            }

            return new OkObjectResult(new
            {
                status = true,
                user
            });
        }
        public async Task<IActionResult> RegistrationUser(UserModel userModel)
        {
            var isEmailNotUnique = await _ContextDb.Users.AnyAsync(x => x.email == userModel.email);
            if (isEmailNotUnique)
                return new OkObjectResult(new { status = false, message = "Почта уже зарегистрирована"});
            var isPhoneNotUnique = await _ContextDb.Users.AnyAsync(x => x.email == userModel.email);
            if (isPhoneNotUnique)
                return new OkObjectResult(new { status = false, message = "Телефон уже занят" });

            var newUser = new User()
            {
                name = userModel.name,
                surname = userModel.surname,
                email = userModel.email,
                phone = userModel.phone,
                password = userModel.password,
                gender = userModel.gender,
                avatar = "http://localhost:5276/images/default-avatar.png",
                date_of_birth = userModel.date_of_birth,
                role_id = 3
            };

            await _ContextDb.AddAsync(newUser);
            await _ContextDb.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                message = "Пользователь успешно зарегистрирован"
            });

        }
        public async Task<IActionResult> AuthUser(AuthModel authModel)
        {
            var user = await _ContextDb.Users.FirstOrDefaultAsync(x => x.email == authModel.email && x.password == authModel.password);

            if (user == null)
                return new OkObjectResult(new { status = false, message = "Пользователь не найден"});

            var token = GenerateJwtToken(user.id_user);

            return new OkObjectResult(new
            {
                status = true,
                token,
                user = user,
                message = "Вход выполнен"
            });
        }

        public async Task<IActionResult> GetAllUser()
        {
            var listUser = await _ContextDb.Users.Where(x=>x.role_id == 3).ToListAsync();

            return new OkObjectResult(new
            {
                status = true,
                list = listUser
            });
        }
        public async Task<IActionResult> UpdateUser(UserModel userModel)
        {
            var user = await _ContextDb.Users.FirstOrDefaultAsync(x => x.id_user == userModel.id_user);
            if (user == null)
                return new OkObjectResult(new { status = false, message = "Пользователь не найден" });

            user.name = userModel.name;
            user.surname = userModel.surname;
            user.email = userModel.email;
            user.phone = userModel.phone;
            user.password = userModel.password;
            user.gender = userModel.gender;
            user.avatar = userModel.avatar;
            user.date_of_birth = userModel.date_of_birth;

            _ContextDb.Update(user);
            await _ContextDb.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                message = "Данные пользователя обновлены"
            });
        }

        public async Task<IActionResult> DeleteUser(int user_id)
        {
            var user = await _ContextDb.Users.FirstOrDefaultAsync(x => x.id_user == user_id);

            if (user == null)
                return new OkObjectResult(new { status = false, message = "Пользователь не найден"});

            if (user.role_id == 1 || user.role_id == 2)
                return new OkObjectResult(new { status = false, message = "Нельзя удалять администраторов и докторов" });

            _ContextDb.Remove(user);
            await _ContextDb.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                message = "Пользователь удален"
            });

        }
    }
}
