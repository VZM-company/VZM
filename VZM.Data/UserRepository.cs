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
    public class UserRepository : IUserRepository
    {
        private readonly SqlConnection _connection;

        public UserRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public void DeleteUser(Guid id)
        {
            var sql = "DELETE FROM [User] WHERE UserId = @UserId";
            var cmd = new SqlCommand(sql, _connection);

            cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier);
            cmd.Parameters["@UserId"].Value = id;

            _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }

        public User GetUser(Guid id)
        {
            var sql = "SELECT UserId, Name, Username, PasswordHash, Email, CreatedAt, Info, Confirmed, RoleId FROM [User] WHERE UserId = @UserId";
            var cmd = new SqlCommand(sql, _connection);

            cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier);
            cmd.Parameters["@UserId"].Value = id;

            var result = new User();

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

        public IEnumerable<User> GetUsers()
        {
            var sql = "SELECT UserId, Name, Username, PasswordHash, Email, CreatedAt, Info, Confirmed, RoleId FROM [User]";
            var cmd = new SqlCommand(sql, _connection);

            var result = new List<User>();

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

        public void SaveUser(User user)
        {
            var sql = "";

            if (user.UserId == default)
            {
                sql = "INSERT INTO [User] (UserId, Name, Username, PasswordHash, Email, CreatedAt, Info, Confirmed, RoleId)" +
" VALUES (@UserId, @Name, @Username, @PasswordHash, @Email, @CreatedAt, @Info, @Confirmed, @RoleId)";

                user.UserId = Guid.NewGuid();
            }
            else
            {
                sql = "UPDATE [User] SET Name = @Name, Username = @Username, PasswordHash = @PasswordHash, Email = @Email, CreatedAt = @CreatedAt, Info = @Info, Confirmed = @Confirmed, RoleId = @RoleId WHERE UserId = @UserId";
            }

            var cmd = new SqlCommand(sql, _connection);

            PopulateParametres(cmd, user);

            _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }

        private static void PopulateParametres(SqlCommand cmd, User user)
        {
            cmd.Parameters.Clear();

            cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier);
            cmd.Parameters["@UserId"].Value = user.UserId;

            cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
            cmd.Parameters["@Name"].Value = user.Name;

            cmd.Parameters.Add("@Username", SqlDbType.NVarChar);
            cmd.Parameters["@Username"].Value = user.UserName;

            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar);
            cmd.Parameters["@PasswordHash"].Value = user.PasswordHash;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
            cmd.Parameters["@Email"].Value = user.Email;

            cmd.Parameters.Add("@CreatedAt", SqlDbType.DateTime);
            cmd.Parameters["@CreatedAt"].Value = user.CreatedAt;

            cmd.Parameters.Add("@Info", SqlDbType.NVarChar);
            cmd.Parameters["@Info"].Value = user.Info;

            cmd.Parameters.Add("@Confirmed", SqlDbType.Bit);
            cmd.Parameters["@Confirmed"].Value = user.Confirmed;

            cmd.Parameters.Add("@RoleId", SqlDbType.UniqueIdentifier);
            cmd.Parameters["@RoleId"].Value = user.RoleId is null ? DBNull.Value : user.RoleId;
        }

        private static User PopulateFromRecord(IDataRecord record)
        {
            var user = new User
            {
                UserId = record.GetGuid(0),
                Name = record.GetString(1),
                UserName = record.GetString(2),
                PasswordHash = record.GetString(3),
                Email = record.GetString(4),
                CreatedAt = record.GetDateTime(5),
                Info = record.GetString(6),
                Confirmed = record.GetBoolean(7),
                RoleId = record.GetValue(8) == DBNull.Value ? null : record.GetGuid(9),
            };

            return user;
        }

        public void ChangeRole(Guid userId, string roleName)
        {
            var sql1 = "Select RoleId FROM [Role] WHERE Name = @RoleName";
            var cmd1 = new SqlCommand(sql1, _connection);

            cmd1.Parameters.Add("@RoleName", SqlDbType.NVarChar);
            cmd1.Parameters["@RoleName"].Value = roleName;

            var sql2 = "UPDATE [User] SET RoleId = @RoleId WHERE UserId = @UserId";
            var cmd2 = new SqlCommand(sql2, _connection);

            cmd2.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier);
            cmd2.Parameters["@UserId"].Value = userId;

            _connection.Open();
            var roleId = (Guid)cmd1.ExecuteScalar();

            cmd2.Parameters.Add("@RoleId", SqlDbType.UniqueIdentifier);
            cmd2.Parameters["@RoleId"].Value = roleId;

            cmd2.ExecuteNonQuery();

            _connection.Close();
        }

        public string GetRole(Guid userId)
        {
            var sql = "Select R.[Name] FROM [User] AS U JOIN Role AS R ON U.[RoleId] = R.[RoleId] WHERE U.[UserId]= @UserId";
            var cmd = new SqlCommand(sql, _connection);

            cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier);
            cmd.Parameters["@UserId"].Value = userId;

            _connection.Open();
            var roleName = (string)cmd.ExecuteScalar();
            _connection.Close();

            return roleName;
        }

        public User GetUserByUsername(string userName)
        {
            var sql = "SELECT UserId, Name, Username, PasswordHash, Email, CreatedAt, Info, Confirmed, RoleId FROM [User] WHERE Username = @Username";
            var cmd = new SqlCommand(sql, _connection);

            cmd.Parameters.Add("@Username", SqlDbType.NVarChar);
            cmd.Parameters["@Username"].Value = userName;

            var result = new User();

            _connection.Open();
            var reader = cmd.ExecuteReader(CommandBehavior.SingleResult);
            while (reader.Read())
            {
                result = PopulateFromRecord(reader);
            }

            if (result.UserId == default)
            {
                result = null;
            }

            reader.Close();
            _connection.Close();

            return result;
        }
    }
}
