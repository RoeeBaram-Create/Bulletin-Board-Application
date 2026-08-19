using CoreBoard_.Domain.Enums;

namespace BulletinBoard_.Application.Dtos
{
    public class AdFilterDto
    {
        public Category? Category { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? Title { get; set; }
        public string? Location { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? RadiusInKm { get; set; } = 10;
    }
}
