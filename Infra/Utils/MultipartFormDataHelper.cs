using System.Text.Json;

namespace Infra.Utils;

/// <summary>
///     Provides helper methods for creating multipart form data content.
/// </summary>
public static class MultipartFormDataHelper
{
    /// <summary>
    ///     Creates a <see cref="MultipartFormDataContent" /> object from the properties of a given object.
    /// </summary>
    /// <typeparam name="T">The type of the object containing the properties.</typeparam>
    /// <param name="data">The object from which to extract properties as form data.</param>
    /// <returns>A <see cref="MultipartFormDataContent" /> object containing the properties as form data.</returns>
    public static MultipartFormDataContent CreateMultipartContent<T>(this T data)
    {
        var multipartContent = new MultipartFormDataContent();

        foreach (var prop in typeof(T).GetProperties())
        {
            var value = prop.GetValue(data);
            if (value == null) continue;

            HttpContent content;

            switch (value)
            {
                case Stream streamValue:
                    content = new StreamContent(streamValue);
                    multipartContent.Add(content, prop.Name, prop.Name);
                    break;

                case byte[] byteArrayValue:
                    content = new ByteArrayContent(byteArrayValue);
                    multipartContent.Add(content, prop.Name, prop.Name);
                    break;


                case string stringValue:
                    content = new StringContent(stringValue);
                    multipartContent.Add(content, prop.Name);
                    break;

                case IEnumerable<KeyValuePair<string, string>> formUrlEncodedValue:
                    content = new FormUrlEncodedContent(formUrlEncodedValue);
                    multipartContent.Add(content, prop.Name);
                    break;

                case int intValue:
                case long longValue:
                case float floatValue:
                case double doubleValue:
                case decimal decimalValue:
                case bool boolValue:
                    // Handle primitive data types (int, long, float, double, decimal, bool)
                    content = new StringContent(value.ToString()!);
                    multipartContent.Add(content, prop.Name);
                    break;

                case ReadOnlyMemory<byte> readOnlyMemoryValue:
                    content = new ReadOnlyMemoryContent(readOnlyMemoryValue);
                    multipartContent.Add(content, prop.Name);
                    break;

                case HttpContent httpContentValue:
                    // If it's already an HttpContent, add it directly
                    multipartContent.Add(httpContentValue, prop.Name);
                    break;

                default:
                    // For other types, treat as JSON
                    var jsonString = JsonSerializer.Serialize(value);
                    content = new StringContent(jsonString);
                    multipartContent.Add(content, prop.Name);
                    break;
            }
        }

        return multipartContent;
    }
}