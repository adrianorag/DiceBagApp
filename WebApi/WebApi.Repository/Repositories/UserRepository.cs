using WebApi.Domain.Entities;
using WebApi.Service.Interfaces.Repositories;

namespace WebApi.Repository.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
    }
}
