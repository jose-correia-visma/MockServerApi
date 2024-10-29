using MockServerApi.CLient;

namespace MockServerApi.MockServer
{
    public static class MockServerHelper
    {
        private static readonly string clearExpectationJson = "{\"path\": \"#ENV#/.*\"}";
        private static MockServerClient mockServerClient;
        private static string Environment { get; set; }

        public static void Initialize(string mockServerUrl, string environment)
        {
            mockServerClient = new MockServerClient(mockServerUrl);
            Environment = environment;
        }
        public static async Task ClearExpectationsAsync()
        {
            await mockServerClient.ClearExpectationAsync(ReplaceTokens(clearExpectationJson, null));
        }
        
        public static async Task SetExpectationAsyncFromJson(string json, Dictionary<string, string> dynamicTokens = null)
        {
            var expectationObj = MockJsonHelper.BuildMockObjFromStr(ReplaceTokens(json, dynamicTokens));
            await mockServerClient.SendExpectationObjAsync(expectationObj);
        }
        
        private static string ReplaceTokens(string input, Dictionary<string, string> dynamicTokens)
        {
            string text = input;
            if (dynamicTokens == null)
            {
                dynamicTokens = new Dictionary<string, string>();
            }

            dynamicTokens.Add("#ENV#", Environment);
            foreach (var token in dynamicTokens)
            {
                text = text.Replace(token.Key, token.Value);
            }

            return text;
        }
    }
}