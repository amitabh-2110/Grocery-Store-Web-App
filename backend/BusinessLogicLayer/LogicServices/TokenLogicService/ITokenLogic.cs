using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.LogicServices.TokenLogicService
{
    public interface ITokenLogic
    {
        public string CreateToken(string email, string role);
    }
}
