﻿using Core.Security.Entities;
using Core.Security.JWT;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AuthService
{
    public interface IAuthService
    {
        public Task<AccessToken> CreateAccessToken(Owner owner);
        public bool ConfirmPasswords(Owner owner, string password);
        public Task<Owner> GetOwnerById(int ownerId);
        public Task AddRestaurantClaim(int ownerId, int restaurantId);
        public ClaimsPrincipal GetPrincipalFromToken(string token);
        public RefreshToken CreateRefreshToken(Owner owner, string ipAddress);
        Task RevokeRefreshToken(RefreshToken storedRefreshToken, string requestIpAdress, string token, string revokedForLogin);
        public Task<EmailVertificator> CreateEmailVerificator(User user);
        public Task SecondFactorSecurity(User user, string AuthenticationCode);
        Task DoesOtpCodeMatches(string otpVerificatorSecretKey, string code);
    }
}
