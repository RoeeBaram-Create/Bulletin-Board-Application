using AutoMapper;
using BulletinBoard_.Application.Dtos;
using CoreBoard_.Domain.Entities;

namespace BulletinBoard.Infrastructure.Mapper
{
    public class AdMapping:Profile
    {
        public AdMapping()
        {
            CreateMap<Ad, AdDto>();
        }
    }
}
