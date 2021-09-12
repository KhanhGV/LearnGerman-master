using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.OAuth;
using Owin;
[assembly: OwinStartup(typeof(Oauth_2._0_v2.Author.Startup))]

namespace Oauth_2._0_v2.Author
{
    public class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public static FacebookAuthenticationOptions facebookAuthOptions { get; private set; }
        public void Configuration(IAppBuilder app)
        {
            //auth Bearer
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/bit_sol/api/v1/sign_in"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(7),
                Provider = new MyAuthorizationServerProvider(),
                RefreshTokenProvider = new SimpleRefreshTokenProvider()

            };
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
            //app.UseFacebookAuthentication(new FacebookAuthenticationOptions()
            //{
            //    AppId = "3828759333908062",
            //    AppSecret= "88c4b205b86614005882961fe9a3b69b"
            //});
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
        }
    }
}
