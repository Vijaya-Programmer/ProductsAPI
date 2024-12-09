using Npgsql;
using ProductsAPI.Models;

namespace ProductsAPI.Repository
{
    public class ProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = new List<Product>();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand("SELECT * FROM \"Product\" ORDER BY \"Id\"", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"]?.ToString(),
                        Type = reader["Type"]?.ToString(), 
                        Description = reader["Description"]?.ToString(),
                        Price = (decimal)reader["Price"],
                        PictureUrl = reader["PictureUrl"]?.ToString()
                    });
                }
            }
            return products;
        }


        public List<Product> GetProductByType(string type)
        {
            List<Product> products = new List<Product>();  // List to hold multiple products
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand("SELECT * FROM \"Product\" WHERE \"Type\" = @Type", connection);
                command.Parameters.AddWithValue("@Type", type);

                var reader = command.ExecuteReader();
                while (reader.Read())  // Loop through all rows returned
                {
                    var product = new Product
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"]?.ToString(),
                        Type = reader["Type"]?.ToString(),
                        Description = reader["Description"]?.ToString(),
                        Price = (decimal)reader["Price"],
                        PictureUrl = reader["PictureUrl"]?.ToString()
                    };
                    products.Add(product);  // Add each product to the list
                }
            }
            return products;  // Return the list of products
        }

        public Product GetProductById(int id)
        {
            Product product = null; // Variable to hold the product
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand("SELECT * FROM \"Product\" WHERE \"Id\" = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                var reader = command.ExecuteReader();
                if (reader.Read()) 
                {
                    product = new Product
                    {
                        Id = (int)reader["Id"],  
                        Name = reader["Name"]?.ToString(),
                        Type = reader["Type"]?.ToString(),
                        Description = reader["Description"]?.ToString(),
                        Price = (decimal)reader["Price"],
                        PictureUrl = reader["PictureUrl"]?.ToString()
                    };
                }
            }
            return product; // Return the product or null if not found
        }


        public void AddProduct(Product product)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand(
                    "INSERT INTO \"Product\" (\"Name\", \"Type\", \"Description\", \"Price\", \"PictureUrl\") " +
                    "VALUES (@Name, @Type, @Description, @Price, @PictureUrl)", connection);

                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Type", product.Type);
                command.Parameters.AddWithValue("@Description", product.Description);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@PictureUrl", product.PictureUrl);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand(
                    "UPDATE \"Product\" SET \"Name\" = @Name, \"Price\" = @Price, \"Description\" = @Description WHERE \"Id\" = @Id", connection);
                command.Parameters.AddWithValue("@Id", product.Id);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Type", product.Type);
                command.Parameters.AddWithValue("@Description", product.Description);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@PictureUrl", product.PictureUrl);
                command.ExecuteNonQuery();
            }
        }



        public void DeleteProduct(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand("DELETE FROM \"Product\" WHERE \"Id\" = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}
