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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SqlConnection _connection;

        public CategoryRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Category> GetCategories()
        {
            throw new NotImplementedException();
        }

        public Category GetCategory(Guid id)
        {
            throw new NotImplementedException();
        }

        public Category GetCategoryByName(string name)
        {
            var sql = "SELECT CategoryId, Title FROM Category WHERE Title = @Title";
            var cmd = new SqlCommand(sql, _connection);

            cmd.Parameters.Add("@Title", SqlDbType.NVarChar);
            cmd.Parameters["@Title"].Value = name;

            Category result = null;

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

        private static Category PopulateFromRecord(IDataRecord record)
        {
            var product = new Category
            {
                CategoryId = record.GetGuid(0),
                Title = record.GetString(1),
            };

            return product;
        }
    }
}
