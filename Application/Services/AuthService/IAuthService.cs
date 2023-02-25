using Core.Security.Entities;
using Core.Security.JWT;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AuthService
{
    public interface IAuthService
    {
        public Task<AccessToken> CreateAccessToken(Owner owner);
        public bool ConfirmPasswords(Owner owner, string password);
        public Task<Owner> GetOwnerById(int ownerId);
    }
}
