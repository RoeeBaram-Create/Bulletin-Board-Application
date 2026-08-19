using BulletinBoard_.Application.Dtos;
using CoreBoard_.Domain.Entities;

namespace BulletinBoard_.Application.Services.Interfaces
{
    public interface IAdService
    {
        Task<IEnumerable<Ad>> GetAdsAsync(AdFilterDto filter);
        Task DeleteAdAsync(Guid id);
        Task<Ad?> UpdateAdAsync(Ad ad);
        Task<Ad> CreateAdAsync(Ad dto);
    }
}
