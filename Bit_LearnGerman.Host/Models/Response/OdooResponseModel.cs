namespace Oauth_2._0_v2.Models.Response
{
    public class OdooResponseModel<TContent>
    {
       

        public string requestId { get; set; }
        public string clientId { get; set; }
        public ResultResponse<TContent> result { get; set; }
        public OdooResponseModel()
        {

        }
        public OdooResponseModel(string requestId, string clientId, ResultResponse<TContent> result)
        {
            this.requestId = requestId;
            this.clientId = clientId;
            this.result = result;
        }

    }
    public class ResultResponse<TContent>
    {
        public ResultResponse(bool status, string message, TContent data)
        {
            this.status = status;
            this.message = message;
            this.data = data;
        }

        public bool status { get; set; }
        public string message { get; set; }
        public TContent data { get; set; }
    }
}