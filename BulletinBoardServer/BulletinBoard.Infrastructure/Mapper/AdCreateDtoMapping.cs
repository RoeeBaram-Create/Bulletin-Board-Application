using AutoMapper;
using BulletinBoard_.Application.Dtos;
using CoreBoard_.Domain.Entities;

namespace BulletinBoard.Infrastructure.Mapper
{
    public class AdCreateDtoMapping : Profile
    {
        public AdCreateDtoMapping()
        {
            CreateMap<AdCreateDto, Ad>();
        }
    }
}
