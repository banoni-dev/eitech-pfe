using Dapper;
using System.Data;
using EitechPfe.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EitechPfe.Repositories
{
    public class LicenseRepository : ILicenseRepository
    {
        private readonly ApplicationDbContext _context;

        public LicenseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(License license)
        {
            _context.Licenses.Add(license);
            return await _context.SaveChangesAsync();
        }

        public async Task<License?> GetById(int id)
        {
            return await _context.Licenses.FindAsync(id);
        }

        public async Task<IEnumerable<License>> GetAll()
        {
            return await _context.Licenses.ToListAsync();
        }

        public async Task<int> Update(License license)
        {
            _context.Licenses.Update(license);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var license = await _context.Licenses.FindAsync(id);
            if (license != null)
            {
                _context.Licenses.Remove(license);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
    }
}
