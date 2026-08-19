using AutoMapper;
using BulletinBoard_.Application.Dtos;
using CoreBoard_.Domain.Entities;

namespace BulletinBoard.Infrastructure.Mapper
{
    public class AdUpdateDtoMapping : Profile
    {
        public AdUpdateDtoMapping()
        {
            CreateMap<AdUpdateDto, Ad>();
        }
    }
}
