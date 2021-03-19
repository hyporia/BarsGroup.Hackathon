using System.Net.Http;

namespace BarsGroup.Hackathon.ObjectStorage.Core
{
	/// <summary>
	/// Фабрика http-клиентов для взаимодействия с хранилищем
	/// </summary>
	public class AwsS3HttpClientFactory : Amazon.Runtime.HttpClientFactory
	{
		private readonly HttpClient _httpClient;

		public AwsS3HttpClientFactory(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public override HttpClient CreateHttpClient(Amazon.Runtime.IClientConfig clientConfig) => _httpClient;
	}
}