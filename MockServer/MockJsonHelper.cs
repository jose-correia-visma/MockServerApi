using Newtonsoft.Json;

namespace MockServerApi.MockServer;

public static class MockJsonHelper
{
    // Parses a JSON string into an object that can be used as an expectation by the MockServerClient.
    public static object BuildMockObjFromStr(string jsonString)
    {
        // For simplicity, we are assuming that the expectation object is represented as a generic dictionary.
        // This can be adjusted to match the actual structure expected by your MockServer API.
        return JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
    }
}