using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oauth_2._0_v2.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection f)
        {
            string usermane = f["uname"].ToString();
            string password = f["psw"].ToString();
            if(usermane== "Panda" && password =="Panda")
            {
                HttpCookie myCookie = new HttpCookie("Auth");
                myCookie.Values.Add("userid", "3a63ccc420c36c8b288382400c838cf6");
                myCookie.Expires = DateTime.Now.AddHours(24);
                Response.Cookies.Add(myCookie);
                return Redirect("/swagger");
            }
            return View();
        }
    }
}