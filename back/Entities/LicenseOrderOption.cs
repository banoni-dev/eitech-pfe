namespace EitechPfe.Entities
{
    public class LicenseOrderOption
    {
        public int OrderOptionId { get; set; }
        public int LicenseOrderId { get; set; }
        public int OptionId { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdateAt { get; set; } = DateTime.UtcNow;
    }
}
