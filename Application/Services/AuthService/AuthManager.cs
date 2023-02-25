using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AuthService
{
    public class AuthManager : IAuthService
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IAuthRepository _authRepository;

        public AuthManager(ITokenHelper tokenHelper, IUserOperationClaimRepository userOperationClaimRepository, IAuthRepository authRepository)
        {
            _tokenHelper = tokenHelper;
            _userOperationClaimRepository = userOperationClaimRepository;
            _authRepository = authRepository;
        }

        public bool ConfirmPasswords(Owner owner, string password)
        {
            return HashingHelper.VerifyPasswordHash(password, owner.PasswordHash, owner.PasswordSalt) ? true : throw new BusinessException("Password is not correct.");
        }

        public async Task<AccessToken> CreateAccessToken(Owner owner)
        {
            List<OperationClaim> claims = await GetUserClaims(owner);

            AccessToken token = _tokenHelper.CreateToken(owner, claims);
            return token;
        }

        public async Task<Owner> GetOwnerById(int ownerId)
        {
            return await _authRepository.GetAsync(x => x.Id == ownerId, x => x.Include(x=>x.Restaurants));
        }


        #region private methods

        private async Task<List<OperationClaim>> GetUserClaims(Owner owner)
        {
            // gets 10 claims maybe can cause errors in future when claims get more
            return (await _userOperationClaimRepository.GetListAsync(x => x.UserId == owner.Id, include: x => x.Include(x => x.OperationClaim))).Items
                                           .Select(x => new OperationClaim() { Id = x.OperationClaim.Id, CreatedDate = x.OperationClaim.CreatedDate, Name = x.OperationClaim.Name }).ToList();

        }
        #endregion

    }
}
