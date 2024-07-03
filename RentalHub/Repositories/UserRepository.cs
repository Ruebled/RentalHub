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
            string query = "INSERT INTO Users (UserID, Username, PasswordHash, Email, FullName, PhoneNumber, UserType, CreatedAt) " +
                "VALUES (user_id_seq.NEXTVAL, :USERNAME, :PASSWORDHASH, :EMAIL, :FULLNAME, :PHONENUMBER, :USERTYPE, SYSTIMESTAMP);";
            var parameters = new OracleParameter[]
            {
                new OracleParameter("USERNAME", userModel.Username),
                new OracleParameter("PASSWORDHASH", HashPassword(userModel.PasswordHash)),
                new OracleParameter("EMAIL", userModel.Email),
                new OracleParameter("FULLNAME", userModel.FullName),
                new OracleParameter("PHONENUMBER", userModel.PhoneNumber),
                new OracleParameter("USERTYPE", userModel.UserType),
            };
            ExecuteNonQuery(query, parameters);
        }

        public UserModel AuthenticateUser(NetworkCredential credential)
        {
            string query = "SELECT * FROM USERS WHERE USERNAME = :USERNAME AND PASSWORDHASH = :PASSWORDHASH";
            var parameters = new OracleParameter[]
            {
                new OracleParameter("USERNAME", credential.UserName),
                new OracleParameter("PASSWORDHASH", HashPassword(credential.Password))
            };

            var result = ExecuteQuery(query, reader => new UserModel
            {
                UserId = reader["USERID"]?.ToString(),
                Username = reader["USERNAME"]?.ToString(),
                PasswordHash = reader["PASSWORDHASH"]?.ToString(),
                Email = reader["EMAIL"]?.ToString(),
                FullName = reader["FULLNAME"]?.ToString(),
                PhoneNumber = reader["PHONENUMBER"]?.ToString(),
                UserType = reader["USERTYPE"]?.ToString(),
                ImageID = reader["PROFILEIMAGEID"]?.ToString(),
                CreateDate = reader["CREATEDAT"]?.ToString()
            }, parameters);

            return result.FirstOrDefault();
        }

        private static string HashPassword(string input)
        {
            using SHA256 sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }

        public UserModel? GetByUsername(string username)
        {
            // Define the SQL query
            string query = "SELECT * FROM USERS WHERE USERNAME = :USERNAME";

            // Create parameters for the query
            var parameters = new OracleParameter[]
            {
                new OracleParameter("USERNAME", username)
            };

            // Execute the query and map the results to a UserModel object
            var result = ExecuteQuery(query, reader => new UserModel
            {
                UserId = reader["USERID"].ToString(),
                Username = reader["USERNAME"].ToString(),
                PasswordHash = reader["PASSWORDHASH"].ToString(),
                Email = reader["EMAIL"].ToString(),
                FullName = reader["FULLNAME"].ToString(),
                PhoneNumber = reader["PHONENUMBER"].ToString(),
                UserType = reader["USERTYPE"].ToString(),
                ImageID = reader["PROFILEIMAGEID"].ToString(),
                CreateDate = reader["CREATEDAT"].ToString()
            }, parameters);

            // Return the first result or null if no results were found
            return result.FirstOrDefault();
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


        public void Remove(UserModel userModel)
        {
            throw new NotImplementedException();
        }
    }
}
