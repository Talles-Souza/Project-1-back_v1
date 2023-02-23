using Application.DTO;
using Application.Service.Interfaces;
using AutoMapper;
using Domain.Authentication;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Web.Http.OData;

namespace Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _user;
        private readonly ITokenGenerator tokenGenerator;
        private readonly IMapper _mapper;
        public UserService(IUserRepository user, ITokenGenerator tokenGenerator, IMapper mapper)
        {
            _user = user;
            this.tokenGenerator = tokenGenerator;
            _mapper = mapper;
        }

        public async Task<ResultService<UserDTO>> Create(UserDTO userDTO)
        {
            if (userDTO == null) return ResultService.Fail<UserDTO>("Object must be informed");
            var result = new UserDTOValidator().Validate(userDTO);
            if (!result.IsValid) return ResultService.RequestError<UserDTO>("Problems in valdiation", result);
            var user = _mapper.Map<User>(userDTO);
            var data = await _user.Create(user);
            return ResultService.Ok(_mapper.Map<UserDTO>(data));
        }


        public async Task<ResultService> Delete(int id)
        {
            var user = await _user.FindById(id);
            if (user == null) return ResultService.Fail<UserDTO>("User not found");
            await _user.Delete(id);
            return ResultService.Ok("User with the id : " + id + " was successfully deleted");
        }

        public async Task<ResultService<ICollection<UserDTO>>> FindByAll()
        {
            var users = await _user.FindByAll();
            return ResultService.Ok<ICollection<UserDTO>>(_mapper.Map<ICollection<UserDTO>>(users));
        }

        public async Task<ResultService<UserDTO>> FindById(int id)
        {
            var person = await _user.FindById(id);
            if (person == null) return ResultService.Fail<UserDTO>("User not found");
            return ResultService.Ok(_mapper.Map<UserDTO>(person));
        }

        public async Task<ResultService<dynamic>> GenerateToken(LoginDTO loginDTO)
        {
            if (loginDTO == null) return ResultService.Fail<dynamic>("Object not found");

            var validator = new LoginDTOValidator().Validate(loginDTO);
            if (!validator.IsValid) return ResultService.RequestError<dynamic>(" Validation problems", validator);

            var user = await _user.Login(loginDTO.Email, loginDTO.Password);
            if (user == null) return ResultService.Fail<dynamic>("Username or password not found");

            return ResultService.Ok(tokenGenerator.Generator(user));
        }

        public async Task<ResultService<dynamic>> LoginWithGoogle(UserGoogleDTO userGoogleDTO)
        {
            if (userGoogleDTO == null) return ResultService.Fail<dynamic>("Object not found");
            var user = await _user.FindByEmail(userGoogleDTO.Email);
            if (user == null)
            {
                var userRegister = _mapper.Map<User>(userGoogleDTO);
                await _user.CreateWithGoogle(userRegister);
                var userLogin = await _user.LoginWithGoogle(userRegister.Email);
                if (userLogin == null) return ResultService.Fail<dynamic>("Login with google not found");
                return ResultService.Ok(tokenGenerator.GeneratorWithGoogle(userRegister));
            }
            var userLogin1 = await _user.LoginWithGoogle(userGoogleDTO.Email);
             if (userLogin1 == null) return ResultService.Fail<dynamic>("Login with google not found");
            user.Picture = userGoogleDTO.Picture;
            user.Name = userGoogleDTO.Name; 
            var userUpdate = await _user.UpdateWithGoogle(user);
           
            return ResultService.Ok(tokenGenerator.GeneratorWithGoogle(userLogin1));
        }

        public async Task<string> Patch(int id, string attribute, string field)
        {
            if (id == null || attribute == null || field == null) return "information must be entered correctly";
            var result = await _user.Patch(id, attribute, field);
            if (result == false) return "User not found or attribute spelled wrong";
            return "Successfully changed object";
        }

        public async Task<ResultService<UserDTO>> Update(UserDTO userDTO)
        {
            if (userDTO == null) return (ResultService<UserDTO>)ResultService.Fail("User must be informed");
            var result = new UserDTOValidator().Validate(userDTO);
            if (!result.IsValid) return ResultService.RequestError<UserDTO>("Problems in valdiation", result);
            var user = await _user.FindById(userDTO.Id);
            if (user == null) return (ResultService<UserDTO>)ResultService.Fail("User not found");
            user = _mapper.Map<UserDTO, User>(userDTO, user);
            var data = await _user.Update(user);
            return ResultService.Ok(_mapper.Map<UserDTO>(data));
        }
    }
}
