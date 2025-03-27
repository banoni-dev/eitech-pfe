public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<int> CreateUser(User user) => _userRepository.CreateUser(user);
    public Task<User?> GetUserById(int id) => _userRepository.GetUserById(id);
    public Task<IEnumerable<User>> GetAllUsers() => _userRepository.GetAllUsers();
    public Task<int> UpdateUser(User user) => _userRepository.UpdateUser(user);
    public Task<int> DeleteUser(int id) => _userRepository.DeleteUser(id);
}
