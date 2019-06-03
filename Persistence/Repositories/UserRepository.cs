using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MMM_Bracket.API.Domain.Models;
using MMM_Bracket.API.Domain.Repositories;
using MMM_Bracket.API.Persistence.Contexts;

namespace MMM_Bracket.API.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public UserRepository(mmm_bracketContext context) : base(context)
        {
        }

        public async Task<User> Authenticate(string username, string password)
        {
            User user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);

            if (user != null && PasswordMatches(user, password))
            {
                return user;
            }

            return null;
        }

        private bool PasswordMatches(User user, string password)
        {
            PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
            if (result == PasswordVerificationResult.Failed )
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<User> SaveRefreshToken(int id, string newRefreshToken)
        {
            try
            {
                User userToEdit = await GetById(id);
                userToEdit.RefreshToken = newRefreshToken;
                _context.Users.Attach(userToEdit).Property(x => x.RefreshToken).IsModified = true;

                await _context.SaveChangesAsync();
                return userToEdit;
            }
            catch (DbUpdateException e)
            {
                throw new Exception("Error: Unable to save new refresh token", e);
            }
        }

        public async Task<bool> AddUser(User user)
        {
            try
            {
                user.Password = _passwordHasher.HashPassword(user, user.Password);
                _context.Users.Attach(user);
                await _context.SaveChangesAsync();
            } 
            catch
            {
                return false;
            }

            return true;
        }
    }
}