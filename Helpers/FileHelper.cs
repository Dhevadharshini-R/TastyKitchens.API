using System.Text.Json;

namespace TastyKitchens.API.Helpers;

public static class FileHelper
{
    public static List<T> ReadFromFile<T>(string path)
    {
        if (!File.Exists(path))
        {
            return new List<T>();
        }

        var json = File.ReadAllText(path);

        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<T>();
        }

        return JsonSerializer.Deserialize<List<T>>(json, new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
}) ?? new List<T>();
    }

    public static void WriteToFile<T>(string path, List<T> data)
    {
        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(path, json);
    }
}