using BarsGroup.Hackathon.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BarsGroup.Hackathon.Core.Abstractions
{
	public interface IAppDbContext
	{
		/// <summary>
		/// Файлы
		/// </summary>
		public DbSet<File> Files { get; set; }

		/// <summary>
		/// Файлы
		/// </summary>
		DbSet<IdentityUser<Guid>> Users { get; set; }

		/// <summary>
		/// Сохранить изменения в БД
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
