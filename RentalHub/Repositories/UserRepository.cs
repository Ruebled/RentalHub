using Oracle.ManagedDataAccess.Client;

using RentalHub.Model;

using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace RentalHub.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;

            try
            {
                using (var connection = GetConnection())
                using (var command = new OracleCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT COUNT(*) FROM USERS WHERE USERNAME = :USERNAME AND PASSWORDHASH = :PASSWORDHASH";

                    // Add parameters to the command
                    command.Parameters.Add(new OracleParameter("USERNAME", credential.UserName));
                    command.Parameters.Add(new OracleParameter("PASSWORDHASH", HashPassword(credential.Password)));

                    // Execute the query
                    int userCount = Convert.ToInt32(command.ExecuteScalar());

                    // Check if user exists
                    validUser = userCount > 0;
                }
            }
            catch (Exception ex)
            {
                validUser = false;
            }

            return validUser;
        }

        private string HashPassword(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); // "x2" converts byte to hexadecimal string
                }
                return builder.ToString();
            }
        }


        public void Edit(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public UserModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public UserModel GetByUsername(string username)
        {
            UserModel user = new UserModel();

            try
            {
                using (var connection = GetConnection())
                using (var command = new OracleCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM USERS WHERE USERNAME=:USERNAME";

                    // Add parameters to the command
                    command.Parameters.Add(new OracleParameter("USERNAME", username));

                    // Execute the query
                    using var reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        user = new UserModel()
                        {
                            Id = reader[0].ToString(),
                            Username = reader[1].ToString(),
                            Password = string.Empty,
                            Name = reader[3].ToString(),
                            LastName = reader[4].ToString(),
                            Email = reader[5].ToString(),
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                //
            }
            return user;
        }

        public void Remove(UserModel userModel)
        {
            throw new NotImplementedException();
        }
    }
}
