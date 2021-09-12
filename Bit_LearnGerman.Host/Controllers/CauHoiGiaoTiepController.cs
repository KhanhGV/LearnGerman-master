using DE_APPLICATION_ELEANING.QuestionApp;
using DE_APPLICATION_ELEANING.QuestionApp.Dto;
using DE_APPLICATION_ELEANING.WordApp;
using Oauth_2._0_v2.Common;
using Oauth_2._0_v2.Models.Response;
using Oauth_2._0_v2.Models.SearchQuestion;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using DE_DB_ELEANING.AFModel;
using Oauth_2._0_v2.Handle;
using DE_APPLICATION_ELEANING.TemplateApp;
using DE_APPLICATION_ELEANING.GrammarApp;
using Oauth_2._0_v2.Models.Communication;
using DE_APPLICATION_ELEANING.WordApp.Dto;
namespace Oauth_2._0_v2.Controllers
{
    [Authorize]
    [RoutePrefix("bit_sol/api/v1/communication")]
    public class CauHoiGiaoTiepController : BaseController
    {
        private readonly TemplateApplication _templateApp;
        private readonly GrammarApplication _grammarApp;
        private readonly QuestionApplication _questionApp;
        private readonly WordApplication _wordApp;
        //
        public CauHoiGiaoTiepController()
        {
            if (_templateApp == null)
            {
                _templateApp = new TemplateApplication();
            }
            if (_grammarApp == null)
            {
                _grammarApp = new GrammarApplication();
            }
            if (_questionApp == null)
            {
                _questionApp = new QuestionApplication();
            }
            if (_wordApp == null)
            {
                _wordApp = new WordApplication();
            }
        }
        [HttpPost]
        [Route("communication-sentence")]
        [CheckModelForNull]
        [InvalidModelStateFilter]
        public async Task<IHttpActionResult> GetBaiCauGiaoTiep(CommunicationSearch model)
        {
            object _response;
            var host = HttpContext.Current.Request.Url.Host;
            string ip = HttpContext.Current.Request.UserHostAddress;
            try
            {
               
                SearchWorDto searchDto = new SearchWorDto();
                searchDto.SubjectId = model.CommunicationId;
                searchDto.Status = true;
                var _result = await _wordApp.GetListAsync(searchDto);
                var data = _result.OrderBy(a => a.Index).Select(t => new
                {
                    Id = t.Id,
                    QuestionVi = t.WordVi,
                    QuestionDe = t.WorDe

                });
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                  new ResultResponse<object>(true, "success", new { communication_sentence = data }));
                return Ok(_response);
            }
            catch(Exception e)
            {
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                  new ResultResponse<object>(false, "error", new { Message = e.Message }));
                return Ok(_response);
            }
            
        }
        [HttpPost]
        [Route("questionss")]
        [CheckModelForNull]
        [InvalidModelStateFilter]
        public async Task<IHttpActionResult> ConCac(CommunicationSearch model)
        {
            object _response;
            var host = HttpContext.Current.Request.Url.Host;
            string ip = HttpContext.Current.Request.UserHostAddress;
            try
            {
                SearchQuestionDto searchDto = new SearchQuestionDto();
                searchDto.CommunicationId = model.CommunicationId;
                searchDto.Status = true;
                var _result = await _questionApp.GetListAsync(searchDto);
                var data = _result.OrderBy(a => a.Index).Select(t => new
                {
                    Id = t.Id,
                    QuestionVi = t.NameVi,
                    QuestionDe = t.NameDe,
                    Answers = t.Answers.Where(a=>a.IsDelete != true).Select(a => new
                    {
                        Id = a.Id,
                        AnswerVi = a.DetailsVi,
                        AnswerDe = a.Details,
                        ParentAnswer = "",
                        IsTrue = a.IsTrue ?? false
                    }),
                });
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                  new ResultResponse<object>(true, "success", new { questions = data }));
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                  new ResultResponse<object>(false, "error", new { Message = e.Message }));
                return Ok(_response);
            }

        }
        [HttpPost]
        [Route("questions")]
        [CheckModelForNull]
        [InvalidModelStateFilter]
        public async Task<IHttpActionResult> GetBaiTap(CommunicationSearch model)
        {
            object _response;
            var host = HttpContext.Current.Request.Url.Host;
            string ip = HttpContext.Current.Request.UserHostAddress;
            try
            {
                SearchQuestionDto searchDto = new SearchQuestionDto();
                searchDto.CommunicationId = model.CommunicationId;
                searchDto.Status = true;
                var _result = await _questionApp.GetListAsync(searchDto);
                var data = _result.OrderBy(a => a.Index).Select(t => new
                {
                    Id = t.Id,
                    QuestionVi = t.NameVi,
                    QuestionDe = t.NameDe,
                    Answers = t.Answers.Where(a => a.IsDelete != true).Select(a => new
                    {
                        Id = a.Id,
                        AnswerVi = a.DetailsVi,
                        AnswerDe = a.Details,
                        ParentAnswer = "",
                        IsTrue = a.IsTrue ?? false
                    }),
                });
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                  new ResultResponse<object>(true, "success", new { questions = data }));
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                  new ResultResponse<object>(false, "error", new { Message = e.Message }));
                return Ok(_response);
            }
           
        }


        [Route("question/{questionId}")]
        [HttpPost]
        public async Task<IHttpActionResult> GetByIdAsync(Guid questionId)
        {
            var host = HttpContext.Current.Request.Url.Host;
            object _response;
            string ip = HttpContext.Current.Request.UserHostAddress;
            try
            {

                var _resutl = await _questionApp.GetbyIdAsync(questionId);
                var model = new
                {
                    Id = _resutl.Id,
                    QuestionDe = _resutl.NameDe,
                    QuestionVi = _resutl.NameVi,
                    Answers = _resutl.Answers.Where(a => a.IsDelete != true).Select(a => new
                    {
                        Id = a.Id,
                        AnswerVi = a.DetailsVi,
                        AnswerDe = a.Details,
                        ParentAnswer = "",
                        IsTrue = a.IsTrue ?? false
                    }),
                };

                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                       new ResultResponse<object>(true, "success", new { Qestion = model }));
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                  new ResultResponse<object>(false, "error", new { Message = e.Message }));
                return Ok(_response);
            }

        }
        [HttpPost]
        [CheckModelForNull]
        [InvalidModelStateFilter]
        [Route("question/update-correct-answer")]
        public async Task<IHttpActionResult> RequesAnswerVocabulary(SearchAnswerModel model)
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var studentId = principal.Claims.Where(c => c.Type == "ID").Single().Value;
            object _response;
            string ip = HttpContext.Current.Request.UserHostAddress;
            try
            {
                
                SearchAnswerDto searchAnswerDto = new SearchAnswerDto();
                searchAnswerDto.QuestionId = model.QuestionId;
                searchAnswerDto.StudenId = Guid.Parse(studentId);

                var _result = await _questionApp.GetVocabularyResult(searchAnswerDto);
                var data = _result.FirstOrDefault();
                if (data == null)
                {
                    VocabularyResult vocabularyResult = new VocabularyResult();
                    vocabularyResult.AnswerId = model.AnswerId;
                    vocabularyResult.QuestionId = model.QuestionId;
                    vocabularyResult.StudenId = Guid.Parse(studentId);
                    var me = await this.CheckIsTrueAsync(model.AnswerId, model.QuestionId);
                    vocabularyResult.Status = me.Status;
                    var create = await _questionApp.CreateVocabularyResult(vocabularyResult);
                    me.Note = "You just made this sentence";
                    _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                         new ResultResponse<object>(true, "success", new { Message= me }));
                    return Ok(_response);

                }
                else
                {
                    var me = await this.CheckIsTrueAsync(model.AnswerId, model.QuestionId);
                    if (data.Answer.IsTrue == true && me.Status == false)
                    {
                        me.Note = "You did it wrong this time, but did it right the previous time";
                        _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                        new ResultResponse<object>(true, "success", new { Message = me }));
                        return Ok(_response);
                    }
                    else
                    if (data.Answer.IsTrue == true && me.Status == true)
                    {
                        me.Note = "Great! Last time you did the right thing and the same goes for now";
                        _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                        new ResultResponse<object>(true, "success", new { Message = me }));
                        return Ok(_response);
                    }
                    else
                    {
                        data.Status = me.Status;
                        data.AnswerId = model.AnswerId;
                        me.Note = "Update results!";
                        var update = await _questionApp.UpdateVocabularyResult(data);
                        _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                       new ResultResponse<object>(true, "success", new { Message = me }));
                        return Ok(_response);
                        // cập nhật kết quả
                    }
                }
            }
            catch (Exception e)
            {
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                  new ResultResponse<object>(false, "error", new { Message = e.Message }));
                return Ok(_response);
            }

        }
        private async Task<Mesages> CheckIsTrueAsync(Guid AnswerId, Guid QuestionId)
        {
            Mesages mesages = new Mesages();
            var question = await _questionApp.GetbyIdAsync(QuestionId);
            var _answer = question.Answers.Where(a => a.Id == AnswerId).FirstOrDefault();
            if (_answer.IsTrue == true)
            {
                mesages.Status = true;
                mesages.Message = "You got this sentence right";
            }
            else
            {
                mesages.Status = false;
                mesages.Message = "You got this sentence wrong";
            }
            return await Task.FromResult(mesages);
        }

    }
}
