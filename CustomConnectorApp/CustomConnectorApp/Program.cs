using Moq;
using Newtonsoft.Json.Linq;
using System.Text;

namespace CustomConnectorApp
{
    class Program
    {

        static HttpClient _client = new HttpClient();
        static CancellationTokenSource _cts = new CancellationTokenSource();
        static async Task Main(string[] args)
        {

            await ScriptTest1();

        }

        static async Task ScriptTest1()
        {

            var moq = new Mock<IScriptContext>();
            moq.Setup(x => x.OperationId).Returns("GetUser");
            moq.Setup(x => x.CorrelationId).Returns("123456");

            var json = new JObject
            {
                ["message"] = "Hello World!"
            }.ToString();
            
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://api.contoso.com/Hello")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            
            moq.Setup(x => x.Request).Returns(requestMessage);

            moq.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .Returns(async (HttpRequestMessage r, CancellationToken c) => await _client.SendAsync(r, c));

            Script script = new Script()
            {
                CancellationToken = _cts.Token,
                Context = moq.Object
            };

            var response = await script.ExecuteAsync();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }


    }


}