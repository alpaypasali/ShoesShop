﻿using Microsoft.EntityFrameworkCore;
using ShoeShop.DataAccess.Abstract;
using ShoeShop.DataAccess.Concrete.Data;
using ShoeShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShop.DataAccess.Concrete.Repository
{
    public class EfUserRepository : IUserRepository
    {
        private readonly ShoeShopDbContext _dbContext;

        public EfUserRepository(ShoeShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IList<User> GetAll()
        {
            return _dbContext.Users.ToList();
        }

        public User GetById(int id)
        {
            return _dbContext.Users.Find(id);
        }

        public int Add(User entity)
        {
            _dbContext.Users.Add(entity);
            _dbContext.SaveChanges();
            return entity.ID;
        }

        public int Update(User entity)
        {
            _dbContext.Users.Update(entity);
            _dbContext.SaveChanges();
            return entity.ID;
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUserByEmail(string email)
        {
            return _dbContext.Users.SingleOrDefault(u => u.Email == email);
        }

        public bool IsExists(string email)
        {
            return _dbContext.Users.Any(u => u.Email == email);
        }

        public User GetUserByName(string name)
        {
            //return _dbContext.Users.Where(u => u.FullName == name).FirstOrDefault();
            return _dbContext.Users.SingleOrDefault(u => u.FullName == name);
        }

        public IList<User> GetAllByName()
        {
            return _dbContext.Users.OrderBy(u => u.FullName).ToList();
        }

        public void DeleteUser(string username)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.FullName == username);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException("A user with this name could not be found.");
            }
        }
    }
}
