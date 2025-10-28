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

    public class Datum
    {
        public int id { get; set; }
        public string organisationName { get; set; }
        public string organisationTin { get; set; }
        public string status { get; set; }
        public string email { get; set; }
    }

    public class GetOrganisationResponse : Response
    {
        public List<Datum> data { get; set; }
    }
    public class DepartmentDatum
    {
        public int id { get; set; }
        public string departmentName { get; set; }
        public string description { get; set; }
        public string status { get; set; }
    }

    public class GetDepartmentResponse:Response
    {
        public List<DepartmentDatum> data { get; set; }
    }
    public class RegisterEmployeeResponse :Response
    {

    }


}
