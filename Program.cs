using segParAgustinSantinaqueCliente.Api;

namespace segParAgustinSantinaqueCliente;

internal class Program
{
    public static async Task Main(string[] args)
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("=======================");
            Console.WriteLine("Bienvenido");
            Console.WriteLine("¿Qué deseas hacer hoy?");
            Console.WriteLine("1 - Login");
            Console.WriteLine("2 - Consultar los 5 discos más vendidos");
            Console.WriteLine("3 - Buscar disco");
            Console.WriteLine("4 - Buscar una canción");
            Console.WriteLine("5 - Modificar un disco por su SKU");
            Console.WriteLine("6 - Agregar un nuevo disco");
            Console.WriteLine("7 - Salir");
            Console.WriteLine("Elija una opción: ");
            Console.WriteLine("=======================");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    await AuthApi.Login();
                    break;
                case "2":
                    Console.Clear();
                    await DiscoApi.GetTopCincoDisco();
                    break;
                case "3":
                    Console.Clear();
                    await DiscoApi.BuscarDisco();
                    break;
                case "4":
                    Console.Clear();
                    await CancionApi.BuscarCancion();
                    break;
                case "5":
                    if (ApiBase.token == null)
                    {
                        Console.WriteLine("Primero debes logearte para usar esta opcion");
                        break;
                    }
                    Console.Clear();
                    await DiscoApi.ModificarDiscoPorSKU();
                    break;
                case "6":
                    if (ApiBase.token == null)
                    {
                        Console.WriteLine("Primero debes logearte para usar esta opcion");
                        break;
                    }
                    await DiscoApi.CrearDisco();
                    break;

                case "7":
                    Console.Clear();
                    exit = true;
                    Console.WriteLine("Saliendo...");
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Opción no válida, por favor intente de nuevo.");
                    break;
            }
        }
    }
}