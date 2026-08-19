using BulletinBoard_.Application.Dtos;
using CoreBoard_.Domain.Entities;

namespace BulletinBoard.Domain.Interfaces
{
    public interface IAdRepository
    {
        Task<IEnumerable<Ad>> GetAllAsync(AdFilterDto? filter = null);
        Task<Ad> AddAsync(Ad ad);
        Task<Ad?> UpdateAsync(Ad ad);
        Task<bool> DeleteAsync(Guid id);
    }
}
