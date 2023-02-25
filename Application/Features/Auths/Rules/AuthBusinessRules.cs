using Application.Services.AuthService;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Rules
{
    public class AuthBusinessRules
    {
        private readonly IAuthRepository _authRepository;

        public AuthBusinessRules(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task IsUserAlreadyExists(string email) { User? user = await _authRepository.GetAsync(x => x.Email == email); if (user != null) throw new BusinessException("User already exists."); }
        public async Task IsUserExists(User user) {if (user == null) throw new BusinessException("User does not exist."); }

    }
}
