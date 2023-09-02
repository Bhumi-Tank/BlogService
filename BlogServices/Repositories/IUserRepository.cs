using BlogServices.Models;

namespace BlogServices.Repositories
{
    public interface IUserRepository
    {
        public JwtToken loginRoute(Users user);
        public int findUserIdForEmail(string email);
        public JwtToken createTocken(Users user, int userId);
    }
}
