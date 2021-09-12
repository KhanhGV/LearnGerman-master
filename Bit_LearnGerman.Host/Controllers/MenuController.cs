using DE_APPLICATION_ELEANING.CategoryApp;
using DE_APPLICATION_ELEANING.CategoryApp.Dto;
using Oauth_2._0_v2.Common;
using Oauth_2._0_v2.Models.Response;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
namespace Oauth_2._0_v2.Controllers
{
    [RoutePrefix("bit_sol/api/v1/menu")]
    public class MenuController : BaseController
    {
        private readonly CategoryAppLication _categoryApp;

        public MenuController()
        {
            if (_categoryApp == null)
            {
                _categoryApp = new CategoryAppLication();
            }
        }
        [Authorize]
        [Route("get-list")]
        public async Task<IHttpActionResult> List()
        {
            var host = HttpContext.Current.Request.Url.Host;
            object _response;
            string ip = HttpContext.Current.Request.UserHostAddress;
            try
            {
                var data = await _categoryApp.GetListAsync(new SearchCateDto(true));
                var model = data.Where(a => a.Parent == null).Select(m => new
                {
                    Id = m.Id,
                    NameVi = m.TypeNameVi,
                    NameDe = m.TypeNameDe,
                    Icon = host + "/getfile?sub=typesubject&Id=" + m.Id,
                    SubMenu = this.GetParentMenuAsync(m.Id).Result ?? new object()
                }); ;
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                    new ResultResponse<object>(true, "success", new { menus = model }));
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                  new ResultResponse<object>(false, "erorr", new { Message = e.Message }));
                return Ok(_response);
            }
            
        }
        private async Task<object> GetParentMenuAsync(Guid parentId)
        {
            object _result = null;
            var host = HttpContext.Current.Request.Url.Host;
            var data = await _categoryApp.GetListAsync(new SearchCateDto(true));
             _result = data.Where(a=>a.Parent != null && a.Parent == parentId).Select(m => new
            {
                Id = m.Id,
                NameVi = m.TypeNameVi,
                NameDe = m.TypeNameDe,
                Icon = host + "/getfile?sub=typesubject&Id=" + m.Id,
            });
            return _result;
        }
    }
}
