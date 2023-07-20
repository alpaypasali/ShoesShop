using ShoeShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShop.DataAccess.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByEmail(string email);
        bool IsExists(string email);
        User GetUserByName(string name);
        IList<User> GetAllByName();
        void DeleteUser(string username);
    }
}
