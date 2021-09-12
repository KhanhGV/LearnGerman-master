using DE_APPLICATION_ELEANING.StudentApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Oauth_2._0_v2.Handle
{
    public class AuthorizationHeaderHandler : DelegatingHandler
    {
        private StudentApplication studentApp = new StudentApplication();
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage();
            try
            {
                AuthenticationHeaderValue authorization = request.Headers.Authorization;
                if (authorization == null)
                {
                    return await base.SendAsync(request, cancellationToken);
                }
                ClaimsPrincipal principal = request.GetRequestContext().Principal as ClaimsPrincipal;
                var studentId = principal.Claims.Where(c => c.Type == "ID").Single().Value;
                var SinhVien = await studentApp.GetbyIdAsync(Guid.Parse(studentId));
                if (SinhVien == null)
                {
                    return await base.SendAsync(request, cancellationToken);
                }
                var status = SinhVien.Status;
                if (status.ToString() == null)
                {
                    return await Task<HttpResponseMessage>.Factory.StartNew(() =>
                        request.CreateResponse(HttpStatusCode.Unauthorized,
                        "Access denied"),
                        cancellationToken);
                }
                else
                {
                    if (status == true)
                    {
                        //SetPrincipal(new GenericPrincipal(identity, null));
                        var identity = new GenericIdentity(SinhVien.Name);
                        identity.AddClaim(new Claim(ClaimTypes.Role, "student"));
                        identity.AddClaim(new Claim(ClaimTypes.Name, SinhVien.Phone));
                        identity.AddClaim(new Claim("ID", SinhVien.Id.ToString()));
                        identity.AddClaim(new Claim("Email", SinhVien.Email));
                        SetPrincipal(new GenericPrincipal(identity, null));
                    }
                    else
                    {
                        return await Task<HttpResponseMessage>.Factory.StartNew(() =>
                        request.CreateResponse(HttpStatusCode.Unauthorized,
                        "Access denied"),
                        cancellationToken);
                    }
                }
                return await base.SendAsync(request, cancellationToken);
            }
            catch (Exception ex)
            {
                return await Task<HttpResponseMessage>.Factory.StartNew(() =>
                        request.CreateResponse(HttpStatusCode.Unauthorized,
                        "Access denied"),
                        cancellationToken);
            }
        }
        //AuthenticationHeaderValue authorization = request.Headers.Authorization;
        private static void SetPrincipal(IPrincipal principal)
        {
            // setting.   
            Thread.CurrentPrincipal = principal;
            // Verification.   
            if (HttpContext.Current != null)
            {
                // Setting.   
                HttpContext.Current.User = principal;
            }
        }
    }
}