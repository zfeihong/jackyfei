using Iot.Identity.Helper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Iot.WebApi.Controllers.v1
{
    //[Authorize]
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class ApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
