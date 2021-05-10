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
    public class DiscountRepository : IDiscountRepository
    {
        private readonly SqlConnection _connection;

        public DiscountRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public void DeleteDiscount(Guid discountId)
        {
            var sql = "DELETE FROM Discount WHERE DiscountId = @DiscountId";
            var cmd = new SqlCommand(sql, _connection);

            cmd.Parameters.Add("@DiscountId", SqlDbType.UniqueIdentifier);
            cmd.Parameters["@DiscountId"].Value = discountId;

            _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }

        public Discount GetDiscount(Guid productId)
        {
            var sql = "Select * FROM [Discount] WHERE [ProductId]= @ProductId";
            var cmd = new SqlCommand(sql, _connection);

            cmd.Parameters.Add("@ProductId", SqlDbType.UniqueIdentifier);
            cmd.Parameters["@ProductId"].Value = productId;

            var result = new Discount();

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

        public void SaveDiscount(Discount discount)
        {
            var sql = "";

            if (discount.DiscountId == default)
            {
                sql = "INSERT INTO Discount (DiscountId, Value, CreatedAt, ExpiredAt, ProductId)" +
                      "VALUES (@DiscountId, @Value, @CreatedAt, @ExpiredAt, @ProductId)";

                discount.DiscountId = Guid.NewGuid();
            }
            else
            {
                sql = "UPDATE Product SET Value = @Value, CreatedAt = @CreatedAt, ExpiredAt = @ExpiredAt, ProductId = @ProductID WHERE DiscountId = @DiscountId";
            }

            var cmd = new SqlCommand(sql, _connection);

            PopulateParametres(cmd, discount);

            _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }

        private static Discount PopulateFromRecord(IDataRecord record)
        {
            var discount = new Discount
            {
                DiscountId = record.GetGuid(0),
                Value = record.GetFloat(1),
                CreatedAt = record.GetDateTime(2),
                ExpiredAt = record.GetDateTime(3),
                ProductId = record.GetGuid(4),
            };

            return discount;
        }

        private static void PopulateParametres(SqlCommand cmd, Discount discount)
        {
            cmd.Parameters.Clear();

            cmd.Parameters.Add("@DiscountId", SqlDbType.UniqueIdentifier);
            cmd.Parameters["@DiscountId"].Value = discount.DiscountId;

            cmd.Parameters.Add("@Value", SqlDbType.Float);
            cmd.Parameters["@Value"].Value = discount.Value;

            cmd.Parameters.Add("@CreatedAt", SqlDbType.DateTime);
            cmd.Parameters["@CreatedAt"].Value = discount.CreatedAt;

            cmd.Parameters.Add("@ExpiredAt", SqlDbType.DateTime);
            cmd.Parameters["@ExpiredAt"].Value = discount.ExpiredAt;

            cmd.Parameters.Add("@ProductId", SqlDbType.UniqueIdentifier);
            cmd.Parameters["@ProductId"].Value = discount.ProductId;
        }
    }
}
