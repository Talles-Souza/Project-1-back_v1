using Application.DTO;
using AutoMapper;
using Domain.Entities;
using System.Web.Http.OData;

namespace Application.Mapping
{
    public class DomainToDtoMapping : Profile
    {
        public DomainToDtoMapping()
        {
            CreateMap<User, UserDTO>();
          CreateMap<User,UserGoogleDTO>();
        }
    }
}
