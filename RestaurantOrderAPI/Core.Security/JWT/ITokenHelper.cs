using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Core.Security.JWT

{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, IList<OperationClaim> operationClaims);

        RefreshToken CreateRefreshToken(User user, string ipAdress);
        bool IsTokenExpired(AccessToken accessToken);
        string CreateEmailVerificationToken(User user);
        public ClaimsPrincipal ValidateEmailVerificationToken(string token, out SecurityToken securityToken);
    }
}
