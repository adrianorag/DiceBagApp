using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Entities;

namespace WebApi.Application.Interface
{
    public interface IUserAppService : IAppServiceBase<User>
    {
    }
}
