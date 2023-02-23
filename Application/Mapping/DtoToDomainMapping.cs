using Application.DTO;
using AutoMapper;
using Domain.Entities;
using System.Web.Http.OData;

namespace Application.Mapping
{
    public class DtoToDomainMapping : Profile
    {
        public DtoToDomainMapping()
        {
            CreateMap<UserDTO, User>();
          CreateMap<UserGoogleDTO, User>();
        }
    }
}
