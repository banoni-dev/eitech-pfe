namespace EitechPfe.Interfaces
{
    public interface ILicenseRepository
    {
        Task<int> Add(License license);
        Task<License?> GetById(int id);
        Task<IEnumerable<License>> GetAll();
        Task<int> Update(License license);
        Task<int> Delete(int id);
        Task<LicenseOrder?> GetLicenseOrderByUserAndLicense(int userId, int licenseId);
    }
}
