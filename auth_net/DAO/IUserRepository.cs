using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using auth_net.Model;

namespace auth_net.DAO
{
    interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();

        User GetUser(string id);

        User AddUser(User u);

        bool RemoveUser(String id);

        bool UpdateUser(String id, User u);

    }
}
