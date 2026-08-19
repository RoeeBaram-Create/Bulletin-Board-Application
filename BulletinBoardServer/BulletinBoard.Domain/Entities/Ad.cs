using CoreBoard_.Domain.Enums;

namespace CoreBoard_.Domain.Entities
{
    public class Ad
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public AdStatus Status { get; set; } = AdStatus.Active;
        public Category? Category { get; set; }
        public Condition Condition { get; set; }
        public List<string> ImageUrls { get; set; } = new();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public string ContactPhone { get; set; } = string.Empty;
        public string SellerEmail { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string? LocationName { get; set; }
        public double Latitude { get; set; }    
        public double Longitude { get; set; }  
    }
}
