using System.Globalization;

namespace segParAgustinSantinaqueCliente.Helpers;

public class LeerDelTeclado
{
    public static string LeerString(string mensaje)
    {
        Console.WriteLine(mensaje);
        string input = Console.ReadLine();
        return string.IsNullOrEmpty(input) ? null : input;
    }

    public static int? LeerInt(string mensaje)
    {
        Console.Write(mensaje);
        string input = Console.ReadLine();
        if (int.TryParse(input, out int result))
        {
            return result;
        }

        return null;
    }

    public static DateTime? LeerFecha(string mensaje)
    {
        Console.Write(mensaje);
        string input = Console.ReadLine();
        if (DateTime.TryParseExact(input, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out DateTime result))
        {
            return result;
        }

        return null;
    }
}