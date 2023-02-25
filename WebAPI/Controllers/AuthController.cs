using System.Web;
using Application.Features.Auths.Commands.EnableEmailVertification;
using Application.Features.Auths.Commands.EnableOtpVerification;
using Application.Features.Auths.Commands.Login;
using Application.Features.Auths.Commands.Refresh;
using Application.Features.Auths.Commands.Register;
using Application.Features.Auths.Commands.VerifyEmailVertification;
using Application.Features.Auths.Commands.VerifyOtpVerification;
using Application.Features.Auths.Commands.VerifyUser;
using Application.Features.Auths.Dtos;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1.Crmf;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly TokenOptions _tokenOptions;
        private readonly IConfiguration _configuration;

        public AuthController(IOptions<TokenOptions> tokenOptions, IConfiguration configuration)
        {
            _tokenOptions = tokenOptions.Value;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            string ipAddress = GetIpAddress();
            RegisterCommand request = new() { UserForRegisterDto = userForRegisterDto, IpAddress = ipAddress,EmailVerificationPrefix = $"{_configuration.GetSection("APIDomain").Value.ToString()}/Auth/VerifyAccount"};
            AuthResponseDto response = await Mediator.Send(request);
            SetRefreshTokenToCookies(response.RefreshToken);
            return Ok(response.AccessToken);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            string ipAddress = GetIpAddress();
            LoginCommand request = new() { IpAdress = ipAddress, UserForLoginDto = userForLoginDto };
            AuthResponseDto response = await Mediator.Send(request);
            if (response.RefreshToken is not null) SetRefreshTokenToCookies(response.RefreshToken);
            SetRefreshTokenToCookies(response.RefreshToken);
            return Ok(response.AccessToken);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] string accessToken)
        {
            string ipAddress = GetIpAddress();
            string refreshToken = HttpContext.Request.Cookies["refreshToken"];
            if (refreshToken == null) return BadRequest();
            RefreshTokenCommand request = new() { IpAdress = ipAddress, RefreshTokenDto = new(){AccessToken = accessToken,RefreshToken = refreshToken}  };
            AuthResponseDto response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("enableEmailVertification")]
        public async Task<IActionResult> EnableEmailVertification()
        {
            int ownerId = GetOwnerId();
            EnableEmailVertificationCommand request = new()
            {
                UserId = ownerId,
                EnableEmailPrefix =
                    $"{_configuration.GetSection("APIDomain").Value.ToString()}/Auth/VerifyEmailVerification"
            };
            await Mediator.Send(request);
            return Ok();
        }

        [HttpGet("VerifyEmailVerification")]
        public async Task<IActionResult> VerifyEmailVerification([FromQuery] string ActivationKey)
        {
            string activationKey = HttpUtility.UrlDecode(ActivationKey);
            VerifyEmailVerificationCommand request = new() { ActivationKey = activationKey };
            await Mediator.Send(request);
            return Ok();
        }

        [HttpGet("EnableOtpVerification")]
        public async Task<IActionResult> EnableOtpVerification()
        {
            int ownerId = GetOwnerId();
            EnableOtpVerificationCommand request = new() { UserId = ownerId };
            EnableOtpVerificationDto response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("VerifyOtpVerification")]
        public async Task<IActionResult> VerifyOtpVerification([FromBody] string code)
        {
            int userId = GetOwnerId();
            VerifyOtpVerificationCommand request = new() { UserId = userId, Code = code };
            await Mediator.Send(request);
            return Ok();
        }

        [HttpGet("VerifyAccount")]
        public async Task<IActionResult> VerifyAccount([FromQuery] string ActivationToken)
        {
            string activationToken = HttpUtility.UrlDecode(ActivationToken);
            VerifyUserCommand request = new() { ActivationToken = ActivationToken };
            await Mediator.Send(request);
            return Ok();
        }


        private void SetRefreshTokenToCookies(string refreshToken)
        {
            CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.Now.AddDays(_tokenOptions.RefreshTokenTTL) };
            Response.Cookies.Append("refreshToken",refreshToken,cookieOptions);
        }
    }
}
