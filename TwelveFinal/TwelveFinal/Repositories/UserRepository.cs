using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace TwelveFinal.Repositories
{
    public interface IUserRepository
    {
        Task<bool> Create(User user);
        Task<User> Get(UserFilter userFilter);
        Task<bool> Update(User user);
        Task<bool> Delete(Guid Id);
    }
    public class UserRepository : IUserRepository
    {
        private readonly TFContext tFContext;
        public UserRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }

        private IQueryable<UserDAO> DynamicFilter(IQueryable<UserDAO> query, UserFilter userFilter)
        {
            if (!string.IsNullOrEmpty(userFilter.Username))
            {
                query = query.Where(u => u.Username.Equals(userFilter.Username));
            }
            if (userFilter.IsAdmin.HasValue)
            {
                query = query.Where(u => u.IsAdmin.Equals(userFilter.IsAdmin.Value));
            }
            return query;
        }

        public async Task<bool> Create(User user)
        {
            UserDAO userDAO = new UserDAO
            {
                Id = user.Id,
                IsAdmin = false,
                Username = user.Username,
                Password = user.Password,
                Salt = user.Salt
            };

            tFContext.User.Add(userDAO);
            await tFContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid Id)
        {
            await tFContext.User.Where(g => g.Id.Equals(Id)).DeleteFromQueryAsync();
            return true;
        }

        public async Task<User> Get(UserFilter userFilter)
        {
            IQueryable<UserDAO> users = tFContext.User;
            UserDAO userDAO = await DynamicFilter(users, userFilter).FirstOrDefaultAsync();
            if (userDAO == null) return null;
            else return new User()
            {
                Id = userDAO.Id,
                Username = userDAO.Username,
                Password = userDAO.Password,
                Salt = userDAO.Salt,
                IsAdmin = userDAO.IsAdmin
            };
        }

        public async Task<bool> Update(User user)
        {
            await tFContext.User.Where(u => u.Id.Equals(user.Id)).UpdateFromQueryAsync(u => new UserDAO
            {
                Password = u.Password,
            });

            return true;
        }
    }
}
