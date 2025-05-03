namespace EitechPfe.Interfaces
{
    public interface ILicenseService
    {
        Task<int> CreateLicense(License license);
        Task<License?> GetLicenseById(int id);
        Task<IEnumerable<License>> GetAllLicenses();
        Task<int> UpdateLicense(License license);
        Task<int> DeleteLicense(int id);
        Task<bool> CheckLicense(int userId, int productId, string? fingerprint = null);
    }
}
