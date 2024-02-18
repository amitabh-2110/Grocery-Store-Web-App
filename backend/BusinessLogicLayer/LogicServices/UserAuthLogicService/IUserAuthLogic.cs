using BusinessObjectLayer.DatabaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.LogicServices.UserAuthLogicService
{
    public interface IUserAuthLogic
    {
        public Task<bool> IsRegistered(string email);

        public Task RegisterUser(RegisteredPerson user);

        public Task<string> FetchUserRole(string email);

        public Task<bool> AuthenticateUser(string email, string password);

        public Task<RegisteredPerson> FetchUser(string userId);
    }
}
