using System.Collections.Generic;

namespace POCSelenium.Domain.Interfaces
{
    public interface IUserRepository
    {
        bool Insert(User user);
        IList<User> GetAll();
        IList<User> GetById(int id);
        User GetRolesUser(string email);
        bool Update(User user);
        bool Delete(User user);

    }
}
