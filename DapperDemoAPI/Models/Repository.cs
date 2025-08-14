using DapperDemoAPI.Models;

namespace DapperDemoAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync(bool useStoredProc = false);
        Task<Product?> GetByIdAsync(int id, bool useStoredProc = false);
        Task<int> CreateAsync(Product product, bool useStoredProc = false);
        Task<bool> UpdateAsync(Product product, bool useStoredProc = false);
        Task<bool> DeleteAsync(int id, bool useStoredProc = false);
    }
}
