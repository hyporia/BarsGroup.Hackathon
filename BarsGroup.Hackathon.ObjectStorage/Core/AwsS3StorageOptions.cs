using System;

namespace BarsGroup.Hackathon.ObjectStorage.Core
{
	/// <summary>
	/// Конфигурация хранилища
	/// </summary>
	public class AwsS3StorageOptions
	{
		/// <summary>
		/// Ключ доступа
		/// </summary>
		public string AccessKeyId { get; set; }

		/// <summary>
		/// Секрет
		/// </summary>
		public string SecretAccessKey { get; set; }

		/// <summary>
		/// УРЛ хранилища
		/// </summary>
		public string ServiceUrl { get; set; }

		/// <summary>
		/// Название бакета
		/// </summary>
		public string BucketName { get; set; }

		/// <summary>
		/// Игнорить проблемы с сертификатом в S3
		/// </summary>
		public bool IgnoreCertificateErrors { get; set; } = true;

		/// <summary>
		/// Таймаут
		/// </summary>
		public TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(3);
	}
}