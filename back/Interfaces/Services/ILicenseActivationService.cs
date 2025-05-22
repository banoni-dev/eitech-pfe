namespace EitechPfe.Interfaces
{
    public interface ILicenseActivationService
    {
        Task<int> CreateLicenseActivation(LicenseActivation licenseActivation);
        Task<LicenseActivation?> GetLicenseActivationById(int id);
        Task<IEnumerable<LicenseActivation>> GetAllLicenseActivations();
        Task<int> UpdateLicenseActivation(LicenseActivation licenseActivation);
        Task<int> DeleteLicenseActivation(int id);
    }
}