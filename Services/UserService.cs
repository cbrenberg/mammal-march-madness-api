using System.Threading.Tasks;
using AutoMapper;
using MMM_Bracket.API.Domain.Services;
using MMM_Bracket.API.Domain.Repositories;
using MMM_Bracket.API.Domain.Models;
using MMM_Bracket.API.Resources;


namespace MMM_Bracket.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public bool IsValidUser(string username, string password)
        {
            if (Authenticate(username, password) != null)
            {
                return true;
            }
            return false;
        }
        public async Task<UserResource> Authenticate(string username, string password)
        {
            User authenticatedUser = await _userRepository.Authenticate(username, password);
            return _mapper.Map<User, UserResource>(authenticatedUser);
        }
        public async Task<UserResource> GetUserById(int id)
        {
            User user = await _userRepository.GetById(id);
            return _mapper.Map<User, UserResource>(user);
        }
        public async Task<UserResource> GetUserByUsername(string username)
        {
            User user = await _userRepository.GetByUsername(username);
            return _mapper.Map<User, UserResource>(user);
        }
        public async Task<UserResource> SaveRefreshToken(int id, string newRefreshToken)
        {
            User user = await _userRepository.SaveRefreshToken(id, newRefreshToken);
            return _mapper.Map<User, UserResource>(user);
        }

        public async Task<bool> RegisterNewUser(User user)
        {
            bool wasRegistrationSuccessful = await _userRepository.AddUser(user);
            return wasRegistrationSuccessful;
        }
    }
}
