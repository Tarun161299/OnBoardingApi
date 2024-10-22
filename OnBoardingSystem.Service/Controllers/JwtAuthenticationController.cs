

namespace OnBoardingSystem.Service.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.Business.Behaviors;
    using Castle.DynamicProxy.Generators.Emitters.SimpleAST;

    [Route("api/[controller]")]
    [ApiController]
    public class JwtAuthenticationController : ControllerBase
    {
        private readonly JwtAuthenticationDirector _authenticationDirector;

        public JwtAuthenticationController(JwtAuthenticationDirector authenticationDirector) 
        {
            _authenticationDirector = authenticationDirector;
        }

        [AllowAnonymous]
        [HttpPost("Authorize")]
        public IActionResult AuthUser([FromBody]UserInfo user)
        {
            var token = _authenticationDirector.Authenticate(user.Username, user.Password);
            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}
