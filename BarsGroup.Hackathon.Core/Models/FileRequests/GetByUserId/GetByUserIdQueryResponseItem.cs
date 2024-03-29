﻿using System;

namespace BarsGroup.Hackathon.Core.Models.FileRequests.GetByUserId
{
	public class GetByUserIdQueryResponseItem
	{
		public Guid Id { get; set; }
		public int Size { get; set; }
		public string Name { get; set; }
		public string ContentType { get; set; }
		public string Address { get; set; }
	}
}