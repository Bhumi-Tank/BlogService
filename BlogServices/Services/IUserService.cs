using BlogServices.Models;

namespace BlogServices.Services
{
    public interface IUserService
    {
        public JwtToken loginRoute(Users user);
        public int findUserIdForEmail(string email);
    }
}
