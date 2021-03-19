using BarsGroup.Hackathon.Core.Abstractions;
using BarsGroup.Hackathon.Core.Models;
using BarsGroup.Hackathon.ObjectStorage.Core;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BarsGroup.Hackathon.ObjectStorage.Services
{
	/// <summary>
	/// Хранилище файлов AwsS3
	/// </summary>
	public class ContentAwsS3Storage : IObjectStorage
	{
		private readonly AwsS3Service _awsS3Service;
		private readonly AwsS3StorageOptions _storageOptions;

		public ContentAwsS3Storage(AwsS3Service awsS3Service, AwsS3StorageOptions storageOptions)
		{
			_awsS3Service = awsS3Service;
			_storageOptions = storageOptions;
		}

		public async Task<ObjectPutResult> PutAsync(ObjectPutParams request, Stream stream)
		{
			var contentBucket = _storageOptions.BucketName;
			var extension = Path.GetExtension(request.FileName);
			var contentKey = $"{DateTime.UtcNow:yyyy-MM-dd}/{Guid.NewGuid()}{extension}";
			await _awsS3Service.UploadFileAsync(stream, request.ContentType, contentBucket, contentKey);
			return new ObjectPutResult(contentBucket, contentKey);
		}

		public async Task<byte[]> GetAsync(string contentKey, string bucket = null, CancellationToken cancellationToken = default)
		{
			bucket ??= _storageOptions.BucketName;
			return await _awsS3Service.DownloadFileAsync(bucket, contentKey, cancellationToken);
		}

		public async Task<Stream> GetStreamAsync(string contentKey, string bucket = null, CancellationToken cancellationToken = default)
		{
			bucket ??= _storageOptions.BucketName;
			return await _awsS3Service.GetFileAsStreamAsync(bucket, contentKey, cancellationToken);
		}

		public async Task RemoveAsync(string key, string bucket = null)
		{
			bucket ??= _storageOptions.BucketName;
			await _awsS3Service.RemoveFileAsync(bucket, key);
		}
	}
}