using ServiceXpert.Web.Constants;
using System.Text;
using NewtonsoftJson = Newtonsoft.Json;

namespace ServiceXpert.Web.Utils;
public static class HttpContentUtil
{
    public static StringContent SerializeContentWithApplicationJson(object value) =>
        new(NewtonsoftJson.JsonConvert.SerializeObject(value), Encoding.UTF8, HttpMediaType.ApplicationJson);

    public static async Task<T?> DeserializeContentAsync<T>(HttpResponseMessage response) =>
        NewtonsoftJson.JsonConvert.DeserializeObject<T>(await GetResultAsStringAsync(response));

    public static async Task<string> GetResultAsStringAsync(HttpResponseMessage response) =>
        await response.Content.ReadAsStringAsync();
}
