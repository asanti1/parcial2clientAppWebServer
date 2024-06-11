using RestSharp;

namespace segParAgustinSantinaqueCliente.Api;

public class ApiBase
{
    const string BaseUrl = "http://localhost:5186/api";
    public static string token = null;
    protected static readonly RestClient _restClient = new(BaseUrl);
}