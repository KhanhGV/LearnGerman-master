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
using Oauth_2._0_v2.Models.Answer;
using Oauth_2._0_v2.Models.Word;
using DE_APPLICATION_ELEANING.WordApp.Dto;
using PagedList;
using System.Collections.Generic;

namespace Oauth_2._0_v2.Controllers
{
    [RoutePrefix("bit_sol/api/v1/question-vocabulary")]
    [Authorize]
    public class CauHoiTuVungController : BaseController
    {
        private readonly QuestionTypeApplication _questionTypeApp;
        private readonly QuestionApplication _questionApp;
        private readonly WordApplication _wordApp;
        private readonly MediaApplication _mediaApp;
        private readonly AswerApplication _aswerApp;


        public CauHoiTuVungController()
        {
            if (_questionTypeApp == null)
            {
                _questionTypeApp = new QuestionTypeApplication();
            }
            if (_questionApp == null)
            {
                _questionApp = new QuestionApplication();
            }
            if (_mediaApp == null)
            {
                _mediaApp = new MediaApplication();
            }
            if (_wordApp == null)
            {
                _wordApp = new WordApplication();
            }
            if (_aswerApp == null)
            {
                _aswerApp = new AswerApplication();
            }
        }
        [HttpPost]
        [CheckModelForNull]
        [InvalidModelStateFilter]
        [Route("get-list-word")]
        public async Task<IHttpActionResult> GetListWWordAsync(SearchWorModelView model)
        {
            int pageSize = 20;
            int pageNumber = model.PageNumber != null && model.PageNumber !=0 ? model.PageNumber.Value : 1;
            object _response;
            string ip = HttpContext.Current.Request.UserHostAddress;
            var host = HttpContext.Current.Request.Url.Host;
            try
            {
                SearchWorDto searchWorDto = new SearchWorDto();
                searchWorDto.SubjectId = model.SubJectId;
                searchWorDto.NameDe = model.Name;
                searchWorDto.Status = true;
                var result = await _wordApp.GetListAsync(searchWorDto) ;
                if(result != null && result.Count==0)
                {
                    searchWorDto.NameDe = null;
                    searchWorDto.NameVi = model.Name;
                    result = await _wordApp.GetListAsync(searchWorDto);
                }  
                int TotalPage = (result.Count / pageSize) + 1;
                var data = result.OrderBy(a => a.Index).Select(w => new
                {
                    Id = w.Id,
                    NameDe = w.WorDe,
                    NameVi = w.WordVi,
                    MediaQuestion = new
                    {
                        Audio = (w.MediaAudioId != null) ? w.MediaAudioId.GetUrlGuiId(host) : null,
                        Image = (w.MediaImgId != null) ? w.MediaImgId.GetUrlGuiId(host) : null
                    },
                });
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                       new ResultResponse<object>(true, "success", new { words = data.ToPagedList(pageNumber, pageSize), pageIndex = model.PageNumber, TotalPage = TotalPage }));
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                  new ResultResponse<object>(false, "erorr", new { Message = e.Message }));
                return Ok(_response);
            }


        }
        [Route("get-type")]
        public async Task<IHttpActionResult> GetKieuCauHoiAsync()
        {
            object _response;
            string ip = HttpContext.Current.Request.UserHostAddress;
            try
            {
                var _forMatQuestion = FormatQuestionMeDia.BuildMappings();
                SearchQuestionTypeDto searchDto = new SearchQuestionTypeDto();
                searchDto.SubjectTypeId = Guid.Parse("6074fd6e-9a50-462b-a235-38de2345b952");
                searchDto.Status = true;
                var _typeQuestion = await _questionTypeApp.GetListAsync(searchDto);
                var model = _typeQuestion.Select(o => new
                {
                    Id = o.Id,
                    NameTypeVi = o.TypeNameVi,
                    NameTypeDe = o.TypeNameDe,
                });
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                       new ResultResponse<object>(true, "success", new { TypeQestions = model }));
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
        [Route("get-tuduy-all")]
        public async Task<IHttpActionResult> GetListTuDuy()
        {
            var host = HttpContext.Current.Request.Url.Host;
            object _response;
            string ip = HttpContext.Current.Request.UserHostAddress;
            try
            {

                SearchQuestionDto searchQuestionDto = new SearchQuestionDto();
                searchQuestionDto.QuestionTypeId = Guid.Parse("26393b3e-6ece-4d9c-aa6d-9f740ee8469b");
                searchQuestionDto.type = "tuvung";
                searchQuestionDto.Status = true;
                var _resutl = await _questionApp.GetListAsync(searchQuestionDto);
                object model = _resutl.OrderBy(a => a.Index).Select(q => new
                {
                    Id = q.Id,
                    QuestionDe = q.NameDe,
                    QuestionVi = q.NameVi,
                    DetailDe = q.DetailDe,
                    DetailVi = q.DetailVi

                });
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                       new ResultResponse<object>(true, "success", new { Questions = model }));
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                  new ResultResponse<object>(false, "erorr", new { Message = e.Message }));
                return Ok(_response);
            }

        }
        [Route("get-tuduy/{questionId}")]
        public async Task<IHttpActionResult> GetTuDuyId(Guid questionId)
        {
            var host = HttpContext.Current.Request.Url.Host;
            object _response;
            string ip = HttpContext.Current.Request.UserHostAddress;
            try
            {

                var _result = await _questionApp.GetbyIdAsync(questionId);
                var model = new
                {
                    Id = _result.Id,
                    QuestionDe = _result.NameDe,
                    QuestionVi = _result.NameVi,
                    DetailDe = _result.DetailDe,
                    DetailVi = _result.DetailVi,
                    TextAnswers = _result.Answers.Where(a => a.IsDelete != true).OrderBy(p => Guid.NewGuid()).Select(a => new
                    {
                        Id = a.Id,
                        AnswerText = a.Name
                    }),
                    MediaAnswers = _result.Answers.Where(a => a.IsDelete != true).OrderBy(p => Guid.NewGuid()).Select(a => new
                    {
                        Id = a.Id,
                        Media = new
                        {
                            Image = (a.ImgMediaId != null) ? a.ImgMediaId.GetUrlGuiId(host) : null
                        }
                    }),
                };

                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                       new ResultResponse<object>(true, "success", new { Qestion = model }));
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                  new ResultResponse<object>(false, "erorr", new { Message = e.Message }));
                return Ok(_response);
            }

        }

        //[Route("get-tuduy-text/{questionId}")]
        //public async Task<IHttpActionResult> GetTuDuyText(Guid questionId)
        //{
        //    var host = HttpContext.Current.Request.Url.Host;
        //    object _response;
        //    string ip = HttpContext.Current.Request.UserHostAddress;
        //    try
        //    {

        //        var _result = await _questionApp.GetbyIdAsync(questionId);
        //        var model = new
        //        {
        //            QuestionID = _result.Id,
        //            TextAnswers = _result.Answers.Where(a => a.IsDelete != true).OrderBy(p => Guid.NewGuid()).Select(a => new
        //            {
        //                Id = a.Id,
        //                AnswerText = a.Name
        //            }),
        //            MediaAnswers = _result.Answers.Where(a => a.IsDelete != true).OrderBy(p => Guid.NewGuid()).Select(a => new
        //            {
        //                Id = a.Id,
        //                Media = new
        //                {
        //                    Image = (a.ImgMediaId != null) ? a.ImgMediaId.GetUrlGuiId(host) : null
        //                }
        //            }),
        //        };

        //        _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
        //               new ResultResponse<object>(true, "success", new { Answer = model }));
        //        return Ok(_response);
        //    }
        //    catch (Exception e)
        //    {
        //        _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
        //          new ResultResponse<object>(false, "erorr", new { Message = e.Message }));
        //        return Ok(_response);
        //    }

        //}

        //[Route("get-tuduy-image/{questionId}")]
        //public async Task<IHttpActionResult> GetTuDuyImage(Guid questionId)
        //{
        //    var host = HttpContext.Current.Request.Url.Host;
        //    object _response;
        //    string ip = HttpContext.Current.Request.UserHostAddress;
        //    try
        //    {

        //        var _result = await _questionApp.GetbyIdAsync(questionId);
        //        var model = new
        //        {
        //            QuestionID = _result.Id,
        //            Answers = _result.Answers.Where(a => a.IsDelete != true).OrderBy(p => Guid.NewGuid()).Select(a => new
        //            {
        //                Id = a.Id,
        //                MediaQuestion = new
        //                {
        //                    Image = (a.ImgMediaId != null) ? a.ImgMediaId.GetUrlGuiId(host) : null
        //                }
        //            }),
        //        };

        //        _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
        //               new ResultResponse<object>(true, "success", new { Question = model }));
        //        return Ok(_response);
        //    }
        //    catch (Exception e)
        //    {
        //        _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
        //          new ResultResponse<object>(false, "erorr", new { Message = e.Message }));
        //        return Ok(_response);
        //    }

        //}
        [HttpPost]
        [Route("tuduy-result-submit")]
        [CheckModelForNull]
        [InvalidModelStateFilter]
        public async Task<IHttpActionResult> SubmitExam(PostAnswerTuDuy model)
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
                    VocabularyResult thi = new VocabularyResult();
                    thi.Id = Guid.NewGuid();
                    thi.QuestionId = model.QuestionId;
                    thi.StudenId = Guid.Parse(studentId);
                    thi.WordAnswerId = item.WordId;
                    thi.ImageAnswerId = item.ImgId;
                    SearchAnswerDto searchModel = new SearchAnswerDto(thi.StudenId.Value, thi.QuestionId.Value,thi.WordAnswerId.Value);
                    var _checkExist = await _questionApp.GetVocabularyResult(searchModel);

                    if (_checkExist.Count == 0)
                    {
                        //insert
                        _result = await _questionApp.CreateVocabularyResult(thi);
                    }
                    else
                    {
                        thi.Student = null;
                        thi.Id = _checkExist[0].Id;

                        _result = await _questionApp.UpdateVocabularyResult(thi);
                    }
                }     
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
        [HttpPost]
        [CheckModelForNull]
        [InvalidModelStateFilter]
        [Route("get-list")]
        public async Task<IHttpActionResult> GetListAsync(SearchQuestionModel search)
        {
            var host = HttpContext.Current.Request.Url.Host;
            object _response;
            string ip = HttpContext.Current.Request.UserHostAddress;
            try
            {

                SearchQuestionDto searchQuestionDto = new SearchQuestionDto();
                searchQuestionDto.QuestionTypeId = search.QuestionType;
                searchQuestionDto.SubjectId = search.SubjectId;
                searchQuestionDto.type = "tuvung";
                searchQuestionDto.Status = true;
                var _resutl = await _questionApp.GetListAsync(searchQuestionDto);
                object model = _resutl.OrderBy(a => a.Index).Where(a => a.WordId != null).Select(q => new
                {
                    Id = q.Id,
                    QuestionDe = q.NameDe,
                    QuestionVi = q.NameVi,
                    QuestionFormat = FormatQuestionMeDia.GetExtension(q.FormatQuestion.ToString()),
                    MediaQuestion = new
                    {
                        Audio = (q.WordId != null && q.Word.MediaAudioId != null) ? q.Word.MediaAudioId.GetUrlGuiId(host) : null,
                        Image = (q.WordId != null && q.Word.MediaImgId != null) ? q.Word.MediaImgId.GetUrlGuiId(host) : null
                    },
                    Answers = q.Answers.Where(a => a.IsDelete != true).Select(a => new
                    {
                        Id = a.Id,
                        AnswerVi = a.Word.WordVi,
                        AnswerDe = a.Word.WorDe,
                        ParentAnswer = "",
                        IsTrue = a.IsTrue ?? false
                    }),
                });
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                       new ResultResponse<object>(true, "success", new { Questions = model }));
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                  new ResultResponse<object>(false, "erorr", new { Message = e.Message }));
                return Ok(_response);
            }

        }
        [Route("get/{questionId}")]
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
                    //QuestionDe = _resutl.NameDe,
                    //QuestionVi = _resutl.NameVi,
                    QuestionFormat = FormatQuestionMeDia.GetExtension(_resutl.FormatQuestion.ToString()),
                    MediaQuestion = new
                    {
                        Audio = (_resutl.WordId != null && _resutl.Word.MediaAudioId != null) ? _resutl.Word.MediaAudioId.GetUrlGuiId(host) : null,
                        Image = (_resutl.WordId != null && _resutl.Word.MediaImgId != null) ? _resutl.Word.MediaImgId.GetUrlGuiId(host) : null
                    },
                    Answers = _resutl.Answers.Where(a => a.IsDelete != true).Select(a => new
                    {
                        Id = a.Id,
                        AnswerVi = a.Word.WordVi,
                        AnswerDe = a.Word.WorDe,
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
                  new ResultResponse<object>(false, "erorr", new { Message = e.Message }));
                return Ok(_response);
            }

        }

        [HttpPost]
        [CheckModelForNull]
        [InvalidModelStateFilter]
        [Route("update-correct-answer")]
        public async Task<IHttpActionResult> RequesAnswerVocabulary(SearchAnswerModel model)
        {
            string ip = HttpContext.Current.Request.UserHostAddress;
            object _response;
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
                         new ResultResponse<object>(true, "success", new { me }));
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
        [HttpPost]
        [CheckModelForNull]
        [InvalidModelStateFilter]
        [Route("fill-words")]
        public async Task<IHttpActionResult> RequestAnswer(PostAsnwerModel model)
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
                    vocabularyResult.StudentWord = model.YourWord;
                    vocabularyResult.QuestionId = model.QuestionId;
                    vocabularyResult.StudenId = Guid.Parse(studentId);
                    var me = await this.CheckIsTrueAsync(model.YourWord, model.QuestionId);
                    vocabularyResult.Status = me.Status;
                    var create = await _questionApp.CreateVocabularyResult(vocabularyResult);
                    me.Note = "You just made this sentence";
                    _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                         new ResultResponse<object>(true, "success", new { Message = me }));
                    return Ok(_response);

                }
                else
                {
                    var me = await this.CheckIsTrueAsync(model.YourWord, model.QuestionId);
                    if (data.StudentWord == model.YourWord && me.Status == false)
                    {
                        me.Note = "You did it wrong this time, but did it right the previous time";
                        _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                        new ResultResponse<object>(true, "success", new { Message = me }));
                        return Ok(_response);
                    }
                    else
                    if (data.StudentWord == model.YourWord && me.Status == true)
                    {
                        me.Note = "Great! Last time you did the right thing and the same goes for now";
                        _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                        new ResultResponse<object>(true, "success", new { Message = me }));
                        return Ok(_response);
                    }
                    else
                    if (me.Status == true)
                    {
                        data.Status = me.Status;
                        data.StudentWord = model.YourWord;
                        me.Note = "Update results!";
                        var update = await _questionApp.UpdateVocabularyResult(data);
                        _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                       new ResultResponse<object>(true, "success", new { Message = me }));
                        return Ok(_response);
                        // cập nhật kết quả
                    }
                    else
                    {
                        me.Note = "Don't update result !";
                        _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                       new ResultResponse<object>(true, "success", new { Message = me }));
                        return Ok(_response);
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
        private async Task<Mesages> CheckIsTrueAsync(string YourWord, Guid QuestionId)
        {
            Mesages mesages = new Mesages();
            var question = await _questionApp.GetbyIdAsync(QuestionId);
            var _answer = question.Answers.Where(a => a.IsTrue == true).FirstOrDefault();
            if (_answer.Word.WorDe.ToLower() == YourWord.ToLower())
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
    public class Mesages
    {
        public bool Status { set; get; }
        public string Message { set; get; }
        public string Note { set; get; }
    }
}
