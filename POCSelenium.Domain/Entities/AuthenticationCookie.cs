//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Http;
//using System.Collections.Generic;
//using System.Security.Claims;

//namespace POCSelenium.Domain.Entities
//{
//    public class AuthenticationCookie
//    {

//        public async void OnValidateCookieAuthentication()
//        {

//            List<Claim> claims = new List<Claim>
//        {
//            new Claim(ClaimTypes.Email, user.Email),
//            new Claim("FullName", user.FullName),
//            new Claim(ClaimTypes.Role, "Administrator"),
//        };

//            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
//            claims, CookieAuthenticationDefaults.AuthenticationScheme);

//            AuthenticationProperties authProperties = new AuthenticationProperties
//            {
//                //AllowRefresh = <bool>,
//                // Refreshing the authentication session should be allowed.

//                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
//                // The time at which the authentication ticket expires. A 
//                // value set here overrides the ExpireTimeSpan option of 
//                // CookieAuthenticationOptions set with AddCookie.

//                //IsPersistent = true,
//                // Whether the authentication session is persisted across 
//                // multiple requests. When used with cookies, controls
//                // whether the cookie's lifetime is absolute (matching the
//                // lifetime of the authentication ticket) or session-based.

//                //IssuedUtc = <DateTimeOffset>,
//                // The time at which the authentication ticket was issued.

//                //RedirectUri = <string>
//                // The full path or absolute URI to be used as an http 
//                // redirect response value.
//            };

//            var result =  await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

//        }
//    }
//}
