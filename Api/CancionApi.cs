using System.Net;
using Newtonsoft.Json;
using RestSharp;
using segParAgustinSantinaqueCliente.Dto.Cancion;
using segParAgustinSantinaqueCliente.Helpers;

namespace segParAgustinSantinaqueCliente.Api;

public class CancionApi : ApiBase
{
    public static async Task BuscarCancion()
    {

        var nombreCancion = LeerDelTeclado.LeerString("Ingrese el nombre de la cancion (opcional): ");
        var banda = LeerDelTeclado.LeerString("Ingrese el nombre de la banda (opcional): ");
        var duracionCancion = LeerDelTeclado.LeerInt("Ingrese la duracion de la cancion en segundos (opcional): ");

        var request = new RestRequest("/Cancion/BuscarCanciones");
        if (!string.IsNullOrEmpty(nombreCancion))
            request.AddParameter("nombreCancion", nombreCancion);

        if (!string.IsNullOrEmpty(banda))
            request.AddParameter("banda", banda);

        if (duracionCancion.HasValue)
            request.AddParameter("duracionCancion", duracionCancion.Value);

        try
        {
            var response = await _restClient.ExecuteAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.Clear();
                Console.WriteLine("Estos son los discos encontrados: ");
                Console.WriteLine("==============================");
                List<CancionGetDto> canciones = JsonConvert.DeserializeObject<List<CancionGetDto>>(response.Content);
                foreach (CancionGetDto cancion in canciones)
                {
                    Console.WriteLine($"Nombre Cancion: {cancion.NombreCancion}");
                    Console.WriteLine($"Banda: {cancion.Banda}");
                    Console.WriteLine($"Genero disco: {cancion.GeneroDelDisco}");
                    Console.WriteLine($"Fecha de lanzamiento del disco: {cancion.FechaLanzamientoDelDisco}");
                    Console.WriteLine("==============================");
                }
            }
            else
            {
                Console.WriteLine("No se encontro disco con esos parametros");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}