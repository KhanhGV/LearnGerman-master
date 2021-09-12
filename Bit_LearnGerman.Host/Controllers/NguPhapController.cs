using DE_APPLICATION_ELEANING.AswerApp;
using DE_APPLICATION_ELEANING.MediaApp;
using DE_APPLICATION_ELEANING.QuestionApp;
using DE_APPLICATION_ELEANING.QuestionApp.Dto;
using DE_APPLICATION_ELEANING.QuestionTypeApp;
using DE_APPLICATION_ELEANING.QuestionTypeApp.Dto;
using DE_APPLICATION_ELEANING.WordApp;
using Oauth_2._0_v2.Common;
using Oauth_2._0_v2.Models.Response;
using Oauth_2._0_v2.Models.SearchQuestion;
using System;
using DE_DB_ELEANING.Config;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using DE_DB_ELEANING.AFModel;
using Oauth_2._0_v2.Handle;
using DE_APPLICATION_ELEANING.TemplateApp;
using Oauth_2._0_v2.Models.Template;
using DE_APPLICATION_ELEANING.TemplateApp.Dto;
using DE_APPLICATION_ELEANING.GrammarApp.Dto;
using DE_APPLICATION_ELEANING.GrammarApp;

namespace Oauth_2._0_v2.Controllers
{
    [RoutePrefix("bit_sol/api/v1/grammar")]
    [Authorize]
    public class NguPhapController : BaseController
    {
        private readonly TemplateApplication _templateApp;
        private readonly GrammarApplication _grammarApp;
        private readonly QuestionApplication _questionApp;
        public NguPhapController()
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
        }
        [HttpPost]
        [Route("theory")]
        [CheckModelForNull]
        [InvalidModelStateFilter]
        public async Task<IHttpActionResult> GetLyThuyetAsync(SearchTemplateModel model)
        {
            object _response;
            var host = HttpContext.Current.Request.Url.Host;
            string ip = HttpContext.Current.Request.UserHostAddress;
            try
            {
                TemplateSearchDto searchDto = new TemplateSearchDto();
                searchDto.SubjectId = model.subjectId;
                searchDto.Status = true;
                var _result = await _templateApp.GetListAsync(searchDto);
                var data = _result.Select(t => new
                {
                    url = host + "/Template/?id=" + t.Id
                });
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                   new ResultResponse<object>(true, "success", new { c = data }));
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
        [Route("exercises")]
        [CheckModelForNull]
        [InvalidModelStateFilter]
        public async Task<IHttpActionResult> GetBaiTap(SearchTemplateModel model)
        {
           
            object _response;
            var host = HttpContext.Current.Request.Url.Host;
            string ip = HttpContext.Current.Request.UserHostAddress;
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var studentId = principal.Claims.Where(c => c.Type == "ID").Single().Value;
                SearchGrammarDto searchDto = new SearchGrammarDto();
                searchDto.SubjectId = model.subjectId;
                searchDto.Status = true;
                var _result = await _grammarApp.GetListAsync(searchDto);
                var data = _result.OrderBy(a => a.Index).Select(t => new
                {
                    Id = t.Id,
                    NameVi = t.NamVi,
                    NameDe = t.NameDe,
                    Question = new
                    {
                        Complete = this.GetComplete(t.Id, Guid.Parse(studentId)).Result,
                        Total = t.Questions.Count,
                    }
                });
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                  new ResultResponse<object>(true, "success", new { exercises = data }));
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
        [Route("question-exercises")]
        [CheckModelForNull]
        [InvalidModelStateFilter]
        public async Task<IHttpActionResult> GetBaiTap(SearchQuestionGrammarModel model)
        {
            object _response;
            var host = HttpContext.Current.Request.Url.Host;
            string ip = HttpContext.Current.Request.UserHostAddress;
            try
            {
                SearchQuestionDto searchDto = new SearchQuestionDto();
                searchDto.Grammar_TheoryId = model.ExerciseId;
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
                  new ResultResponse<object>(false, "erorr", new { Message = e.Message }));
                return Ok(_response);
            }
            
        }
        [Route("question-exercise/{questionId}")]
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
                       new ResultResponse<object>(true, "success", new { question = model }));
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
        [Route("question-exercise/update-correct-answer")]
        public async Task<IHttpActionResult> RequesAnswerVocabulary(SearchAnswerModel model)
        {
            object _response;
            string ip = HttpContext.Current.Request.UserHostAddress;
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var studentId = principal.Claims.Where(c => c.Type == "ID").Single().Value;

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
                         new ResultResponse<object>(true, "success", new { Message = me }));
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
                  new ResultResponse<object>(false, "erorr", new { Message = e.Message }));
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
        private async Task<int> GetComplete(Guid ExercisesId, Guid StudenId)
        {
            int count = 0;
            SearchQuestionDto searchQuestionDto = new SearchQuestionDto();
            searchQuestionDto.Grammar_TheoryId = ExercisesId;
            var question = await _questionApp.GetListAsync(searchQuestionDto);
            foreach (var item in question)
            {
                SearchAnswerDto search = new SearchAnswerDto();
                search.StudenId = StudenId;
                search.QuestionId = item.Id;
                var result = await _questionApp.GetVocabularyResult(search);
                if(result !=null && result.Count !=0)
                {
                    foreach (var subitem in result)
                    {
                        if(subitem.Answer.IsTrue==true)
                        {
                            count++;
                        }    
                    }
                }    
            }
            
            return count;
        }
    }
}