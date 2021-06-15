using Microsoft.EntityFrameworkCore;
using NetCoreApi.Interfaces;
using NetCoreApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;

        }

        public async Task<ServiceResponse<string>> Login(string userName, string password)
        {
            var response = new ServiceResponse<string>();
            if(! await UserExists(userName))
            {
                response.IsSuccess = false;
                response.Message = "User does not exists.";
                return response;
            }

            var userData = await _context.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == userName.ToLower());

            if(!VerifyPassword(password, userData.PasswordHash, userData.PasswordSalt))
            {
                response.IsSuccess = false;
                response.Message = "Incorrect password.";
            }
            response.Data = userData.Id.ToString();
            return response;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            var response = new ServiceResponse<int>();
            if(await UserExists(user.UserName))
            {
                response.IsSuccess = false;
                response.Message = "Username is taken.";
                return response;
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _context.Add(user);
            await _context.SaveChangesAsync();

            response.Data = user.Id;

            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _context.Users.AnyAsync(x => x.UserName.ToLower().Equals(username.ToLower())))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i< computeHash.Length; i++ )
                {
                    if( computeHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }            
        }
    }
}
