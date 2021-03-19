using BarsGroup.Hackathon.Core.Models;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BarsGroup.Hackathon.Core.Abstractions
{
	public interface IObjectStorage
	{
		/// <summary>
		/// Сохранить файл
		/// </summary>
		/// <param name="request">Атрибуты файла</param>
		/// <param name="stream">Байтовый поток файла</param>
		/// <returns>Результат сохранения файла</returns>
		Task<ObjectPutResult> PutAsync(ObjectPutParams request, Stream stream);

		/// <summary>
		/// Получить файл в виде массива байтов
		/// </summary>
		/// <param name="contentKey">Ключ файла в хранилище.</param>
		/// <param name="bucket">Бакет, в котором хранится файл. Если он не задан, то название берется из конфигурации приложения</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>Массив байтов</returns>
		Task<byte[]> GetAsync(string contentKey, string bucket = null, CancellationToken cancellationToken = default);

		/// <summary>
		/// Получить поток байтов файла
		/// </summary>
		/// <param name="contentKey">Ключ файла в хранилище.</param>
		/// <param name="bucket">Бакет, в котором хранится файл. Если он не задан, то название берется из конфигурации приложения</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>Массив байтов</returns>
		Task<Stream> GetStreamAsync(string contentKey, string bucket = null, CancellationToken cancellationToken = default);

		/// <summary>
		/// Удалить файл
		/// </summary>
		/// <param name="key">Ключ файла в хранилище</param>
		/// <param name="bucket">Бакет, в котором хранится файл. Если он не задан, то название берется из конфигурации приложения</param>
		// ReSharper disable UnusedMember.Global
		Task RemoveAsync(string key, string bucket = null);
	}
}
