using System.Net;
namespace Oauth_2._0_v2.Handle
{
    public class RequestSLL
    {
        public static void SSL()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls 
                |SecurityProtocolType.Tls11 
                | SecurityProtocolType.Tls12 
                | SecurityProtocolType.Ssl3;
        }
    }
}