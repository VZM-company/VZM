using Microsoft.Data.SqlClient;
using System.Data;
using VZM.Entities;
using VZM.Interfaces;

namespace VZM.Data
{
    public class RoleRepository : IRoleRepository
    {
        private readonly SqlConnection _connection;

        public RoleRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public Role GetRoleByTitle(string title)
        {
            var sql = "Select * FROM [Role] WHERE [Name]= @Name";
            var cmd = new SqlCommand(sql, _connection);

            cmd.Parameters.Add("@Name", SqlDbType.VarChar);
            cmd.Parameters["@Name"].Value = title;

            _connection.Open();
            var reader = cmd.ExecuteReader();
            reader.Read();

            var role = PopulateFromRecord(reader);
            _connection.Close();

            return role;
        }

        private static Role PopulateFromRecord(IDataRecord record)
        {
            var role = new Role
            {
                RoleId = record.GetGuid(0),
                Name = record.GetString(1),
                NameShort = record.GetString(2)
            };

            return role;
        }
    }
}
