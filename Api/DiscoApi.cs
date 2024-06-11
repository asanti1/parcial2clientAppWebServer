using System.Net;
using Newtonsoft.Json;
using RestSharp;
using segParAgustinSantinaqueCliente.Dto.Cancion;
using segParAgustinSantinaqueCliente.Dto.Disco;
using segParAgustinSantinaqueCliente.Helpers;

namespace segParAgustinSantinaqueCliente.Api;

public class DiscoApi : ApiBase
{
    public static async Task GetTopCincoDisco()
    {
        var request = new RestRequest("/Disco/GetTopCinco");

        try
        {
            var response = await _restClient.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                List<DiscoGetDto> discos = JsonConvert.DeserializeObject<List<DiscoGetDto>>(response.Content);
                foreach (DiscoGetDto disc in discos)
                {
                    Console.WriteLine($"Titulo disco: {disc.TituloDisco}");
                    Console.WriteLine($"Banda: {disc.Banda}");
                    Console.WriteLine($"Cantidad de canciones: {disc.CantidadCanciones}");
                    Console.WriteLine($"Genero disco: {disc.Genero}");
                    Console.WriteLine($"Cantidad discos vendidos: {disc.CantidadVendida}");
                    Console.WriteLine($"Cantidad discos vendidos: {disc.FechaLanzamiento}");
                    Console.WriteLine("==============================");
                }
            }
            else
            {
                Console.WriteLine(response.ErrorMessage);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static async Task BuscarDisco()
    {
        var genero = LeerDelTeclado.LeerString("Ingrese el género del disco (opcional): ");
        var banda = LeerDelTeclado.LeerString("Ingrese el nombre de la banda (opcional): ");
        var cantidadVendida = LeerDelTeclado.LeerInt("Ingrese la cantidad de discos vendidos (opcional): ");
        var tituloDisco = LeerDelTeclado.LeerString("Ingrese el título del disco (opcional): ");

        var request = new RestRequest("/Disco/Buscar");

        if (!string.IsNullOrEmpty(genero))
            request.AddParameter("genero", genero);
        if (!string.IsNullOrEmpty(banda))
            request.AddParameter("banda", banda);
        if (cantidadVendida.HasValue)
            request.AddParameter("cantidadVendida", cantidadVendida.Value);
        if (!string.IsNullOrEmpty(tituloDisco))
            request.AddParameter("tituloDisco", tituloDisco);

        try
        {
            var response = await _restClient.ExecuteAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.Clear();
                Console.WriteLine("Estos son los discos encontrados: ");
                Console.WriteLine("==============================");
                List<DiscoGetDto> discos = JsonConvert.DeserializeObject<List<DiscoGetDto>>(response.Content);
                foreach (DiscoGetDto disc in discos)
                {
                    Console.WriteLine($"Titulo disco: {disc.TituloDisco}");
                    Console.WriteLine($"Banda: {disc.Banda}");
                    Console.WriteLine($"Cantidad de canciones: {disc.CantidadCanciones}");
                    Console.WriteLine($"Genero disco: {disc.Genero}");
                    Console.WriteLine($"Cantidad discos vendidos: {disc.CantidadVendida}");
                    Console.WriteLine($"Cantidad discos vendidos: {disc.FechaLanzamiento}");
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

    public static async Task ModificarDiscoPorSKU()
    {
        var sku = LeerDelTeclado.LeerString("Ingresa el sku del disco a modificar");
        var genero = LeerDelTeclado.LeerString("Ingrese el género del disco (opcional): ");
        var nombre = LeerDelTeclado.LeerString("Ingrese el nombre de la banda (opcional): ");
        var fechaLanzamiento = LeerDelTeclado.LeerFecha("Ingrese la nueva fecha en formato dia-mes-anio (opcional)");
        var cantidadVendida = LeerDelTeclado.LeerInt("Ingrese la cantidad de discos vendidos (opcional): ");
        var tituloDisco = LeerDelTeclado.LeerString("Ingrese el título del disco (opcional): ");

        var request = new RestRequest($"/Disco/ActualizarDisco/{sku}");
        var body =
            new
            {
                nombre,
                genero,
                cantidadVendida,
                tituloDisco,
                fechaLanzamiento
            };

        string jsonBody = JsonConvert.SerializeObject(body);
        request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);
        request.AddHeader("Authorization", $"Bearer {token}");
        try
        {
            var response = await _restClient.ExecutePutAsync(request);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                Console.Clear();
                DiscoGetDto disco = JsonConvert.DeserializeObject<DiscoGetDto>(response.Content);
                Console.WriteLine($"Se ha actualizado el disco con SKU: {sku}");
                Console.WriteLine("==============================");
                Console.WriteLine($"Titulo disco: {disco.TituloDisco}");
                Console.WriteLine($"Banda: {disco.Banda}");
                Console.WriteLine($"Cantidad de canciones: {disco.CantidadCanciones}");
                Console.WriteLine($"Genero disco: {disco.Genero}");
                Console.WriteLine($"Cantidad discos vendidos: {disco.CantidadVendida}");
                Console.WriteLine($"Cantidad discos vendidos: {disco.FechaLanzamiento}");
                Console.WriteLine("==============================");
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine($"No se encontro disco con sku: {sku}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }


    public static async Task CrearDisco()
    {
        string nombre = LeerDelTeclado.LeerString("Ingrese el nombre del disco: ");
        string banda = LeerDelTeclado.LeerString("Ingrese el nombre de la banda: ");
        DateTime fechaLanzamiento =
            LeerDelTeclado.LeerFecha("Ingrese la fecha de lanzamiento del disco (formato: dd-MM-yyyy): ")!.Value;
        string genero = LeerDelTeclado.LeerString("Ingrese el género del disco: ");
        int unidadesVendidas = LeerDelTeclado.LeerInt("Ingrese la cantidad de unidades vendidas: ")!.Value;
        string sku = LeerDelTeclado.LeerString("Ingrese el SKU del disco: ");

        var canciones = new List<CancionPostDto>();
        bool agregarOtraCancion = true;

        while (agregarOtraCancion)
        {
            string tituloCancion = LeerDelTeclado.LeerString("Ingrese el título de la canción: ");
            int duracionCancion = LeerDelTeclado.LeerInt("Ingrese la duración de la canción en segundos: ")!.Value;

            canciones.Add(new CancionPostDto { TituloCancion = tituloCancion, TiempoDuracion = duracionCancion });

            Console.Write("¿Desea agregar otra canción? (s/n): ");
            agregarOtraCancion = Console.ReadLine().ToLower() == "s";
        }

        var request = new RestRequest($"/Disco/AgregarDisco");

        var body = new
        {
            nombre,
            banda,
            fechaLanzamiento,
            genero,
            unidadesVendidas,
            sku,
            canciones
        };

        string jsonBody = JsonConvert.SerializeObject(body);
        request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);
        request.AddHeader("Authorization", $"Bearer {token}");
        try
        {
            var response = await _restClient.ExecutePostAsync(request);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                Console.Clear();
                DiscoPostDto disco = JsonConvert.DeserializeObject<DiscoPostDto>(response.Content);
                Console.WriteLine($"Se ha actualizado el disco con SKU: {sku}");
                Console.WriteLine("==============================");
                Console.WriteLine($"Titulo disco: {disco.Nombre}");
                Console.WriteLine($"Banda: {disco.Banda}");
                Console.WriteLine($"Fecha Lanzamiento del disco: {disco.FechaLanzamiento}");
                Console.WriteLine($"Genero disco: {disco.Genero}");
                Console.WriteLine($"Cantidad discos vendidos: {disco.UnidadesVendidas}");
                Console.WriteLine($"SKU del disco: {disco.SKU}");
                Console.WriteLine("==============================");
                Console.WriteLine("Canciones en el disco");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}