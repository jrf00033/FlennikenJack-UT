using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using FlennikenJack_UT.Models;
using System.Data;

namespace FlennikenJack_UT.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly string connectionString;

        public ProductRepository(IConfiguration config) //bringing in the capability to talk to the configuration files
        {

            connectionString = config.GetConnectionString("DefaultConnection"); //store database info in connectionString variable

        }

        public List<Product> GetAllProducts()
        {

            var products = new List<Product>();

            using (var connection = new SqlConnection(connectionString))
            using (var sqlcommand = new SqlCommand("GetAllProducts", connection))

            {

                sqlcommand.CommandType = CommandType.StoredProcedure; //telling it that the command type will be a stored procedure
                connection.Open();

                //run the command
                using (var reader = sqlcommand.ExecuteReader())
                {
                    while (reader.Read()) //checking if there are more records to read, and looping
                    {
                        products.Add(new Product //adding a new product
                        {
                            ProductID = reader.GetInt32(0), //Look at column 0 and in SQL and converting it into an integer
                            ProductName = reader.GetString(1),
                            Category = reader.GetString(2),
                            Price = reader.GetDecimal(3),
                            StockQuantity = reader.GetInt32(4),                     

                        });
                    }


                }
                connection.Close();

            }
            return products;

        }

        public void AddProduct(Product product) 
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@ProductName",SqlDbType.NVarChar) {Value = product.ProductName}, //creating a paramater and what type it is set to
                
                new SqlParameter("@Category", SqlDbType.NVarChar) {Value = product.Category},

                new SqlParameter("@Price", SqlDbType.Money) {Value = product.Price}, 
                //creating a parameter, matching it a procedure in sql and saying what value it matches in the tabel
                
                new SqlParameter("@StockQuantity", SqlDbType.Int) {Value = product.StockQuantity},

                new SqlParameter("@ProductID", SqlDbType.Int) {Direction = ParameterDirection.Output} //Lets the compiler know that it will equal an output
            };

            using (var connection = new SqlConnection(connectionString)) //opening the connection
            {
                connection.Open();

                using (var command = new SqlCommand("InsertProduct", connection))
                //using the stored procedure in SQL
                {
                    command.CommandType = CommandType.StoredProcedure; //saying what command it is
                    command.Parameters.AddRange(parameters); //adding a range of values in the paramters variable


                    command.ExecuteScalar(); //if a command is gonna bring you back a number use executescalar
                }



            }
        }

    }
}
