using Data.Context;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ContextDb db;

        public UserRepository(ContextDb db)
        {
            this.db = db;
        }

        public async Task<User> Login(string email, string password)
        {
            var pass = ComputeHash(password, new SHA256CryptoServiceProvider());
            return await db.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == pass);
        }
        public async Task<User> LoginWithGoogle(string email)
        {
            return await db.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
        private string ComputeHash(string input, SHA256CryptoServiceProvider algoriti)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algoriti.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }
        public async Task<User> Create(User user)
        {
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            user.Password = pass;
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return user;
        }
        public async Task<User> CreateWithGoogle(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return user;
        }


        public async Task<ICollection<User>> FindByAll()
        {
           List<User> users = await db.Users.ToListAsync();
            return users;
        }

        public async Task<User> FindById(int id)
        {
           User user = await db.Users.SingleOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<User> FindByEmail(string email)
        {
            User user = await db.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }
        public async Task<User> Update(User user)
        {
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            user.Password = pass;
            db.Users.Update(user);
            await db.SaveChangesAsync();
            return user;
        }
        public async Task<User> UpdateWithGoogle(User user)
        {
            db.Users.Update(user);
            await db.SaveChangesAsync();
            return user;
        }


        public async Task<bool> Delete(int id)
        {
            try
            {
                User user = await db.Users.SingleOrDefaultAsync(x => x.Id == id);
                if (user == null) return false;
                db.Users.Remove(user);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> Patch(int id, string attribute, string field)
        {
            User user = await db.Users.SingleOrDefaultAsync(x => x.Id == id);
            if (user == null) return false;
            {
                if (attribute == "name") user.Name = field;
                else if (attribute == "document") user.Document = field;
                else if (attribute == "email") user.Email = field;
                else if (attribute == "phone") user.Phone = field;
                else if (attribute == "password")
                {
                    var pass = ComputeHash(field, new SHA256CryptoServiceProvider());
                    user.Password = pass;
                }
                else { return false; }
            } 
            db.Users.Update(user);
            await db.SaveChangesAsync();
            return true;
        }
    }
}
