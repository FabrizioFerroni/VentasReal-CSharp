using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVentas.Models.Dto;

namespace WSVentas.Services
{
    public interface IUserService
    {
        UserDto Auth(AuthDao dao);
    }
}
