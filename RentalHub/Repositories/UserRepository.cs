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
                    command.CommandText = "SELECT COUNT(*) FROM USERS WHERE USERNAME = :USERNAME AND PASSWORD = :PASSWORD";

                    // Add parameters to the command
                    command.Parameters.Add(new OracleParameter("USERNAME", credential.UserName));
                    command.Parameters.Add(new OracleParameter("PASSWORD", credential.Password));

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

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
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
            throw new NotImplementedException();
        }

        public void Remove(UserModel userModel)
        {
            throw new NotImplementedException();
        }
    }
}
