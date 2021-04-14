using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using POCSelenium.Domain.Interfaces;
using POCSelenium.Infra;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace POCSelenium
{


    public class MyAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler
             DefaultHandler = new AuthorizationMiddlewareResultHandler();

        public async Task HandleAsync(
            RequestDelegate requestDelegate,
            HttpContext httpContext,
            AuthorizationPolicy authorizationPolicy,
            PolicyAuthorizationResult policyAuthorizationResult)
        {
            // if the authorization was forbidden and the resource had specific requirements,
            // provide a custom response.
            if (Show404ForForbiddenResult(policyAuthorizationResult))
            {
                // Return a 404 to make it appear as if the resource does not exist.
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }

            // Fallback to the default implementation.
            await DefaultHandler.HandleAsync(requestDelegate, httpContext, authorizationPolicy,
                                   policyAuthorizationResult);
        }

        bool Show404ForForbiddenResult(PolicyAuthorizationResult policyAuthorizationResult)
        {
            return policyAuthorizationResult.Forbidden &&
                policyAuthorizationResult.AuthorizationFailure.FailedRequirements.OfType<Show404Requirement>().Any();
        }
    }

    public class Show404Requirement : IAuthorizationRequirement
    {

    }
    public class Startup
    {

        private static void HandleBranch(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var branchVer = context.Request.Query["branch"];
                await context.Response.WriteAsync($"Branch used = {branchVer}");
            });
        }

        public IConfiguration Configuration { get; }
        public Startup(IWebHostEnvironment webHostEnvironment)
        {

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(webHostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{webHostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();


            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            
            
            



            //   services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            // services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions>("BasicAuthentication", null);


            //services.AddAuthorization(options =>
            //{

            //    options.AddPolicy ("Admin",
            //        authBuilder =>
            //        {
            //            authBuilder.RequireRole("Administrators");
            //        });
            //});

            //services.AddSingleton<IAuthorizationMiddlewareResultHandler,MyAuthorizationMiddlewareResultHandler>();

            //services.AddControllers(config =>
            //{
            //    var policy = new AuthorizationPolicyBuilder()
            //                     .RequireAuthenticatedUser()
            //                     .Build();
            //    config.Filters.Add(new AuthorizeFilter(policy));
            //});

            //services.AddSwaggerGen(c =>
            //    {
            //    c.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Title = "IntuitiveAPI",
            //        Version = "v1",
            //        Description = "Projeto de demonstração ASP.Net Core"
            //    });

            //        //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //        //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            //        //c.IncludeXmlComments(xmlPath);



            //        c.AddSecurityDefinition(

            //    "Bearer",
            //    new OpenApiSecurityScheme
            //    {
            //    Description = "Copie 'Bearer ' + token'",
            //    Name = "Authorization",
            //    In = ParameterLocation.Header,
            //    Type = SecuritySchemeType.ApiKey,
            //    Scheme = "Bearer"

            //    });

            //    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "POCSelenium v1"));
                //app.UseSwaggerUI(c => c.OAuthUsePkce());
            }
            CookiePolicyOptions cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
            };

            app.MapWhen(context => context.Request.Query.ContainsKey("branch"),
                               HandleBranch);

            app.UseCookiePolicy(cookiePolicyOptions);
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }


    //public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    //{
    //    public override void OnAuthorization(HttpActionContext actionContext)
    //    {
    //        if (actionContext.Request.Headers.Authorization != null)
    //        {
    //            var authToken = actionContext.Request.Headers.Authorization.Parameter;
    //            // decoding authToken we get decode value in 'Username:Password' format
    //            var decodeauthToken = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
    //            // spliting decodeauthToken using ':'
    //            var arrUserNameandPassword = decodeauthToken.Split(':');
    //            // at 0th postion of array we get username and at 1st we get password
    //            if (IsAuthorizedUser(arrUserNameandPassword[0], arrUserNameandPassword[1]))
    //            {
    //                // setting current principle
    //                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(arrUserNameandPassword[0]), null);
    //            }
    //            else
    //            {
    //                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
    //            }
    //        }
    //        else
    //        {
    //            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
    //        }
    //    }

    //    public static bool IsAuthorizedUser(string Username, string Password)
    //    {
    //        // In this method we can handle our database logic here...
    //        return Username.Equals("test") && Password == "test";
    //    }
    //}

}
