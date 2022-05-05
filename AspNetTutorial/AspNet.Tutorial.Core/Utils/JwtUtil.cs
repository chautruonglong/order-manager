using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

using AspNet.Tutorial.Core.Exceptions;

namespace AspNet.Tutorial.WebApi.Utils
{
    public class JwtUtil
    {
        public static Guid GetGuid(string token)
        {
            token = token.Substring("Bearer ".Length);
            var securityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var id = securityToken.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            if (id == null)
            {
                throw new ClaimException("User id is not include in claims");
            }
            
            return Guid.Parse(id);
        }
    }
}
