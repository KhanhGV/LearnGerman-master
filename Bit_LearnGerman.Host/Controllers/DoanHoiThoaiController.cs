using DE_APPLICATION_ELEANING.ConversationApp;
using DE_APPLICATION_ELEANING.ConversationApp.Dto;
using DE_APPLICATION_ELEANING.ConversationSentenceApp;
using DE_APPLICATION_ELEANING.ConversationSentenceApp.Dto;
using Oauth_2._0_v2.Common;
using Oauth_2._0_v2.Handle;
using Oauth_2._0_v2.Models.Communication;
using Oauth_2._0_v2.Models.Response;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
namespace Oauth_2._0_v2.Controllers
{
    [RoutePrefix("bit_sol/api/v1/conversation")]
    [Authorize]
    public class DoanHoiThoaiController : BaseController
    {
        private readonly ConversationApp _conversationApp;
        private readonly ConversationSentenceApp _conversationSentenceApp;
        public DoanHoiThoaiController()
        {
            if(_conversationApp==null)
            {
                _conversationApp = new ConversationApp();
            }   
            if(_conversationSentenceApp == null)
            {
                _conversationSentenceApp = new ConversationSentenceApp();
            }    
        }
        [HttpPost]
        [CheckModelForNull]
        [InvalidModelStateFilter]
        [Route("get-list")]
        public async Task<IHttpActionResult> GetConversationResultAsync(CommunicationSearch search)
        {
            object _response;
            var host = HttpContext.Current.Request.Url.Host;
            string ip = HttpContext.Current.Request.UserHostAddress;
            try
            {
                SearchConversationDto searchDto = new SearchConversationDto();
                searchDto.SubjectId = search.CommunicationId;
                searchDto.Status = true;
                var _result = await _conversationApp.GetListAsync(searchDto);
                var data = _result.OrderBy(a => a.Index).Select(t => new
                {
                    Id = t.Id,
                    NameVi = t.NameVi,
                    NamDe = t.NameDe
                });
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                  new ResultResponse<object>(true, "success", new { conversations = data }));
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                  new ResultResponse<object>(false, "erorr", new { Message = e.Message }));
                return Ok(_response);
            }
            
        }
        [HttpPost]
        [CheckModelForNull]
        [InvalidModelStateFilter]
        [Route("get-list-conversation-sentence")]
        public async Task<IHttpActionResult> GetConversationResultAsync(ConverstrationSearch search)
        {
            object _response;
            var host = HttpContext.Current.Request.Url.Host;
            string ip = HttpContext.Current.Request.UserHostAddress;
            try
            {
                ConversationSentenceDto searchDto = new ConversationSentenceDto();
                searchDto.ConversationId = search.ConverstrationId;
                searchDto.Status = true;
                var _result = await _conversationSentenceApp.GetListAsync(searchDto);
                var data = _result.OrderBy(a => a.Index).Select(t => new
                {
                    Id = t.Id,
                    NameVi = t.NameVi,
                    NamDe = t.NameDe,
                    Location = t.Location,

                });
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                  new ResultResponse<object>(true, "success", new { conversation_sentences = data }));
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                  new ResultResponse<object>(false, "erorr", new { Message = e.Message }));
                return Ok(_response);
            }
            
        }
    }
}
