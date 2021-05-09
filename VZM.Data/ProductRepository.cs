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

        public void DeleteProduct(Guid id)
        {
            var sql = "DELETE FROM Product WHERE ProductId = @ProductId";
            var cmd = new SqlCommand(sql, _connection);

            cmd.Parameters.Add("@ProductId", SqlDbType.UniqueIdentifier);
            cmd.Parameters["@ProductId"].Value = id;

            _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }

        public Product GetProduct(Guid id)
        {
            var sql = "SELECT ProductId, Title, MetaTitle, Price, CreatedAt, Description, DescriptionShort, ImageUrl, SellerId FROM Product WHERE ProductId = @ProductId";
            var cmd = new SqlCommand(sql, _connection);

            cmd.Parameters.Add("@ProductId", SqlDbType.UniqueIdentifier);
            cmd.Parameters["@ProductId"].Value = id;

            var result = new Product();

            _connection.Open();
            var reader = cmd.ExecuteReader(CommandBehavior.SingleResult);
            while (reader.Read())
            {
                result = PopulateFromRecord(reader);
            }

            reader.Close();
            _connection.Close();

            return result;
        }

        public IEnumerable<Product> GetProducts()
        {
            var sql = "SELECT ProductId, Title, MetaTitle, Price, CreatedAt, Description, DescriptionShort, ImageUrl, SellerId FROM Product";
            var cmd = new SqlCommand(sql, _connection);

            var result = new List<Product>();

            _connection.Open();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(PopulateFromRecord(reader));
            }

            reader.Close();
            _connection.Close();

            return result;
        }

        public void SaveProduct(Product product)
        {
            var sql = "";

            if (product.ProductId == default)
            {
                sql = "INSERT INTO Product (ProductId, Title, MetaTitle, Price, CreatedAt, Description, DescriptionShort, ImageUrl, SellerId)" +
" VALUES (@ProductId, @Title, @MetaTitle, @Price, @CreatedAt, @Description, @DescriptionShort, @ImageUrl, @SellerId)";

                product.ProductId = Guid.NewGuid();
            }
            else
            {
                sql = "UPDATE Product SET Title = @Title, MetaTitle = @MetaTitle, Price = @Price, CreatedAt = @CreatedAt, Description = @Description, DescriptionShort = @DescriptionShort, ImageUrl = @ImageUrl WHERE ProductId = @ProductId";
            }

            var cmd = new SqlCommand(sql, _connection);

            PopulateParametres(cmd, product);

            _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }

        private static void PopulateParametres(SqlCommand cmd, Product product)
        {
            cmd.Parameters.Clear();

            cmd.Parameters.Add("@ProductId", SqlDbType.UniqueIdentifier);
            cmd.Parameters["@ProductId"].Value = product.ProductId;

            cmd.Parameters.Add("@Title", SqlDbType.NVarChar);
            cmd.Parameters["@Title"].Value = product.Title;

            cmd.Parameters.Add("@MetaTitle", SqlDbType.NVarChar);
            cmd.Parameters["@MetaTitle"].Value = product.MetaTitle;

            cmd.Parameters.Add("@Price", SqlDbType.Int);
            cmd.Parameters["@Price"].Value = product.Price;

            cmd.Parameters.Add("@CreatedAt", SqlDbType.DateTime);
            cmd.Parameters["@CreatedAt"].Value = product.CreatedAt;

            cmd.Parameters.Add("@Description", SqlDbType.NVarChar);
            cmd.Parameters["@Description"].Value = product.Description;

            cmd.Parameters.Add("@DescriptionShort", SqlDbType.NVarChar);
            cmd.Parameters["@DescriptionShort"].Value = product.DescriptionShort;

            cmd.Parameters.Add("@ImageUrl", SqlDbType.NVarChar);
            cmd.Parameters["@ImageUrl"].Value = product.ImageUrl;

            cmd.Parameters.Add("@SellerId", SqlDbType.UniqueIdentifier);
            cmd.Parameters["@SellerId"].Value = product.SellerId  is null ? DBNull.Value : product.SellerId;
        }

        private static Product PopulateFromRecord(IDataRecord record)
        {
            var product = new Product
            {
                ProductId = record.GetGuid(0),
                Title = record.GetString(1),
                MetaTitle = record.GetString(2),
                Price = record.GetDouble(3),
                CreatedAt = record.GetDateTime(4),
                Description = record.GetString(5),
                DescriptionShort = record.GetString(6),
                ImageUrl = record.GetString(7),
                SellerId = record.GetValue(8) == DBNull.Value ? null : record.GetGuid(8),
            };

            return product;
        }

        public IEnumerable<Product> GetProductsByName(string name)
        {
            var sql = "SELECT ProductId, Title, MetaTitle, Price, CreatedAt, Description, DescriptionShort, ImageUrl, SellerId FROM Product Where Title = @Title";
            var cmd = new SqlCommand(sql, _connection);

            cmd.Parameters.Add("@Title", SqlDbType.NVarChar);
            cmd.Parameters["@Title"].Value = name;

            var result = new List<Product>();

            _connection.Open();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(PopulateFromRecord(reader));
            }

            reader.Close();
            _connection.Close();

            return result;
        }

        public IEnumerable<Product> GetProductsByPrice(double startPrice, double endPrice)
        {
            var sql = "SELECT ProductId, Title, MetaTitle, Price, CreatedAt, Description, DescriptionShort, ImageUrl, SellerId FROM Product WHERE Price BETWEEN @Start AND @End";
            var cmd = new SqlCommand(sql, _connection);

            cmd.Parameters.Add("@Start", SqlDbType.Float);
            cmd.Parameters["@Start"].Value = startPrice;

            cmd.Parameters.Add("@End", SqlDbType.Float);
            cmd.Parameters["@End"].Value = endPrice;

            var result = new List<Product>();

            _connection.Open();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(PopulateFromRecord(reader));
            }

            reader.Close();
            _connection.Close();

            return result;
        }

        public IEnumerable<Product> GetProductsByCategory(Category category)
        {
            var sql = "Select P.ProductId, P.Title, P.MetaTitle, P.Price, P.CreatedAt, P.[Description], P.DescriptionShort, P.ImageUrl, P.SellerId" +
                " FROM ProductCategory AS PC JOIN Product AS P ON PC.[ProductId] = P.[ProductId] WHERE PC.[CategoryId]= @CategoryId";
            var cmd = new SqlCommand(sql, _connection);

            cmd.Parameters.Add("@CategoryId", SqlDbType.UniqueIdentifier);
            cmd.Parameters["@CategoryId"].Value = category.CategoryId;

            var result = new List<Product>();

            _connection.Open();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(PopulateFromRecord(reader));
            }

            reader.Close();
            _connection.Close();

            return result;
        }

        public IEnumerable<Product> GetProductsByUser(User user)
        {
            var sql = "Select P.ProductId, P.Title, P.MetaTitle, P.Price, P.CreatedAt, P.[Description], P.DescriptionShort, P.ImageUrl, P.SellerId" +
                " FROM UserProduct AS UP JOIN Product AS P ON UP.[ProductId] = P.[ProductId] WHERE UP.[UserId]= @UserId";
            var cmd = new SqlCommand(sql, _connection);

            cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier);
            cmd.Parameters["@UserId"].Value = user.UserId;

            var result = new List<Product>();

            _connection.Open();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(PopulateFromRecord(reader));
            }

            reader.Close();
            _connection.Close();

            return result;
        }

        public IEnumerable<Product> GetProductsBySeller(User user)
        {
            var sql = "SELECT ProductId, Title, MetaTitle, Price, CreatedAt, Description, DescriptionShort, ImageUrl, SellerId FROM Product Where SellerId = @SellerId";
            var cmd = new SqlCommand(sql, _connection);

            cmd.Parameters.Add("@SellerId", SqlDbType.UniqueIdentifier);
            cmd.Parameters["@SellerId"].Value = user.UserId;

            var result = new List<Product>();

            _connection.Open();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(PopulateFromRecord(reader));
            }

            reader.Close();
            _connection.Close();

            return result;
        }
    }
}
