public interface IUserService
{
    Task<int> CreateUser(User user);
    Task<User?> GetUserById(int id);
    Task<IEnumerable<User>> GetAllUsers();
    Task<int> UpdateUser(User user);
    Task<int> DeleteUser(int id);
}
