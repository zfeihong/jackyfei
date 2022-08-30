using Iot.Application.Common.Interfaces;
using Iot.Application.Dto.User;
using Microsoft.AspNetCore.Mvc;

namespace Iot.WebApi.Controllers.v1
{
    /// <summary>
    /// 简单起见，这里请求用户使用了硬编码
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService) => _userService = userService;

        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody] AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "用户名和密码无效！" });

            return Ok(response);
        }
    }
}
