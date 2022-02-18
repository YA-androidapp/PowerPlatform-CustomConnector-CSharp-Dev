using Newtonsoft.Json.Linq;

namespace CustomConnectorApp
{
    // https://docs.microsoft.com/en-us/connectors/custom-connectors/write-code

    public class Script : ScriptBase
    {
        public override async Task<HttpResponseMessage> ExecuteAsync()
        {
            // Create a new response
            var response = new HttpResponseMessage();

            // Set the content
            // Initialize a new JObject and call .ToString() to get the serialized JSON
            response.Content = CreateJsonContent(new JObject
            {
                ["greeting"] = "Hello World!",
            }.ToString());

            return response;
        }
    }
}
