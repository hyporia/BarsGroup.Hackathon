using System.Collections.Generic;

namespace BarsGroup.Hackathon.Core.Models.FileRequests.GetByUserId
{
	public class GetByUserIdQueryResponse
	{
		public List<GetByUserIdQueryResponseItem> Files { get; set; }
	}
}
