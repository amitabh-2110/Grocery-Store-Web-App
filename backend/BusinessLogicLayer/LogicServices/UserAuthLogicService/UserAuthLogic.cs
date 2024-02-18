using BusinessObjectLayer.DatabaseEntities;
using DataAccessLayer.DataAccessServices.UserAuthDataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.LogicServices.UserAuthLogicService
{ 
    public class UserAuthLogic: IUserAuthLogic
    {
        private readonly IUserAuthData _userAuthData;

        public UserAuthLogic(IUserAuthData userAuthData)
        {
            _userAuthData = userAuthData;
        }

        public Task<bool> IsRegistered(string email)
        {
            return _userAuthData.IsRegistered(email);
        }

        public async Task RegisterUser(RegisteredPerson user)
        {
            await _userAuthData.StoreUser(user);
        }

        public async Task<bool> AuthenticateUser(string email, string password)
        {
            return await _userAuthData.AuthenticateUser(email, password);
        }

        public async Task<string> FetchUserRole(string email)
        {
            return await _userAuthData.FetchUserRole(email);
        }

        public async Task<RegisteredPerson> FetchUser(string userId)
        {
            return await _userAuthData.FetchUser(userId);
        }
    }
}
