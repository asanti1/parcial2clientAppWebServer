using Newtonsoft.Json;
using RestSharp;

namespace segParAgustinSantinaqueCliente.Api;

public class AuthApi : ApiBase
{
    public static async Task Login()
    {
        var request = new RestRequest("/Login");
        try
        {
            request.AddJsonBody(new
                {
                    user = "johndoe",
                    pass = "123456"
                }
            );
            var response = await _restClient.ExecutePostAsync(request);
            if (response.IsSuccessful)
            {
                token = JsonConvert.DeserializeObject<string>(response.Content);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}