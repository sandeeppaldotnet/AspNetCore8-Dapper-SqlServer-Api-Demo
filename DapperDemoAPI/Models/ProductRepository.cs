using Dapper;
using DapperDemoAPI.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DapperDemoAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task<IEnumerable<Product>> GetAllAsync(bool useStoredProc = false)
        {
            var sql = useStoredProc ? "GetAllProducts" : "SELECT * FROM Products";
            using var connection = CreateConnection();
            return await connection.QueryAsync<Product>(
                sql,
                commandType: useStoredProc ? CommandType.StoredProcedure : CommandType.Text
            );
        }

        public async Task<Product?> GetByIdAsync(int id, bool useStoredProc = false)
        {
            var sql = useStoredProc ? "GetProductById" : "SELECT * FROM Products WHERE Id = @Id";
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Product>(
                sql,
                new { Id = id },
                commandType: useStoredProc ? CommandType.StoredProcedure : CommandType.Text
            );
        }

        public async Task<int> CreateAsync(Product product, bool useStoredProc = false)
        {
            using var connection = CreateConnection();

            if (useStoredProc)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Name", product.Name);
                parameters.Add("@Price", product.Price);
                parameters.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await connection.ExecuteAsync("InsertProduct", parameters, commandType: CommandType.StoredProcedure);
                return parameters.Get<int>("@NewId");
            }
            else
            {
                var sql = "INSERT INTO Products (Name, Price) VALUES (@Name, @Price); SELECT CAST(SCOPE_IDENTITY() as int)";
                return await connection.ExecuteScalarAsync<int>(sql, product);
            }
        }

        public async Task<bool> UpdateAsync(Product product, bool useStoredProc = false)
        {
            var sql = useStoredProc ? "UpdateProduct" : "UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id";
            using var connection = CreateConnection();
            var rows = await connection.ExecuteAsync(
                sql,
                product,
                commandType: useStoredProc ? CommandType.StoredProcedure : CommandType.Text
            );
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id, bool useStoredProc = false)
        {
            var sql = useStoredProc ? "DeleteProduct" : "DELETE FROM Products WHERE Id = @Id";
            using var connection = CreateConnection();
            var rows = await connection.ExecuteAsync(
                sql,
                new { Id = id },
                commandType: useStoredProc ? CommandType.StoredProcedure : CommandType.Text
            );
            return rows > 0;
        }
    }
}
