﻿using Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Dtos
{
    public class AuthResponseDto
    {
        public string RefreshToken { get; set; }
        public AccessToken AccessToken { get; set; }

        
    }
}
