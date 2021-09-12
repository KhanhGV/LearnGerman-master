using DE_APPLICATION_ELEANING.DeThiApp;
using DE_APPLICATION_ELEANING.DeThiApp.Dto;
using DE_APPLICATION_ELEANING.SubjectTypeApp;
using DE_APPLICATION_ELEANING.SubjectTypeApp.Dto;
using DE_APPLICATION_ELEANING.ThiAnswerApp;
using DE_APPLICATION_ELEANING.ThiBaiTapApp;
using DE_APPLICATION_ELEANING.ThiBaiTapApp.Dto;
using DE_APPLICATION_ELEANING.ThiQuestionApp;
using DE_APPLICATION_ELEANING.ThiQuestionApp.Dto;
using DE_APPLICATION_ELEANING.ThiQuestionTypeApp.Dto;
using DE_APPLICATION_ELEANING.ThiQuestionTypeTypeApp;
using DE_APPLICATION_ELEANING.ThiResultApp;
using DE_APPLICATION_ELEANING.ThiResultApp.Dto;
using DE_DB_ELEANING.AFModel;
using Oauth_2._0_v2.Common;
using Oauth_2._0_v2.Handle;
using Oauth_2._0_v2.Models.DeThi;
using Oauth_2._0_v2.Models.Response;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;
namespace Oauth_2._0_v2.Controllers
{
    [Authorize]
    [RoutePrefix("bit_sol/api/v1/dethi")]
    public class DeThiController : BaseController
    {

