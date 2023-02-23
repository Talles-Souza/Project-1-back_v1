using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Web.Http.OData;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> Login(string email, string password);
        Task<User> LoginWithGoogle(string email);
        Task<User> FindById(int id);
        Task<User> FindByEmail(string sub);
        Task<ICollection<User>> FindByAll();
        Task<User> Create(User user);
        Task<User> CreateWithGoogle(User user);
        Task<User> Update(User user);
        Task<User> UpdateWithGoogle(User user);
        Task<bool> Delete(int id);
        Task<bool> Patch(int id,string attribute, string field);
   
    }
}
