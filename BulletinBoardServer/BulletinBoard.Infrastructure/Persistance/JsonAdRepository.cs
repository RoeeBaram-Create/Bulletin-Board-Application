using BulletinBoard.Domain.Interfaces;
using BulletinBoard.Infrastructure.Settings;
using BulletinBoard_.Application.Dtos;
using CoreBoard_.Domain.Entities;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BulletinBoard.Infrastructure.Persistance
{
    public class JsonAdRepository : IAdRepository
    {
        private readonly JsonSerializerOptions _options = new()
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() } // זה השורה החשובה
        };
        private readonly string _filePath;
        private static readonly SemaphoreSlim _semaphore = new(1, 1); // למניעת התנגשויות כתיבה

        public JsonAdRepository(IOptions<FileStorageSettings> settings)
        {
            _filePath = Path.Combine(AppContext.BaseDirectory, settings.Value.AdsFilePath);
        }

        private async Task<List<Ad>> ReadFromFileAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                var json = await File.ReadAllTextAsync(_filePath);
                return JsonSerializer.Deserialize<List<Ad>>(json, _options) ?? new List<Ad>();
            }
            finally { _semaphore.Release(); }
        }

        private async Task WriteToFileAsync(List<Ad> ads)
        {
            await _semaphore.WaitAsync();
            try
            {
                var json = JsonSerializer.Serialize(ads, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(_filePath, json);
            }
            finally { _semaphore.Release(); }
        }

        public async Task<IEnumerable<Ad>> GetAllAsync(AdFilterDto? filter = null)
        {
            var allAds = await ReadFromFileAsync(); 
            var query = allAds.AsQueryable();

            if (filter != null)
            {
                if (filter.Category.HasValue)
                    query = query.Where(a => a.Category == filter.Category);

                if (filter.MaxPrice.HasValue)
                    query = query.Where(a => a.Price <= filter.MaxPrice.Value);

                if (string.IsNullOrEmpty(filter.Title) == false)
                    query = query.Where(a => a.Title.Contains(filter.Title, StringComparison.OrdinalIgnoreCase));

                if (string.IsNullOrWhiteSpace(filter.Location) == false)
                    query = query.Where(a => 
                    a.City.Contains(filter.Location, StringComparison.OrdinalIgnoreCase));

                if(filter.Latitude.HasValue && filter.Longitude.HasValue)
                    query = query.Where(a =>
                    CalculateDistance(filter.Latitude.Value, filter.Longitude.Value,
                    a.Latitude, a.Longitude) <= filter.RadiusInKm);
            }

            return query.ToList();
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            double R = 6371; 

            double dLat = (lat2 - lat1) * Math.PI / 180;
            double dLon = (lon2 - lon1) * Math.PI / 180;

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(lat1 * Math.PI / 180) * Math.Cos(lat2 * Math.PI / 180) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }

        public async Task<Ad> AddAsync(Ad ad)
        {
            List<Ad> ads = await ReadFromFileAsync();

            ad.Id = Guid.NewGuid();
            ad.CreatedAt = DateTime.UtcNow;
            ads.Add(ad);
            await WriteToFileAsync(ads);

            return ad;
        }
    
        public async Task<Ad?> UpdateAsync(Ad ad)
        {
          
            var ads = await ReadFromFileAsync();

            var index = ads.FindIndex(a => a.Id == ad.Id);

            if (index == -1)
                return null;

            ads[index] = ad;
            await WriteToFileAsync(ads);

            return ad;
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            var ads = await ReadFromFileAsync();

            var ad = ads.FirstOrDefault(a => a.Id == id);
            if (ad == null) return false; 

            ads.Remove(ad);

            await WriteToFileAsync(ads);
            return true; 
        }
    }
}
