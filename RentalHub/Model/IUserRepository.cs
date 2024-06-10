using System.Net;
using System.Security;

namespace RentalHub.Model
{
    public interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);

        void Add(UserModel userModel);
        void Edit(UserModel userModel);
        void Remove(UserModel userModel);

        UserModel GetById(int id);
        UserModel GetByUsername(string username);
        IEnumerable<UserModel> GetByAll();
        //...
    }
}
