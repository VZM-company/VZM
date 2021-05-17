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
    public class CartRepository : ICartRepository
    {
        private readonly SqlConnection _connection;

        public CartRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public void Add(Guid productId, Guid userId)
        {
            var sql0 = "SELECT COUNT(*) FROM Cart WHERE UserId=@UserId AND ProductId=@ProductId";
            var cmd0 = new SqlCommand(sql0, _connection);

            var sql = "INSERT INTO Cart(UserId, ProductId) VALUES(@UserId, @ProductId)";
            var cmd = new SqlCommand(sql, _connection);

            cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier);
            cmd.Parameters["@UserId"].Value = userId;

            cmd.Parameters.Add("@ProductId", SqlDbType.UniqueIdentifier);
            cmd.Parameters["@ProductId"].Value = productId;

            cmd0.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier);
            cmd0.Parameters["@UserId"].Value = userId;

            cmd0.Parameters.Add("@ProductId", SqlDbType.UniqueIdentifier);
            cmd0.Parameters["@ProductId"].Value = productId;

            _connection.Open();
            if ((int)cmd0.ExecuteScalar() == 0)
            {
                cmd.ExecuteNonQuery();
            }
            else
            {
                _connection.Close();
                throw new ArgumentException();
            }

            _connection.Close();
        }

        public void DeleteProduct(Guid productId, Guid userId)
        {
            var sql = "DELETE FROM Cart WHERE UserId=@UserId and ProductId=@ProductId";

            var cmd = new SqlCommand(sql, _connection);

            cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier);
            cmd.Parameters["@UserId"].Value = userId;

            cmd.Parameters.Add("@ProductId", SqlDbType.UniqueIdentifier);
            cmd.Parameters["@ProductId"].Value = productId;

            _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }

        public void DeleteAll(Guid userId)
        {
            var sql = "DELETE FROM Cart WHERE UserId=@UserId";

            var cmd = new SqlCommand(sql, _connection);

            cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier);
            cmd.Parameters["@UserId"].Value = userId;

            _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }

        public IEnumerable<Product> GetItems(Guid userId)
        {
            var sql = "SELECT P.ProductId, P.Title, P.MetaTitle, P.Price, P.CreatedAt, P.[Description], P.DescriptionShort, P.ImageUrl, P.SellerId" +
" FROM [Cart] as C JOIN [Product] as P ON C.ProductId = P.ProductId WHERE UserId=@UserId";
            var cmd = new SqlCommand(sql, _connection);

            cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier);
            cmd.Parameters["@UserId"].Value = userId;

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

        public void Purchase(Guid userId)
        {
            var sql = "INSERT INTO UserProduct(UserId, ProductId) SELECT UserId, ProductId FROM Cart WHERE UserId=@UserId";

            var cmd = new SqlCommand(sql, _connection);

            cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier);
            cmd.Parameters["@UserId"].Value = userId;

            _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();

            DeleteAll(userId);
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
                ImageUrl = record.GetValue(7) == DBNull.Value ? null : record.GetString(7),
                SellerId = record.GetValue(8) == DBNull.Value ? null : record.GetGuid(8),
            };

            return product;
        }
    }
}
