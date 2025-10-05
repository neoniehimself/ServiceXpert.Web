using System.Net;

namespace ServiceXpert.Web.Extensions;
public static class HttpStatusCodeExtension
{
    public static int ToInt(this HttpStatusCode statusCode)
    {
        return (int)statusCode;
    }
}
