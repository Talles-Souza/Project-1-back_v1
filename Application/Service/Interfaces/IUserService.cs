using Application.DTO;
using Microsoft.AspNetCore.JsonPatch;
using System.Web.Http.OData;

namespace Application.Service.Interfaces
{
    public interface IUserService
    {
        Task<ResultService<dynamic>> GenerateToken(LoginDTO loginDTO);
        Task<ResultService<UserDTO>> Create(UserDTO userDTO);
        Task<ResultService<dynamic>> LoginWithGoogle(UserGoogleDTO userGoogleDTO);
        Task<ResultService<ICollection<UserDTO>>> FindByAll();
        Task<ResultService<UserDTO>> FindById(int id);
        Task<ResultService<UserDTO>> Update(UserDTO userDTO);
        Task<ResultService> Delete(int id);
        Task<string>Patch(int id, string attribute, string field);
    }
}
