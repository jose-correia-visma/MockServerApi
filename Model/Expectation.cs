namespace MockServerApi.Model;

public class Expectation
{
    public string Method { get; set; }
    public string Url { get; set; }
    public string? RequestBody { get; set; } // Optional for POST methods
    public string ResponseBody { get; set; }
    public int ResponseStatus { get; set; } = 200;
    public string? SpecUrlOrPayload { get; set; }
}