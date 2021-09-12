using Oauth_2._0_v2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oauth_2._0_v2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.Cookies["Auth"] != null && Request.Cookies["Auth"].ToString()== "userid=3a63ccc420c36c8b288382400c838cf6")
            {
                ViewBag.Title = "Home Page";

                return Redirect("/swagger");
            }
            var xr =  EncytCommon.Decrypt("ob3escvy5stefLDBwm5f5dMVaAskLkRlHQ84IBHo4rAElhF/rGd2mbQUCkNSX/5qR1jxuOZHhIzKpfXTMn4b1SRiao9pIkPYvua0tcbMw9UvIj7bY5nXkuRo+FKUPI6mlwrDjyqO4RyKTYmupQr45x0POCAR6OKwBJYRf6xndplhblZ1iAdd8cxJTMOpVai6fpB7PfHoDIUFp3oUB8Ai9y+UVWKQXO379ejOpjs2+kWd9uNPsxvh84eOaFhjBNBFLTYEXbJNj0qFZTT9B160F31v1qAXZT8Fzx/um7CAx6Tr4xagar3COZyj9vGrv1bdtHxnYjpXoH4rRwtRx/cgJzFXPO7ey0wOcPFgw65qnDuAcyE0WurV5Ul7N7Pl0wD3mJ1CfJj6HuUt5tvu+GwiUGy49Vlmqt++ETZxBG/bEdiScFZ/vj2Zp0vTHHJMn8/E");
           var xrs =  EncytCommon.Encrypt("Data Source=db869678066.hosting-data.io;Initial Catalog=db869678066;User Id=dbo869678066;Password=Zxcv@123123;MultipleActiveResultSets=True;App=EntityFramework;");
            ViewBag.Title = "Home Page";

            return Redirect("/Login");
        }
    }
}
