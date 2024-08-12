using Npgsql;
using RandomUsers.Web.Models;

namespace RandomUsers.Web.Data
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<UserModel>> GetUsersAsync()
        {
            var users = new List<UserModel>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new NpgsqlCommand("SELECT uuid, email, name_first, name_last, gender, cell FROM public.\"User\"", connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        users.Add(new UserModel
                        {
                            Gender = reader.GetString(reader.GetOrdinal("gender")),
                            Name = new Name
                            {
                                First = reader.GetString(reader.GetOrdinal("name_first")),
                                Last = reader.GetString(reader.GetOrdinal("name_last"))
                            },
                            Email = reader.GetString(reader.GetOrdinal("email")),
                            Login = new Login
                            {
                                Uuid = reader.GetString(reader.GetOrdinal("uuid"))
                            },
                            Cell = reader.GetString(reader.GetOrdinal("cell"))
                        });
                    }
                }
            }

            return users;
        }

        public async Task InsertUserAsync(UserModel user)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = @"
                    INSERT INTO public.""User"" (uuid, email, name_first, name_last, gender, cell) VALUES (@uuid, @email, @name_first, @name_last, @gender, @cell)";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("uuid", user.Login.Uuid);
                    command.Parameters.AddWithValue("email", user.Email);
                    command.Parameters.AddWithValue("name_first", user.Name.First);
                    command.Parameters.AddWithValue("name_last", user.Name.Last);
                    command.Parameters.AddWithValue("gender", user.Gender);
                    command.Parameters.AddWithValue("cell", user.Cell);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
