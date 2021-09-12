using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Oauth_2._0_v2.Author._2._0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Oauth_2._0_v2.Author
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            string symmetricKeyAsBase64 = string.Empty;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "client_Id is not set");
                return Task.FromResult<object>(null);
            }
            var audience = AudiencesStore.FindAudience(context.ClientId);
            if (audience == null)
            {
                context.SetError("invalid_clientId", string.Format("Invalid client_id '{0}'", context.ClientId));
                return Task.FromResult<object>(null);
            }

            context.Validated();
            return Task.FromResult<object>(null);
        }


        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            using (CustomerRepository _repo = new CustomerRepository())
            {
                if (context.UserName == null || context.Password == null)
                {
                    context.SetError("invalid_grant", "Provided  is null");
                    return;
                }
                var customer = await _repo.ValidateUser(context.UserName) ?? null;
                if (customer.Email == null)
                {
                    context.SetError("invalid_grant", "Provided phone or email is incorrect");
                    return;
                }
                else
                if (customer.Account.Password != context.Password)
                {
                    context.SetError("invalid_grant", "Provided password is incorrect");
                    return;
                }
                else
                 if (customer.Status == false)
                {
                    context.SetError("invalid_status", "Provided status is block");
                    return;
                }
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                identity.AddClaim(new Claim(ClaimTypes.Name, customer.Phone));
                identity.AddClaim(new Claim("ID", customer.Id.ToString()));
                identity.AddClaim(new Claim("Email", customer.Email));              
                var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                         "audience", (context.ClientId == null) ? string.Empty : context.ClientId
                    }
                });

                var ticket = new AuthenticationTicket(identity, props);
                context.Validated(ticket);
            }
        }
        //public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        //{
        //    foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
        //    {
        //        context.AdditionalResponseParameters.Add(property.Key, property.Value);
        //    }

        //    return Task.FromResult<object>(null);
        //}
    }
}