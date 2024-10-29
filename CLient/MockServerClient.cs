using System.Text;
using Newtonsoft.Json;

namespace MockServerApi.CLient;

public class MockServerClient
{
    private readonly HttpClient _httpClient;

    public string Url { get; set; }

    public MockServerClient(string url)
    {
        Url = url;
        _httpClient = new HttpClient { BaseAddress = new Uri(Url) };
    }

    public async Task ClearExpectationAsync(string expectationJson)
    {
        var content = new StringContent(expectationJson, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync("/mockserver/clear", content);
        response.EnsureSuccessStatusCode();
    }

    public async Task SendExpectationObjAsync(object expectationObj)
    {
        var json = JsonConvert.SerializeObject(expectationObj);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync("/mockserver/expectation", content);
        response.EnsureSuccessStatusCode();
    }

    public async Task<bool> VerifyExpectationCalledAsync(string path, int count = 1, bool exact = true)
    {
        // Implement verification logic using mock server API
        var query = $"/mockserver/verify?path={path}&count={count}&exact={exact}";
        var response = await _httpClient.GetAsync(query);
        return response.IsSuccessStatusCode;
    }
}