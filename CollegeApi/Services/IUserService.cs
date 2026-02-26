using CollegeApi.Models;

namespace CollegeApi.Services
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(UserDTO dto);
        Task<List<UserReadonlyDTO>> GetUsersAsync();
        Task<UserReadonlyDTO> GetUserByIdAsync(int id);
        Task<UserReadonlyDTO> GetUserByUsernameAsync(string username);
        Task<bool> UpdateUserAsync(UserDTO dto);
        Task<bool> DeleteUserAsync(int userId);
        (string PasswordHash, string Salt) CreatePasswordHashWithSalt(string password);
    }
}
