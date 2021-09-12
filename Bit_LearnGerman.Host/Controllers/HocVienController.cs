using DE_APPLICATION_ELEANING.AccountApp;
using DE_APPLICATION_ELEANING.GrammarApp;
using DE_APPLICATION_ELEANING.QuestionApp;
using DE_APPLICATION_ELEANING.QuestionApp.Dto;
using DE_APPLICATION_ELEANING.StudentApp;
using DE_APPLICATION_ELEANING.StudentApp.Dto;
using DE_APPLICATION_ELEANING.ThiResultApp;
using DE_APPLICATION_ELEANING.ThiResultApp.Dto;
using DE_DB_ELEANING.AFModel;
using DE_DB_ELEANING.Model;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Oauth_2._0_v2.Author.GeToken;
using Oauth_2._0_v2.Common;
using Oauth_2._0_v2.Handle;
using Oauth_2._0_v2.Models;
using Oauth_2._0_v2.Models.Response;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
namespace Oauth_2._0_v2.Controllers
{
    [RoutePrefix("bit_sol/api/v1")]
    public class HocVienController : BaseController
    {
        private readonly AccountApplication _accountApp;
        private readonly StudentApplication _studentApp;
        private readonly GrammarApplication _grammarApp;
        private readonly QuestionApplication _questionApp;

        private readonly ThiResultApplication _thiResultApp;
        public HocVienController()
        {
            if (_questionApp == null)
            {
                _questionApp = new QuestionApplication();
            }
            if (_grammarApp == null)
            {
                _grammarApp = new GrammarApplication();
            }
            if (_thiResultApp == null)
            {
                _thiResultApp = new ThiResultApplication();
            }
            if (_accountApp == null)
            {
                _accountApp = new AccountApplication();
            }
            if (_studentApp == null)
            {
                _studentApp = new StudentApplication();
            }
        }
        //[HttpPost]
        //[AllowAnonymous]
        //[Route("get-token")]
        //public async Task<IHttpActionResult> GetToken(TokenRequest request)
        //{
        //    try
        //    {
        //        var content = new FormUrlEncodedContent(new[]
        //            {
        //            new KeyValuePair<string, string>("client_id", request.client_id),
        //            new KeyValuePair<string, string>("grant_type", request.grant_type),
        //            new KeyValuePair<string, string>("username", request.Username),
        //            new KeyValuePair<string, string>("password", request.Password)
        //             });

        //        var client = new HttpClient()
        //        {
        //            BaseAddress = new Uri(WebConfigurationManager.AppSettings["URIAUTH"])
        //        };
        //        RequestSLL.SSL();

        //        var result = await client.PostAsync("/bit_sol/api/v1/sign_in", content);
        //        string resultContent = await result.Content.ReadAsStringAsync();
        //        resultContent = resultContent.Replace(".issued", "issued").Replace(".expires", "expires");
        //        object tokenResponse = JsonConvert.DeserializeObject<object>(resultContent);

        //        return Ok(tokenResponse);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.Message);
        //    }

        //}
        //[HttpPost]
        //[AllowAnonymous]
        //[Route("test-token")]
        //public async Task<IHttpActionResult> TestToken(TokenRequest request)
        //{
        //    try
        //    {
        //        var content = new FormUrlEncodedContent(new[]
        //            {
        //            new KeyValuePair<string, string>("client_id", request.client_id),
        //            new KeyValuePair<string, string>("grant_type", request.grant_type),
        //            new KeyValuePair<string, string>("username", request.Username),
        //            new KeyValuePair<string, string>("password", request.Password)
        //             });

        //        var client = new HttpClient()
        //        {
        //            BaseAddress = new Uri(WebConfigurationManager.AppSettings["URIAUTH"])
        //        };
        //        return Ok("ok");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.Message);
        //    }

