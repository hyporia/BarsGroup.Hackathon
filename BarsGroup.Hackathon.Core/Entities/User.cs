using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BarsGroup.Hackathon.Core.Entities
{
	public class User : IdentityUser<Guid>
	{
		public List<File> Files { get; set; }
	}
}
