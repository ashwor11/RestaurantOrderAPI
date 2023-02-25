using Core.Security.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        private IMediator? _mediator;

        protected int GetOwnerId() { int ownerId = HttpContext.User.GetUserId();  return ownerId; }

        protected string GetIpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For")) return Request.Headers["X-Forwarded-For"];
            return HttpContext.Connection.RemoteIpAddress.ToString();
        }



    }
}