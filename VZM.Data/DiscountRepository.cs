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
            throw new NotImplementedException();
        }

        public Discount GetDiscount(Guid productId)
        {
            var sql = "Select * FROM [Discount] WHERE [ProductId]= @ProductId";
            var cmd = new SqlCommand(sql, _connection);

            cmd.Parameters.Add("@ProductId", SqlDbType.UniqueIdentifier);
            cmd.Parameters["@ProductId"].Value = productId;

            _connection.Open();
            var reader = cmd.ExecuteReader();
            reader.Read();
            var result = PopulateFromRecord(reader);
            _connection.Close();

            return result;
        }

        public void SaveDiscount(Discount discount)
        {
            throw new NotImplementedException();
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
    }
}
