using BusinessObjectLayer.DatabaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataAccessServices.UserAuthDataService
{
    public interface IUserAuthData
    {
        public Task<bool> IsRegistered(string email);

        public Task StoreUser(RegisteredPerson user);

        public Task<bool> AuthenticateUser(string email, string password);

        public Task<string> FetchUserRole(string email);

        public Task<RegisteredPerson> FetchUser(string email);
    }
}
