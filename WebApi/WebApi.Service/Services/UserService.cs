using WebApi.Domain.Entities;
using WebApi.Domain.Services.Interfaces;
using WebApi.Service.Interfaces.Repositories;

namespace WebApi.Service.Services
{
    public class UserService : ServiceBase<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }
    }
}
