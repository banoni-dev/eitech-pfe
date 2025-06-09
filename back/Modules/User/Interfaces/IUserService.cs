using EitechPfe.Modules.User;

namespace EitechPfe.Modules.User.Interfaces
{
    public interface IUserService
    {
        Task<int> CreateUser(User user);
        Task<User?> GetUserById(int id);
        Task<IEnumerable<User>> GetAllUsers();
        Task<int> UpdateUser(User user);
        Task<int> DeleteUser(int id);
    }
}
