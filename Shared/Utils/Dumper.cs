using System.Text.Json;

namespace Torico.Shared.Utils;

public class Dumper
{
    public static void dump(object obj)
    {
        Console.WriteLine(JsonSerializer.Serialize(obj));
    }
}