        //}
        //[HttpPost]
        //[AllowAnonymous]
        //[Route("refresh-token")]
        //public async Task<IHttpActionResult> Refresh_token(RefreshToken request)
        //{
        //    try
        //    {
        //        var content = new FormUrlEncodedContent(new[]
        //   {
        //    new KeyValuePair<string, string>("client_id", request.client_id),
        //    new KeyValuePair<string, string>("grant_type", request.grant_type),
        //    new KeyValuePair<string, string>("refresh_token", request.refresh_token),
        //});
        //        var client = new HttpClient()
        //        {
        //            BaseAddress = new Uri(WebConfigurationManager.AppSettings["URIAUTH"])
        //        };
        //        RequestSLL.SSL();
        //        var result = await client.PostAsync("/bit_sol/api/v1/sign_in", content);
        //        string resultContent = await result.Content.ReadAsStringAsync();
        //        resultContent = resultContent.Replace(".issued", "issued").Replace(".expires", "expires");
        //        object tokenResponse = JsonConvert.DeserializeObject<object>(resultContent);

        //        return Ok(tokenResponse);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.Message);
        //    }

        //}
        [HttpPost]
        [CheckModelForNull]
        [InvalidModelStateFilter]
        [Route("sign-up")]
        public async Task<IHttpActionResult> SignUp(SingupModel singup)
        {
            object _response;
            string ip = HttpContext.Current.Request.UserHostAddress;
            if (ModelState.IsValid)
            {
                try
                {
                    if (singup.PassWord != singup.RePassWord)
                    {
                        _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip, new ResultResponse<object>(true, "failse", new { mesages = "Imported passwords do not match!" }));
                        return Ok(_response);
                    }
                    else
                    {
                        if (_studentApp.GetByEmail(singup.EmailAddress).Result == null && _studentApp.GetByPhone(singup.PhoneNumber).Result == null)
                        {
                            Account account = new Account();
                            account.Username = Guid.NewGuid().ToString();
                            account.Password = singup.PassWord;
                            var checkAcc = await _accountApp.CreateAsync(account);
                            var acc = await _accountApp.GetByUserName(account.Username);
                            Student _model = new Student();
                            _model.Name = singup.FullName;
                            _model.Phone = singup.PhoneNumber;
                            _model.Email = singup.EmailAddress;
                            _model.Facebook = singup.LinkFacebook;
                            _model.AccountId = acc.Id;
                            var check = await _studentApp.CreateAsync(_model);
                            //AppUser user = new AppUser();
                            //user.UserName = acc.Username;
                            //user.DisplayName = singup.FullName;
                            //var result = await _userManager.CreateAsync(user, "1234");
                            //var roleResult = await _userManager.AddToRoleAsync(user, "user");
                            if (check == true)
                            {
                                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(), new ResultResponse<object>(true, "success", new { message = "Sign Up Success !!" }));
                                return Ok(_response);
                            }
                            else
                            {
                                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(), new ResultResponse<object>(false, "error", new { message = "failse!" }));
                                return Ok(_response);
                            }

                        }
                        else
                        {
                            _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(), new ResultResponse<object>(false, "error", new { message = "Account already exists!" }));
                            return Ok(_response);
                        }

                    }
                }
                catch (Exception e)
                {
                    _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                      new ResultResponse<object>(false, "erorr", new { message = e.Message }));
                    return Ok(_response);
                }


            }
            return BadRequest();
        }
        [Authorize]
        [HttpPost]
        [Route("student-search")]
        public async Task<IHttpActionResult> SearchStudent(string key)
        {
            string host = HttpContext.Current.Request.Url.Host;
            object _response;
            object data;
            List<Student> resutl = new List<Student>();
            string ip = HttpContext.Current.Request.UserHostAddress;
            try
            {
                StudenSearchDto studenSearchDto = new StudenSearchDto();
                studenSearchDto.Name = key;
                resutl = await _studentApp.GetListAsync(studenSearchDto);
                if(resutl != null && resutl.Count !=0)
                {
                    data = resutl.Select(a => new
                    {
                        Id = a.Id,
                        Username = a.Account.Username,
                        Name = a.Name,
                        AvatarUrl = _accountApp.GetPhoByUserName(a.Account.Username).Url ?? "https://" + host + "/Img/Avantar/user.png",

                    });
                    _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                   new ResultResponse<object>(true, "success", new { students = data }));
                    return Ok(_response);
                }    
                else
                {
                    studenSearchDto.Name = null;
                    studenSearchDto.Phone = key;
                    resutl = await _studentApp.GetListAsync(studenSearchDto);
                    if(resutl != null && resutl.Count !=0)
                    {
                        data = resutl.Select(a => new
                        {
                            Id = a.Id,
                            Username = a.Account.Username,
                            Name = a.Name,
                        });
                        _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                      new ResultResponse<object>(true, "success", new { students = data }));
                        return Ok(_response);
                    }
                    else
                    {
                        data = new object();
                        _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                      new ResultResponse<object>(true, "success", new { students = data }));
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
        [Authorize]
        [HttpPost]
        [Route("student-list")]
        public async Task<IHttpActionResult> SearchStudent(int? page)
        {
            string host = HttpContext.Current.Request.Url.Host;
            object _response;
            object data;
            int pageNumber = (page != null && page.Value > 0) ? page.Value : 1;
            List<Student> resutl = new List<Student>();
            string ip = HttpContext.Current.Request.UserHostAddress;
            try
            {
                StudenSearchDto studenSearchDto = new StudenSearchDto();
                var TotalPage = resutl.Count % 20 == 0 ? resutl.Count % 20 : (resutl.Count % 20) + 1;
                resutl = await _studentApp.GetListAsync(studenSearchDto);
                var _result = resutl.ToPagedList(pageNumber, 20);
                data = _result.Select(a => new
                {
                    Id = a.Id,
                    Username = a.Account.Username,
                    Name = a.Name,
                    AvatarUrl = _accountApp.GetPhoByUserName(a.Account.Username).Url ?? "https://" + host + "/Img/Avantar/user.png",

                });
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
               new ResultResponse<object>(true, "success", new { students = data}));
                return Ok(_response);


            }
            catch (Exception e)
            {
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                 new ResultResponse<object>(false, "erorr", new { Message = e.Message }));
                return Ok(_response);
            }
        }
        [Authorize]
        [HttpPost]
        [Route("student-info")]
        public async Task<IHttpActionResult> Info()
        {
            string host = HttpContext.Current.Request.Url.Host;

            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var studentId = principal.Claims.Where(c => c.Type == "ID").Single().Value;

            object _response;
            var result = await _studentApp.GetbyIdAsync(Guid.Parse(studentId));

            SearchThiResult searchThiResult = new SearchThiResult();
            searchThiResult.StudentID = Guid.Parse(studentId);
            var _resultThi =  _thiResultApp.GetListAsync(searchThiResult).Result.GroupBy(p=>p.DeThiID).Select(p=>p.First()).Count();


            SearchAnswerDto searchAnswerDto = new SearchAnswerDto();           
            searchAnswerDto.StudenId = Guid.Parse(studentId);
            searchAnswerDto.Type = "result";

            var _resultGrammar =  _questionApp.GetVocabularyResult(searchAnswerDto).Result.GroupBy(p=>p.Question.Grammar_TheoryId).Select(p=>p.First()).Count();
            //var _resultGrammar = _grammarApp.GetListAsync();

            string ip = HttpContext.Current.Request.UserHostAddress;
            try
            {
                object model = new
                {
                    FullName = result.Name,
                    Facebook = result.Facebook,
                    Username = result.Account.Username,
                    Email = result.Email,
                    Phone = result.Phone,
                    AvatarUrl = _accountApp.GetPhoByUserName(result.Account.Username).Url ?? "https://" + host + "/Img/Avantar/user.png",
                    DeThiQTY = _resultThi,
                    NguPhapQTY = _resultGrammar
                };
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(), new ResultResponse<object>(true, "success", new { student = model }));
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),
                  new ResultResponse<object>(false, "erorr", new { Message = e.Message }));
                return Ok(_response);
            }
        }
        [Authorize]
        [HttpPost]
        [CheckModelForNull]
        [InvalidModelStateFilter]
        [Route("student-update")]
        public async Task<IHttpActionResult> Update(UpdateAccountModel student)
        {
            object _response;
            string ip = HttpContext.Current.Request.UserHostAddress;
            if (ModelState.IsValid)
            {
                try
                {
                    ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                    var studentId = principal.Claims.Where(c => c.Type == "ID").Single().Value;
                    var studentFix = await _studentApp.GetbyIdAsync(Guid.Parse(studentId));
                    var accountFix = await _accountApp.GetbyIdAsync(studentFix.AccountId.Value);
                    var ivalidAccoutEmail = await _studentApp.GetByEmail(student.EmailAddress);
                    var ivalidAccoutPhone = await _studentApp.GetByPhone(student.PhoneNumber);
                    if (student == null)
                    {
                        _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(), new ResultResponse<object>(true, "success", new { Message = "Don't have any change!" }));
                        return Ok(_response);
                    }
                    if (studentFix.Email == student.EmailAddress || ivalidAccoutEmail == null)
                    {
                        if (ivalidAccoutPhone == null || studentFix.Phone == student.PhoneNumber)
                        {
                            if (accountFix.Password != student.OldPassWord && student.OldPassWord != null)
                            {
                                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(), new ResultResponse<object>(false, "error", new { Message = "Old password is incorrect!" }));
                                return Ok(_response);
                            }
                            accountFix.Password = student.NewPassWord ?? accountFix.Password;
                            studentFix.Name = student.FullName ?? studentFix.Name;
                            studentFix.Phone = student.PhoneNumber ?? studentFix.Phone;
                            studentFix.Email = student.EmailAddress ?? studentFix.Email;
                            studentFix.Facebook = student.LinkFacebook ?? studentFix.Facebook;
                            var checkAcc = await _accountApp.UpdateAsync(accountFix);
                            var check = await _studentApp.UpdateAsync(studentFix);
                            if (check == true)
                            {
                                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(), new ResultResponse<object>(true, "success", new { Message = "Save the information successfully!" }));
                                return Ok(_response);
                            }
                            else
                            {
                                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(), new ResultResponse<object>(true, "error", new { Message = "failse!" }));
                                return Ok(_response);
                            }
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
            return BadRequest();
        }
        [Authorize]
        [HttpPost]
        [Route("update-avantar")]
        public async Task<IHttpActionResult> Image()
        {
            object _response;
            string ip = HttpContext.Current.Request.UserHostAddress;
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var studentId = principal.Claims.Where(c => c.Type == "ID").Single().Value;
                string host = HttpContext.Current.Request.Url.Host;
                string _path = "~/Img/Avantar/" + studentId;
                var map = HttpContext.Current.Server.MapPath(_path);
                if (!System.IO.Directory.Exists(_path))
                {
                    Directory.CreateDirectory(map);
                    var httpRequest = HttpContext.Current.Request;
                    if (httpRequest.Files.Count > 0)
                    {
                        var docfiles = new List<string>();
                        foreach (string file in httpRequest.Files)
                        {
                            string name = DateTime.Now.ToString();
                            var postedFile = httpRequest.Files[file];
                            string extension = Path.GetExtension(postedFile.FileName);
                            if (String.Compare(extension, ".jpg", true) == 0 || String.Compare(extension, ".png", true) == 0 || String.Compare(extension, ".jpeg", true) == 0)
                            {
                                var newFileName = Guid.NewGuid() + extension;
                                var filePath = HttpContext.Current.Server.MapPath(_path + "/" + newFileName);
                                postedFile.SaveAs(filePath);
                                docfiles.Add(filePath);

                                var _studen = await _studentApp.GetbyIdAsync(Guid.Parse(studentId));
                                var check = _accountApp.UpdatePhoto(_studen.Account.Username, "https://" + host + _path.Replace("~","")+ "/"+ newFileName);
                                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(),new ResultResponse<object>(true, "success", new { Message = "https://" + host + _path.Replace("~", "") +"/" + newFileName }));
                                return Ok(_response);
                            }
                        }

                    }
            
                }
                _response = new OdooResponseModel<object>(Guid.NewGuid().ToString(), ip.Base64UrlEncode(), new ResultResponse<object>(false, "erorr", new { Message = "" }));
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
