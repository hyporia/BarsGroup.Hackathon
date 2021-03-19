using System;
using System.Runtime.Serialization;

namespace BarsGroup.Hackathon.Core.Exceptions
{
	/// <summary>
	/// Исключение про некорректную конфигурацию сервиса
	/// </summary>
	public class InvalidConfigurationException : ApplicationExceptionBase
	{
		/// <inheritdoc/>
		public InvalidConfigurationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <inheritdoc/>
		public InvalidConfigurationException()
		{
		}

		/// <inheritdoc/>
		public InvalidConfigurationException(string message) : base(message)
		{
		}

		/// <inheritdoc/>
		public InvalidConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
