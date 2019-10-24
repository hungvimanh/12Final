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
            if (!string.IsNullOrEmpty(userFilter.FullName))
            {
                query = query.Where(u => u.FullName.Equals(userFilter.FullName));
            }
            if (!string.IsNullOrEmpty(userFilter.Email))
            {
                query = query.Where(u => u.Email.Equals(userFilter.Email));
            }
            if (!string.IsNullOrEmpty(userFilter.Phone))
            {
                query = query.Where(u => u.Phone.Equals(userFilter.Phone));
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
                IsAdmin = user.IsAdmin,
                FullName = user.FullName,
                Password = user.Password,
                Salt = user.Salt,
                Email = user.Email,
                Phone = user.Phone,
                Gender = user.Gender
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
                FullName = userDAO.FullName,
                Password = userDAO.Password,
                Salt = userDAO.Salt,
                IsAdmin = userDAO.IsAdmin,
                Email = userDAO.Email,
                Phone = userDAO.Phone,
                Gender = userDAO.Gender
            };
        }

        public async Task<bool> Update(User user)
        {
            await tFContext.User.Where(u => u.Id.Equals(user.Id)).UpdateFromQueryAsync(u => new UserDAO
            {
                Password = u.Password,
                Email = u.Email,
                Phone = u.Phone,
                Gender = u.Gender
            });

            return true;
        }
    }
}
