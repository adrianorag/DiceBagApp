using WebApi.Application.Interface;
using WebApi.Domain.Entities;
using WebApi.Domain.Services.Interfaces;

namespace WebApi.Application
{
    public class UserAppService : AppServiceBase<User>, IUserAppService
    {

        private readonly IUserService _userService;


        public UserAppService(IUserService userService) : base(userService)
        {
            _userService = userService;
        }

    }
}
