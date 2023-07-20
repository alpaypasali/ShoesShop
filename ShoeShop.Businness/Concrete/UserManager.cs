﻿using AutoMapper;
using ShoeShop.Businness.Abstract;
using ShoeShop.DataAccess.Abstract;
using ShoeShop.Dtos.Concrete;
using ShoeShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShop.Businness.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserManager(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public UserDto GetUser(string email)
        {
            var user = _userRepository.GetUserByEmail(email);
            return _mapper.Map<UserDto>(user);
        }

        public bool IsExist(string email)
        {
            return _userRepository.IsExists(email);
        }

        public void AddUser(UserDto userDto)
        {
            var newUser = _mapper.Map<User>(userDto);
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            newUser.Role = "client";
            _userRepository.Add(newUser);
        }

        public UserDto ValidateUser(string email, string password)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return _mapper.Map<UserDto>(user);
            }
            else
            {
                return null;
            }
        }

        public UserDto GetUserByName(string name)
        {
            var user = _userRepository.GetUserByName(name);
            return _mapper.Map<UserDto>(user);
        }

        public UserDto GetUserById(int Id)
        {
            var user = _userRepository.GetById(Id);
            return _mapper.Map<UserDto>(user);
        }   

        public void UpdateUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _userRepository.Update(user);
        }

        public void ChangeUserRole(string username, string newRole)
        {
            var user = _userRepository.GetUserByName(username);
            if (user != null)
            {
                user.Role = newRole;
                _userRepository.Update(user);
            }
            else
            {
                throw new ArgumentException("Пользователь с таким именем не найден.");
            }
        }

        public IList<User> GetAllUsersSortedByName()
        {
            return _userRepository.GetAll();
        }
        public void DeleteUserByName(string username)
        {
            var user = _userRepository.GetUserByName(username);
            if (user != null)
            {
                _userRepository.DeleteUser(username);
            }
            else
            {
                throw new ArgumentException("Kullanıcı bulunamadı.");
            }
        }

    }
}
