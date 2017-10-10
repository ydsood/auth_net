using auth_net.Model;
using auth_net.DAO.Common;

namespace auth_net.Controllers
{
    public class Util
    {
        public static string GenerateJWT(User user)
        {
            return user.UserName;
        }

        public static bool PasswordsMatch(User retrievedUser, string password)
        {
            return MongoHelper<User>.PasswordsMatch(retrievedUser.Password, password);
        }
    }
}
