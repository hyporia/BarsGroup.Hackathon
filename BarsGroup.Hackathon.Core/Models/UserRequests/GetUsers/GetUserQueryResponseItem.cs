using System;

namespace BarsGroup.Hackathon.Core.Models.UserRequests.GetUsers
{
	public class GetUserQueryResponseItem
	{
		public Guid Id { get; set; }
		public string Login { get; set; }
		public double DataSize { get; set; }
		public int FileNumber { get; set; }
	}
}