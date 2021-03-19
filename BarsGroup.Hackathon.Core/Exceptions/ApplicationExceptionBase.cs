using System;
using System.Runtime.Serialization;

namespace BarsGroup.Hackathon.Core.Exceptions
{
	/// <summary>
	/// Исключение уровня приложения
	/// </summary>
	public class ApplicationExceptionBase : Exception
	{
		public string BaseExceptionMessage { get; }
		public string BaseExceptionStackTrace { get; }
		public string BaseExceptionType { get; }
		public string BaseInnerExceptionMessage { get; }
		public string BaseInnerExceptionStackTrace { get; }
		public string BaseInnerExceptionType { get; }

		public ApplicationExceptionBase()
		{
		}

		public ApplicationExceptionBase(string message) : base(message)
		{
		}

		protected ApplicationExceptionBase(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public ApplicationExceptionBase(string message, Exception innerException) : base(message, innerException)
		{
		}

		public ApplicationExceptionBase(Exception baseException, string errorMessage)
			: base($"{errorMessage}: {baseException.Message}")
		{
			BaseExceptionMessage = baseException.Message;
			BaseExceptionStackTrace = baseException.StackTrace;
			BaseExceptionType = baseException.GetType().FullName;
			BaseInnerExceptionMessage = baseException.InnerException?.Message;
			BaseInnerExceptionStackTrace = baseException.InnerException?.StackTrace;
			BaseInnerExceptionType = baseException.InnerException?.GetType()?.FullName;
		}
	}
}