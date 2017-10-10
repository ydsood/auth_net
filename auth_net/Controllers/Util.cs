using auth_net.Model;

namespace auth_net.Controllers
{
    public class Util
    {
        public static string GenerateJWT(User user)
        {
            return user.UserName;
        }
    }
}
