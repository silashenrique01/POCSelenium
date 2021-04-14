using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using POCSelenium.Domain;
using POCSelenium.Domain.Entities;
using POCSelenium.Domain.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace POCSelenium.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var httpContext = HttpContext.Request.Headers.GetCommaSeparatedValues("Authorization");

            var credentials = httpContext[0];
            var credentials2 = credentials.Split("Basic ");
            credentials.Split(" ");
            var encoding = Encoding.GetEncoding("iso-8859-1");
            credentials = encoding.GetString(Convert.FromBase64String(credentials2[1]));
            int separator = credentials.IndexOf(':');
            string email = credentials.Substring(0, separator);

            var user = _userRepository.GetRolesUser(email);

            if (user.Role == "Admin") {
                var result = _userRepository.GetAll();
                return Ok(result);
            }else
               {
                return Unauthorized();
               }
        }



        [HttpPost]
        [Route("insert")]
        //[CustomAuthorize(Roles = RoleConstants.Admin+", "+RoleConstants.Moderator)]
        //[Authorize("Admin")]
        [AllowAnonymous]
        public IActionResult Insert([FromBody] User user)
        {
            bool result = _userRepository.Insert(user);
            return Ok(result);
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult Delete([FromBody] User user)
        {
            bool result = _userRepository.Delete(user);
            return Ok(result);
        }

        [HttpPut]
        [Route("edit")]
        public IActionResult Edit([FromBody] User user)
        {
            bool result = _userRepository.Update(user);
            return Ok(result);
        }
    }
}
