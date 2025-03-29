namespace EitechPfe.Entities
{
    public enum BlackListType
    {
        IP,
        User,
        Device
    }

    public class BlackListed
    {
        public int Id { get; set; }
        public string Ip { get; set; } = string.Empty;
        public BlackListType Type { get; set; }
        public DateTime BlockedDate { get; set; }
        public DateTime? RecoveryDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdateAt { get; set; } = DateTime.UtcNow;
    }
}
