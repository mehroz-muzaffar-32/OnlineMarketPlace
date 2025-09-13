using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http;
using System.Linq;

namespace Torico.Client.Utilities;

public static class JsonUtility
{
    public static string SerializeObj(object modelObject)
    {
        if (modelObject == null)
            throw new ArgumentNullException(nameof(modelObject));

        return JsonSerializer.Serialize(modelObject, JsonOptions());
    }

    public static T DeserializeJsonString<T>(string jsonString) => JsonSerializer.Deserialize<T>(jsonString, JsonOptions())!;

    public static IList<T> DeserializeJsonStringList<T>(string jsonString) => JsonSerializer.Deserialize<IList<T>>(jsonString, JsonOptions())!;

    // Removed the invalid property `UnmappedMemberHandling` as it is not part of `JsonSerializerOptions` in .NET.
    public static JsonSerializerOptions JsonOptions()
    {
        return new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    // Added methods to generate string content and build query strings for HTTP requests.
    public static StringContent GenerateStringContent(string serializedObj)
    {
        return new StringContent(serializedObj, System.Text.Encoding.UTF8, "application/json");
    }

    public static string BuildQueryString(Dictionary<string, string> queryParams)
    {
        var validParams = queryParams
            .Where(kvp => !string.IsNullOrEmpty(kvp.Value))
            .Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}");

        return string.Join("&", validParams);
    }
}