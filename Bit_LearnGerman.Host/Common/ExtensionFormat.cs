using System;

namespace Oauth_2._0_v2.Common
{
    public static class ExtensionFormat
    {
        public static string GetUrlGuiId(this Guid? Id, string host)
        {
            if(Id== null)
            {
                return "";
            }    
            return host+ "/getfile?sub=mediaquery&Id=" + Id;
        }
    }
}