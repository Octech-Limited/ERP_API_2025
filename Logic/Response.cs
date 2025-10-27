namespace ErpApi.Logic
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
    }
    public class GenerateTokenResponse : Response
    {
        public string Token { get; set; }
    }
    public class ValidateTokenResponse : Response
    {
        public bool IsValid { get; set; }
        public System.Security.Principal.IIdentity? Identity { get; set; }
    }
}
