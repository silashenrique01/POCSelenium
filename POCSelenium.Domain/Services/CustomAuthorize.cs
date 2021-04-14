using Microsoft.AspNetCore.Http;

namespace POCSelenium.Domain.Services
{
    public class CustomAuthorize
    {

        public CustomAuthorize()
        {
        }

        public void OnAuthorization()
        {

           // var httpContext = HttpContext.Request.Headers.GetCommaSeparatedValues("Authorization");
         //   var credentials = httpContext[0].Split("Bearer ")[1];
        }
    }

}
