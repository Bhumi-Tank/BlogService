using BlogServices.Models;
using BlogServices.Repositories;

namespace BlogServices.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepoitory;
        public UserService(IUserRepository userRepoitory)
        {
            this.userRepoitory = userRepoitory;
        }
        public JwtToken loginRoute(Users user)
        {
            var jwt = userRepoitory.loginRoute(user);
            return jwt;
        }
        public int findUserIdForEmail(string email)
        {
            return userRepoitory.findUserIdForEmail(email);
        }
    }
}
