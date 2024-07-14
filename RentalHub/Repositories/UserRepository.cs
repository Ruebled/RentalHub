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
            string query = "INSERT INTO Users (UserID, FirstName, LastName, Username, PasswordHash, Email, PhoneNumber, UserType, ProfileImageId, CreatedAt) " +
                "VALUES (user_id_seq.NEXTVAL, :FIRSTNAME, :LASTNAME, :USERNAME, :PASSWORDHASH, :EMAIL, :PHONENUMBER, :USERTYPE, :PROFILEIMAGEID, SYSTIMESTAMP)";

            var parameters = new OracleParameter[]
            {
                new OracleParameter("FIRSTNAME", userModel.FirstName),
                new OracleParameter("LASTNAME", userModel.LastName),
                new OracleParameter("USERNAME", userModel.Username),
                new OracleParameter("PASSWORDHASH", userModel.PasswordHash),
                new OracleParameter("EMAIL", userModel.Email),
                new OracleParameter("PHONENUMBER", userModel.PhoneNumber),
                new OracleParameter("USERTYPE", userModel.UserType),
                new OracleParameter("PROFILEIMAGEID", userModel.ImageID),
            };

            try
            {
                ExecuteNonQuery(query, parameters);
            }
            catch (OracleException ex)
            {
                // Handle Oracle specific exceptions
                Console.WriteLine($"Oracle Exception: {ex.Message}");
                throw; // Rethrow the exception or handle as appropriate
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"Exception: {ex.Message}");
                throw; // Rethrow the exception or handle as appropriate
            }
        }

        public void Update(UserModel userModel)
        {
            // Check for null or empty values in mandatory fields (except PasswordHash)
            if (string.IsNullOrEmpty(userModel.FirstName))
                throw new ArgumentException("FirstName cannot be null or empty.");
            if (string.IsNullOrEmpty(userModel.LastName))
                throw new ArgumentException("LastName cannot be null or empty.");
            if (string.IsNullOrEmpty(userModel.Email))
                throw new ArgumentException("Email cannot be null or empty.");
            if (string.IsNullOrEmpty(userModel.PhoneNumber))
                throw new ArgumentException("PhoneNumber cannot be null or empty.");
            if (string.IsNullOrEmpty(userModel.UserType))
                throw new ArgumentException("UserType cannot be null or empty.");
            if (string.IsNullOrEmpty(userModel.ImageID))
                throw new ArgumentException("ProfileImageId cannot be null or empty.");

            var setClauses = new List<string>();
            var parameters = new List<OracleParameter>();

            setClauses.Add("FirstName = :FIRSTNAME");
            parameters.Add(new OracleParameter("FIRSTNAME", userModel.FirstName));

            setClauses.Add("LastName = :LASTNAME");
            parameters.Add(new OracleParameter("LASTNAME", userModel.LastName));

            setClauses.Add("Email = :EMAIL");
            parameters.Add(new OracleParameter("EMAIL", userModel.Email));

            setClauses.Add("PhoneNumber = :PHONENUMBER");
            parameters.Add(new OracleParameter("PHONENUMBER", userModel.PhoneNumber));

            setClauses.Add("UserType = :USERTYPE");
            parameters.Add(new OracleParameter("USERTYPE", userModel.UserType));

            setClauses.Add("ProfileImageId = :PROFILEIMAGEID");
            parameters.Add(new OracleParameter("PROFILEIMAGEID", userModel.ImageID));

            // Conditionally add PasswordHash if it's not null or empty
            if (!string.IsNullOrEmpty(userModel.PasswordHash))
            {
                setClauses.Add("PasswordHash = :PASSWORDHASH");
                parameters.Add(new OracleParameter("PASSWORDHASH", userModel.PasswordHash));
            }

            string setClause = string.Join(", ", setClauses);
            string query = $"UPDATE Users SET {setClause} WHERE UserID = :USERID";

            parameters.Add(new OracleParameter("USERID", userModel.UserId)); // Assuming UserID is the primary key

            try
            {
                ExecuteNonQuery(query, parameters.ToArray());
            }
            catch (OracleException ex)
            {
                // Handle Oracle specific exceptions
                Console.WriteLine($"Oracle Exception: {ex.Message}");
                throw; // Rethrow the exception or handle as appropriate
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"Exception: {ex.Message}");
                throw; // Rethrow the exception or handle as appropriate
            }
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
                FirstName = reader["FIRSTNAME"]?.ToString(),
                LastName = reader["LASTNAME"]?.ToString(),
                Username = reader["USERNAME"]?.ToString(),
                PasswordHash = reader["PASSWORDHASH"]?.ToString(),
                Email = reader["EMAIL"]?.ToString(),
                PhoneNumber = reader["PHONENUMBER"]?.ToString(),
                UserType = reader["USERTYPE"]?.ToString(),
                ImageID = reader["PROFILEIMAGEID"]?.ToString(),
                CreateDate = reader["CREATEDAT"]?.ToString()
            }, parameters);

            return result.FirstOrDefault();
        }

        public string HashPassword(string input)
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
                FirstName = reader["FIRSTNAME"].ToString(),
                LastName = reader["LASTNAME"].ToString(),
                Username = reader["USERNAME"].ToString(),
                PasswordHash = reader["PASSWORDHASH"].ToString(),
                Email = reader["EMAIL"].ToString(),
                PhoneNumber = reader["PHONENUMBER"].ToString(),
                UserType = reader["USERTYPE"].ToString(),
                ImageID = reader["PROFILEIMAGEID"].ToString(),
                CreateDate = reader["CREATEDAT"].ToString()
            }, parameters);

            // Return the first result or null if no results were found
            return result.FirstOrDefault();
        }

        public UserModel? GetByEmail(string email)
        {
            string query = "SELECT USERID, EMAIL FROM USERS WHERE EMAIL = :EMAIL";
            var parameters = new OracleParameter[]
            {
                new OracleParameter("EMAIL", email)
            };

            var result = ExecuteQuery(query, reader => new UserModel
            {
                UserId = reader["USERID"].ToString(),
                Email = reader["EMAIL"].ToString()
            }, parameters);

            return result.FirstOrDefault();
        }

        public List<string> GetAllUsernames()
        {
            // Define the SQL query
            string query = "SELECT USERNAME FROM USERS";

            // Execute the query and retrieve usernames
            var usernames = ExecuteQuery(query, reader => reader["USERNAME"].ToString());

            return usernames.ToList();
        }


        public void UpdateUserPassword(string userId, string newPassword)
        {
            string query = "UPDATE USERS SET PASSWORDHASH = :PASSWORDHASH WHERE USERID = :USERID";
            var parameters = new OracleParameter[]
            {
                new OracleParameter("PASSWORDHASH", HashPassword(newPassword)),
                new OracleParameter("USERID", userId)
            };

            ExecuteNonQuery(query, parameters);
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


        public void Remove(UserModel User)
        {
            string query = "DELETE FROM Users WHERE UserId = :USERID";

            var parameters = new OracleParameter[]
            {
                new OracleParameter("USERID", User.UserId)
            };

            ExecuteNonQuery(query, parameters);
        }
    }
}
