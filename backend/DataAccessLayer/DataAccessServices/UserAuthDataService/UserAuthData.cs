using BusinessObjectLayer.Data;
using BusinessObjectLayer.DatabaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataAccessServices.UserAuthDataService
{
    public class UserAuthData: IUserAuthData
    {
        private readonly ManageDb _context;

        public UserAuthData(ManageDb context)
        {
            _context = context;
        }

        public async Task<bool> IsRegistered(string email)
        {
            var user = await _context.RegisteredPersons.FindAsync(email);
            return user != null;
        }

        public async Task<bool> AuthenticateUser(string email, string password)
        {
            var user = await _context.RegisteredPersons.FindAsync(email);
            if(user != null)
            {
                return user.Password == password;
            }
            return false;
        } 

        public async Task<string> FetchUserRole(string email)
        {
            var user = await _context.RegisteredPersons.FindAsync(email);

            if (user != null)
                return user.Role;

            return "";
        }

        public async Task<RegisteredPerson> FetchUser(string email)
        {
            var user = await _context.RegisteredPersons.FindAsync(email);
            return user;
        }

        public async Task StoreUser(RegisteredPerson user)
        {
            await _context.RegisteredPersons.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
