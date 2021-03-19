using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using BarsGroup.Hackathon.Core.Abstractions;
using BarsGroup.Hackathon.Core.Exceptions;
using BarsGroup.Hackathon.ObjectStorage.Core;
using BarsGroup.Hackathon.ObjectStorage.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace BarsGroup.Hackathon.ObjectStorage
{
	public static class Entry
	{
		public static IServiceCollection AddAwsS3Storage(this IServiceCollection serviceCollection, Action<AwsS3StorageOptions> storageOptionsAction)
		{
			var storageOptions = new AwsS3StorageOptions();
			storageOptionsAction?.Invoke(storageOptions);

			if (string.IsNullOrWhiteSpace(storageOptions.AccessKeyId))
				throw new InvalidConfigurationException(nameof(storageOptions.AccessKeyId));
			if (string.IsNullOrWhiteSpace(storageOptions.BucketName))
				throw new InvalidConfigurationException(nameof(storageOptions.BucketName));
			if (string.IsNullOrWhiteSpace(storageOptions.SecretAccessKey))
				throw new InvalidConfigurationException(nameof(storageOptions.SecretAccessKey));
			if (string.IsNullOrWhiteSpace(storageOptions.ServiceUrl))
				throw new InvalidConfigurationException(nameof(storageOptions.ServiceUrl));

			serviceCollection.AddSingleton(storageOptions);
			serviceCollection.AddTransient<HttpRequestHandler>();
			serviceCollection.AddSingleton<AwsS3Service>();
			serviceCollection.AddSingleton<IObjectStorage, ContentAwsS3Storage>();

			serviceCollection
				.AddHttpClient<AwsS3HttpClientFactory>(opt =>
				{
					opt.Timeout = storageOptions.Timeout;
				})
				.ConfigurePrimaryHttpMessageHandler(() =>
				{
					var handler = new HttpClientHandler();
					if (storageOptions.IgnoreCertificateErrors)
						handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
					return handler;
				})
				.AddHttpMessageHandler<HttpRequestHandler>()
				.Services
				.AddAWSService<IAmazonS3>(new AWSOptions
				{
					Credentials = new BasicAWSCredentials(storageOptions.AccessKeyId, storageOptions.SecretAccessKey),
					DefaultClientConfig =
					{
						ServiceURL = storageOptions.ServiceUrl,
						MaxErrorRetry = 0,
					},
				});

			return serviceCollection;
		}
	}
}
