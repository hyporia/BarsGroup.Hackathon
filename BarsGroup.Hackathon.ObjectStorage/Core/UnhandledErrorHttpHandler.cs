using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BarsGroup.Hackathon.ObjectStorage.Core
{
	/// <summary>
	/// Обработчик запросов в хранилище
	/// </summary>
	public class HttpRequestHandler : DelegatingHandler
	{
		private readonly ILogger<HttpRequestHandler> _logger;

		public HttpRequestHandler(ILogger<HttpRequestHandler> logger)
		{
			_logger = logger;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var requestLogData = new { request.Method, request.RequestUri };
			_logger.LogInformation("AwsS3 request {method}", requestLogData);
			var response = await base.SendAsync(request, cancellationToken);
			_logger.LogInformation("AwsS3 response {response} {request}", new { response.StatusCode, response.ReasonPhrase }, requestLogData);
			return response;
		}
	}
}