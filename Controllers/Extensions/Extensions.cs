using MockServerApi.Model;
using Newtonsoft.Json;

namespace MockServerApi.Controllers.Extensions;

public static class Extensions
{
    public static string BuildGetExpectationJson(this Expectation expectation)
    {
        var expectationObj = new
        {
            httpRequest = new
            {
                method = expectation.Method,
                path = expectation.Url
            },
            httpResponse = new
            {
                statusCode = expectation.ResponseStatus,
                body = expectation.ResponseBody,
                headers = new
                {
                    ContentType = new[] { "application/json" }
                }
            }
        };

        return JsonConvert.SerializeObject(expectationObj);
    }

    public static string BuildPostExpectationJson(this Expectation expectation)
    {
        var expectationObj = new
        {
            httpRequest = new
            {
                method = expectation.Method,
                path = expectation.Url,
                body = new
                {
                    type = "JSON",
                    json = expectation.RequestBody,
                    contentType = "application/json",
                    matchType = "STRICT"
                }
            },
            httpResponse = new
            {
                statusCode = expectation.ResponseStatus,
                body = expectation.ResponseBody,
                headers = new
                {
                    ContentType = new[] { "application/json" }
                }
            }
        };

        return JsonConvert.SerializeObject(expectationObj);
    }
}