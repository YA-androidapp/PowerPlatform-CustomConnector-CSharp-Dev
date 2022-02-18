using Microsoft.Extensions.Logging;
using System.Text;

namespace CustomConnectorApp
{
    public abstract class ScriptBase
    {
        public IScriptContext Context { get; init; }

        public CancellationToken CancellationToken { get; init; }

        public static StringContent CreateJsonContent(string serializedJson)
        {
            return new StringContent(serializedJson, Encoding.UTF8, "application/json");
        }

        public abstract Task<HttpResponseMessage> ExecuteAsync();
    }

    public interface IScriptContext
    {
        string CorrelationId { get; }

        string OperationId { get; }

        HttpRequestMessage Request { get; }

        ILogger Logger { get; }

        Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken);
    }
}
