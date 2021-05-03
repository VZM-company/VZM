using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VZM.Entities;
using VZM.Interfaces;

namespace VZM.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly SqlConnection _connection;

        public ProductRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public void DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProducts()
        {
            throw new NotImplementedException();
        }

        public void SaveProduct(Product product)
        {
            //var sql = "UPDATE product SET product_id = @ProductId, title = @Title, meta_title = @MetaTitle, price = @Price, created_at = @CreatedAt, description = @Description, description_short = @DescriptionShort, cart_id = @CartId, seller_id = @SellerId WHERE product_id = @Id";

            //var sql = "INSERT INTO product (product_id, title, meta_title, price, created_at, description, description_short, cart_id, seller_id)" +
            //    "VALUES (@ProductId, @Title, @MetaTitle, @Price, @CreatedAt, @Description, @DescriptionShort, @CartId, @SellerId)";

            var sql = "INSERT INTO product (product_id, title, meta_title, price, created_at, description, description_short)" +
    " VALUES (@ProductId, @Title, @MetaTitle, @Price, @CreatedAt, @Description, @DescriptionShort)";

            var cmd = new SqlCommand(sql, _connection);

            cmd.Parameters.Add("@ProductId", SqlDbType.Int);
            cmd.Parameters["@ProductId"].Value = 1;

            cmd.Parameters.Add("@Title", SqlDbType.NVarChar);
            cmd.Parameters["@Title"].Value = "Title";

            cmd.Parameters.Add("@MetaTitle", SqlDbType.NVarChar);
            cmd.Parameters["@MetaTitle"].Value = "Meta";

            cmd.Parameters.Add("@Price", SqlDbType.Int);
            cmd.Parameters["@Price"].Value = 1;

            cmd.Parameters.Add("@CreatedAt", SqlDbType.DateTime);
            cmd.Parameters["@CreatedAt"].Value = DateTime.Now;

            cmd.Parameters.Add("@Description", SqlDbType.NVarChar);
            cmd.Parameters["@Description"].Value = "Description";

            cmd.Parameters.Add("@DescriptionShort", SqlDbType.NVarChar);
            cmd.Parameters["@DescriptionShort"].Value = "DescriptionShort";

            //cmd.Parameters.Add("@CartId", SqlDbType.Int);
            //cmd.Parameters["@CartId"].Value = 1;

            //cmd.Parameters.Add("@SellerId", SqlDbType.Int);
            //cmd.Parameters["@SellerId"].Value = 1;

            _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }
    }
}
