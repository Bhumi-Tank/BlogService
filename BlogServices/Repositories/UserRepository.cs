using System;
using BlogServices.Data;
using BlogServices.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace BlogServices.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BlogServicesContext context;
        public UserRepository(BlogServicesContext context) {  this.context = context; }
        public JwtToken loginRoute(Users user)
        {
            //var userid = findUserIdForEmail(user.UserEmail);
            var userid = 1;
            var jwt = createTocken(user, userid);
            return jwt;
        }

        public int findUserIdForEmail(string email)
        {
            var user = context.Users.Where(s => s.UserEmail == email).FirstOrDefault();
            return user.Id;
        }

        public JwtToken createTocken(Users user, int userId)
        {
            var privateKey = Convert.FromBase64String("MIICXAIBAAKBgQCJHYcuRHdJ5S8GYcz/IgF5hJY+vvchVaeKyD+GGSoiD58pRJ3kx5b7YbbP/EyzwhUxWncvbsiWZFdqca/DHsFKxNnRgvidoyq2dgA+erP91aFHpccH2ykNxC6LTzMXX4XWp5mXKm6XfMkBFzsVC4/a7A6UHnsL7MU2b4lec+WkSwIDAQABAoGAMV1zLOIzfGRKAOc3MefhVgm5Og/w04yODHY6AKKQu8CaEfaFTjfZkNnGQq1YRCOtE565aFdfWl335vfVSs+I0UTKYtUdU0DkeZ93nB+eaUIQ/7UC99UlcdSrlRfXGwBxdcwM+Ek93VeITWERydh+xyXN3VxzaYtApA1fB/YGnzkCQQDcb8X6xFigw0qSpHXX+qpBQcTSbIQ5u47vK0VRIkDI3vN3tGBeIpU78kK3E+cHG8e74Nobn1/nJ09TKS4jzoudAkEAnzyDQPD269xzwTvICVMDvezEN4sy3+XJdJjtOdtL4RZKRqOUiaJmKhR1QUtlrG71LqudUiwZ7DnzloKyvLYvBwJAXCyw0GcB2FdQ+3ihfipmvtrNfl+5+poe7otddMup41S24bsfAL3dQS/QDdXYqPRI1Jr1GM/PvkyFsvRpQre/UQJBAIavIDVlmvSUWjQu5Fs+pAOYp75zNmy6Z1L/pmcxXVTdDaYB5jkj61XcR/EaXL0kfK0k6sP+GU79FVNQ6O1FCzECQAD8SLqaJOGbiPYrf+gRx337xQTatlaaXIaRrtNKj4E3/WtyKQQXEILCMDS1Xa88sK12nQgu6DVxOtIs+7cEvds=");
            using RSA rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(privateKey, out _);
            var signingCredential = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);

            var nowUtc = DateTime.UtcNow;
            var expires = nowUtc.AddSeconds(10);
            var centuryBegin = new DateTime(1970, 1, 1);
            var expireyTime = (long)new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalSeconds;

            var jwtClaims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserEmail)
            };

            var jwtHeader = new JwtHeader(signingCredential);
            var jwtPayload = new JwtPayload(jwtClaims);

            var jwt = new JwtSecurityToken(jwtHeader, jwtPayload);
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtToken
            {
                Token = token,
                Expires = expireyTime
            };
        }
    }
}
