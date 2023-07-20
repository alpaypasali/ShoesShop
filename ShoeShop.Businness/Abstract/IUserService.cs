using ShoeShop.Dtos.Concrete;
using ShoeShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShop.Businness.Abstract
{
    public interface IUserService
    {
        UserDto GetUser(string email);
        bool IsExist(string email);
        void AddUser(UserDto user);
        UserDto ValidateUser(string email, string password);
        UserDto GetUserByName(string name);
        void UpdateUser(UserDto userDto);
        UserDto GetUserById(int Id);
        void ChangeUserRole(string username, string newRole);
        IList<User> GetAllUsersSortedByName();
        void DeleteUserByName(string username);
    }
}
