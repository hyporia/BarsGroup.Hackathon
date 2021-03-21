using System.Collections.Generic;

namespace BarsGroup.Hackathon.Core.Models.UserRequests.GetUsers
{
	public class GetUsersQueryResponse
	{
		public List<GetUserQueryResponseItem> Users { get; set; }
	}
}