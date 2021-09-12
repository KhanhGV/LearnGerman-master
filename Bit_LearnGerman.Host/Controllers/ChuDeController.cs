using DE_APPLICATION_ELEANING.SubjectApp;
using DE_APPLICATION_ELEANING.SubjectApp.Dto;
using Oauth_2._0_v2.Common;
using Oauth_2._0_v2.Handle;
using Oauth_2._0_v2.Models;
using Oauth_2._0_v2.Models.Response;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
namespace Oauth_2._0_v2.Controllers
{
    [RoutePrefix("bit_sol/api/v1/subject")]
    public class ChuDeController : BaseController
    {
        private readonly SubjectApplication _subjectApp;
        public ChuDeController()
        {

            if (_subjectApp == null)
            {
                _subjectApp = new SubjectApplication();
            }
        }
        [HttpPost]
        [Route("get-list")]
        [Authorize]
        [CheckModelForNull]
        [InvalidModelStateFilter]
        public async Task<IHttpActionResult> List(SearchSubjectModel search)
        {
            object _response;
            string ip = HttpContext.Current.Request.UserHostAddress;
            try
            {
                if (ModelState.IsValid)
                {
                    SearchSubjectDto typesubject = new SearchSubjectDto();
                    typesubject.NameVi = search.NameVi;
                    typesubject.NameDe = search.NameDe;
                    typesubject.Type = search.Menu;
                    typesubject.Status = true;
                    var host = HttpContext.Current.Request.Url.Host;

                    var data = await _subjectApp.GetListAsync(typesubject);
                    var model = data.OrderBy(a=>a.Index).Select(m => new
                    {
                        Id = m.Id,
                        NameVi = m.NameVi,
                        NameDe = m.NameDe,
                        Icon = host + "/getfile?sub=subject&Id=" + m.Id
                    }).ToList();
                    _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                        new ResultResponse<object>(true, "success", new { subjects = model }));
                    return Ok(_response);
                }
            }
            catch (Exception e)
            {
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                  new ResultResponse<object>(false, "erorr", new { Message = e.Message }));
                return Ok(_response);
            }

            return BadRequest();
        }
    }
}