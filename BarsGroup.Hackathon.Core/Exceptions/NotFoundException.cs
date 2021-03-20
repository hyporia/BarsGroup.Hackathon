using System;
using System.Runtime.Serialization;
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable UnusedMember.Global

namespace BarsGroup.Hackathon.Core.Exceptions
{
	/// <summary>
	/// Исключение для обозначения, что какие-то данные не найдены
	/// </summary>
	public class NotFoundException : ApplicationExceptionBase
	{
		/// <summary>
		/// Исключение для обозначения, что какие-то данные не найдены
		/// </summary>
		public NotFoundException()
		{
		}

		/// <inheritdoc/>
		public NotFoundException(string message) : base(message)
		{
		}

		/// <inheritdoc/>
		protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <inheritdoc/>
		public NotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
