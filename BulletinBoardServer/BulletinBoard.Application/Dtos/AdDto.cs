using CoreBoard_.Domain.Enums;

namespace BulletinBoard_.Application.Dtos
{
    public class AdDto
    {
        public Guid Id { get; set; } 
        public string? Title { get; set; } 
        public string? Description { get; set; } 
        public Category? Category { get; set; }
        public string? CategoryDescription => Category?.GetDescription();
        public decimal? Price { get; set; }
        public string? City { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}
