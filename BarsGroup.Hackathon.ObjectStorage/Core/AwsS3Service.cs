using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BarsGroup.Hackathon.ObjectStorage.Core
{
	/// <summary>
	/// Работа с файловым хранилищем
	/// </summary>
	public class AwsS3Service
	{
		private readonly IAmazonS3 _client;

		public AwsS3Service(IAmazonS3 client, AwsS3HttpClientFactory factory)
		{
			_client = client;
			var amazonS3Config = (AmazonS3Config)_client.Config;
			amazonS3Config.HttpClientFactory = factory;
			amazonS3Config.ForcePathStyle = true;
		}

		public async Task UploadFileAsync(byte[] content, string contentType, string bucket, string key)
		{
			using var fileToUpload = new MemoryStream(content);
			await UploadFileAsync(fileToUpload, contentType, bucket, key);
		}

		public async Task UploadFileAsync(Stream stream, string contentType, string bucket, string key)
		{
			var putRequest = new PutObjectRequest
			{
				BucketName = bucket,
				Key = key,
				InputStream = stream,
				ContentType = contentType
			};

			await _client.PutObjectAsync(putRequest);
		}

		public async Task<byte[]> DownloadFileAsync(string bucket, string key, CancellationToken cancellationToken = default)
		{
			using var response = await _client.GetObjectStreamAsync(bucket, key, null, cancellationToken);
			return StreamToByteArray(response);
		}

		public async Task<Stream> GetFileAsStreamAsync(string bucket, string key, CancellationToken cancellationToken = default) =>
			await _client.GetObjectStreamAsync(bucket, key, null, cancellationToken);


		private byte[] StreamToByteArray(Stream stream)
		{
			byte[] buffer = new byte[stream.Length];
			for (int totalBytesCopied = 0; totalBytesCopied < stream.Length;)
				totalBytesCopied += stream.Read(buffer, totalBytesCopied, Convert.ToInt32(stream.Length) - totalBytesCopied);
			return buffer;
		}

		public async Task RemoveFileAsync(string bucket, string key)
		{
			var request = new DeleteObjectRequest
			{
				BucketName = bucket,
				Key = key
			};
			await _client.DeleteObjectAsync(request);
		}

		public async Task<List<string>> ListBucketsAsync(CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var request = new ListBucketsRequest();
			var response = await _client.ListBucketsAsync(request, cancellationToken);
			return response?.Buckets?.Select(x => x.BucketName).ToList() ?? new List<string>();
		}
	}
}