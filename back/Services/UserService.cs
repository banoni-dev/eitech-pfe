namespace EitechPfe.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> CreateUser(User user)
        {
            return await _userRepository.CreateUser(user);
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<int> UpdateUser(User user)
        {
            return await _userRepository.UpdateUser(user);
        }

        public async Task<int> DeleteUser(int id)
        {
            return await _userRepository.DeleteUser(id);
        }
    }
}
