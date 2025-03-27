namespace EitechPfe.Models
{
    public class License
    {
        public int LicenseId { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdateAt { get; set; }
    }
}
