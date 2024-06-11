using Newtonsoft.Json;
using RestSharp;
using segParAgustinSantinaqueCliente.Helpers;

namespace segParAgustinSantinaqueCliente.Api;

public class AuthApi : ApiBase
{
    public static async Task Login()
    {
        string user = LeerDelTeclado.LeerString("Ingrese su usuario");
        string pass = LeerDelTeclado.LeerString("Ingrese su contraseña");

        var request = new RestRequest("/Login");
        try
        {
            request.AddJsonBody(new
                {
                    user,
                    pass
                }
            );
            var response = await _restClient.ExecutePostAsync(request);
            if (response.IsSuccessful)
            {
                Console.WriteLine("Login Exitoso");
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