        private readonly ThiQuestionTypeTypeApplication _thiquesstionType;
        private readonly ThiQuestionApplication _thiQuestionApp;
        private readonly DeThiApplication _deThiApplication;
        private readonly ThiResultApplication _thiResultApplication;
        private readonly ThiAnswerApplication _thiAnswerApplication;
        private readonly ThiSubjectTypeApplication _thiSubjectType;
        private readonly ThiBaiTapApplication _thiBaiTapApp;
        public DeThiController()
        {
            if (_thiBaiTapApp == null)
            {
                _thiBaiTapApp = new ThiBaiTapApplication();
            }
            if (_thiQuestionApp == null)
            {
                _thiQuestionApp = new ThiQuestionApplication();
            }
            if (_deThiApplication == null)
            {
                _deThiApplication = new DeThiApplication();
            }
            if (_thiResultApplication == null)
            {
                _thiResultApplication = new ThiResultApplication();
            }
            if (_thiAnswerApplication == null)
            {
                _thiAnswerApplication = new ThiAnswerApplication();
            }
            if (_thiSubjectType == null)
            {
                _thiSubjectType = new ThiSubjectTypeApplication();
            }
            if (_thiquesstionType == null)
            {
                _thiquesstionType = new ThiQuestionTypeTypeApplication();
            }

        }
        [HttpPost]
        [Route("examination-all-list")]
        [CheckModelForNull]
        [InvalidModelStateFilter]
        public async Task<IHttpActionResult> GetListDeThi()
        {
            SearchDeThiModelView model = new SearchDeThiModelView();            
            object _response;
            var host = HttpContext.Current.Request.Url.Host;
            string ip = HttpContext.Current.Request.UserHostAddress;
            try
            {
                SearchDeThi searchDto = new SearchDeThi();
                searchDto.NameDe = model.NameDe;
                searchDto.NameVi = model.NameVi;
                searchDto.Status = true;
                searchDto.Type = "luat";
                var _result = await _deThiApplication.GetListAsync(searchDto);
                var data = _result.OrderBy(a => a.Index).Select(t => new
                {
                    Id = t.ID,
                    QuestionVi = t.Name.Trim(),
                    QuestionDe = t.NameDe.Trim()
                });
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                  new ResultResponse<object>(true, "success", new { examination_list = data }));
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
        [Route("examination-type-subject")]
        [CheckModelForNull]
        [InvalidModelStateFilter]
        public async Task<IHttpActionResult> GetTypeSubject()
        {
            object _response;
            var host = HttpContext.Current.Request.Url.Host;
            string ip = HttpContext.Current.Request.UserHostAddress;
            try
            {
                SearchSubjectType searchQuestionDto = new SearchSubjectType();
                var _result = await _thiSubjectType.GetListAsync(searchQuestionDto);
                var data = _result.Select(t => new
                {
                    Id = t.ID,
                    TypeNameVi = t.TypeNameVi,
                    TypeNameDe = t.TypeNameDe,
                    NoteVi = t.NoteVi,
                    NoteDe = t.NoteDe
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
        [Route("examination-type-question")]
        [CheckModelForNull]
        [InvalidModelStateFilter]
        public async Task<IHttpActionResult> GetTypeQuestion()
        {
            object _response;
            var host = HttpContext.Current.Request.Url.Host;
            string ip = HttpContext.Current.Request.UserHostAddress;
            try
            {
                SearchThiQuestionType searchQuestionDto = new SearchThiQuestionType();
                var _result = await _thiquesstionType.GetListAsync(searchQuestionDto);
                var data = _result.Select(t => new
                {
                    Id = t.ID,
                    TypeNameVi = t.TypeNameVi,
                    TypeNameDe = t.TypeNameDe
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
        [Route("examination-get-bai-tap")]
        [CheckModelForNull]
        [InvalidModelStateFilter]
        public async Task<IHttpActionResult> GetQuestion(SearchBaiTapThiModelView model)
        {
            object _response;
            var host = HttpContext.Current.Request.Url.Host;
            string ip = HttpContext.Current.Request.UserHostAddress;
            try
            {
                SearchThiBaiTap searchDto = new SearchThiBaiTap();
                //searchDto.ThiTypeSubjectID = model.ThiTypeSubjectID;
                searchDto.DeThiID = model.DeThiID;
                searchDto.SubjectTypeId = model.SubjectTypeID;
                var _result = await _thiBaiTapApp.GetListAsync(searchDto);
                var data = _result.OrderBy(a => a.Index).Select(t => new
                {
                    Id = t.ID,
                    NameVi = t.BtNameVi,
                    NameDe = t.BtNameDe,
                    NoteVi = t.NoteVi,
                    NoteDe = t.NoteDe
                });
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                  new ResultResponse<object>(true, "success", new { ThiBaiTap = data }));
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                  new ResultResponse<object>(false, "error", new { ThiBaiTap = e.Message }));
                return Ok(_response);
            }

        }



        [HttpPost]
        [Route("examination-questions")]
        [CheckModelForNull]
        [InvalidModelStateFilter]
        public async Task<IHttpActionResult> GetQuestion(SearchThiQuestionModelView model)
        {
            object _response;
            var host = HttpContext.Current.Request.Url.Host;
            string ip = HttpContext.Current.Request.UserHostAddress;
            try
            {
                SearchThiQuestionApi searchDto = new SearchThiQuestionApi();
                //searchDto.ThiTypeSubjectID = model.ThiTypeSubjectID;
                searchDto.BaiTapThiID = model.BaiTapThiID;
                searchDto.Status = true;
                var _result = await _thiQuestionApp.GetListAsync(searchDto);
                var data = _result.OrderBy(a => a.Index).Select(t => new
                {
                    Id = t.ID,
                    QuestionVi = t.QuestionVi,
                    QuestionDe = t.QuestionDe,
                    MediaQuestion = new
                    {
                        Audio = t.AudioMediaId.GetUrlGuiId(host) ?? null,
                        Image = t.ImageMediaId.GetUrlGuiId(host) ?? null
                    },
                    TypeQuestion = t.BaiTapThiID,
                    Answers = t.ThiAnswers.Where(a => a.IsDelete != true).Select(a => new
                    {
                        Id = a.ID,
                        AnswerVi = a.AnswerVi,
                        AnswerDe = a.AnswerDe,
                        Audio = t.AudioMediaId != null ? t.AudioMediaId.GetUrlGuiId(host) : null,
                        Image = t.ImageMediaId != null ? t.ImageMediaId.GetUrlGuiId(host) : null,
                        IsTrue = a.IsTrue ?? false
                    }),
                }); ;
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
        [Route("examination-question/{questionId}")]
        [HttpPost]
        public async Task<IHttpActionResult> GetByIdAsync(Guid questionId)
        {        
            var host = HttpContext.Current.Request.Url.Host;
            object _response;
            string ip = HttpContext.Current.Request.UserHostAddress;
            try
            {

                var _result = await _thiQuestionApp.GetbyIdAsync(questionId);
                var data = new
                {
                    Id = _result.ID,
                    QuestionVi = _result.QuestionVi,
                    QuestionDe = _result.QuestionDe,
                    MediaQuestion = new
                    {
                        Audio = _result.AudioMediaId.GetUrlGuiId(host) ?? null,
                        Image = _result.ImageMediaId.GetUrlGuiId(host) ?? null
                    },
                    TypeQuestion = _result.BaiTapThiID,
                    Answers = _result.ThiAnswers.Where(a => a.IsDelete != true).Select(a => new
                    {
                        Id = a.ID,
                        AnswerVi = a.AnswerVi,
                        AnswerDe = a.AnswerDe,
                        Audio = a.AudioMediaId != null ? a.AudioMediaId.GetUrlGuiId(host) : null,
                        Image = a.ImageMediaId != null ? a.ImageMediaId.GetUrlGuiId(host) : null,
                        IsTrue = a.IsTrue ?? false
                    }),
                };
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                  new ResultResponse<object>(true, "success", new { question = data }));
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
        [Route("examination-submit")]
        [CheckModelForNull]
        [InvalidModelStateFilter]
        public async Task<IHttpActionResult> SubmitExam(ThiResultModelView model)
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var studentId = principal.Claims.Where(c => c.Type == "ID").Single().Value;


            object _response;
            var host = HttpContext.Current.Request.Url.Host;
            string ip = HttpContext.Current.Request.UserHostAddress;
            try
            {
                var _result = false;
                foreach (var item in model.ListResult)
                {
                    ThiResult thi = new ThiResult();
                    thi.ID = Guid.NewGuid();
                    thi.QuestionID = model.QuestionID;
                    thi.StudentID = Guid.Parse(studentId);
                    thi.AnswerID = item.AnswerID;
                    thi.Sequence = item.Sequence;
                    thi.AnswerContent = item.AnswerContent;
                    thi.IsTrue = item.IsTrue;
                    thi.DeThiID = model.DeThiID;
                    SearchThiResult searchModel = new SearchThiResult(thi.StudentID, thi.QuestionID, thi.AnswerID, thi.DeThiID = model.DeThiID, "Check");
                    var _checkExist = await _thiResultApplication.GetListAsync(searchModel);
                   
                    if (_checkExist.Count == 0)
                    {
                        //insert
                        _result = await _thiResultApplication.CreateAsync(thi);
                    }
                    else
                    {
                        thi.Student = null;
                        thi.ThiAnswer = null;
                        thi.ThiDeThi = null;
                        thi.ID = _checkExist[0].ID;

                        _result = await _thiResultApplication.UpdateAsync(thi);
                    }
                }
                //SearchThiQuestionApi searchDto = new SearchThiQuestionApi();
                //searchDto.I = model.ThiTypeSubjectID;
                //searchDto.Status = true;
                if (!_result)
                {
                    _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                  new ResultResponse<object>(false, "error", new { Message = "false" }));
                    return Ok(_response);
                }

                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                  new ResultResponse<object>(true, "success", new { Message = _result }));
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                  new ResultResponse<object>(false, "error", new { Message = e.Message }));
                return Ok(_response);
            }

        }
    }
}