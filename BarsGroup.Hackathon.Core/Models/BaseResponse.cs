namespace BarsGroup.Hackathon.Core.Models
{
	public class BaseResponse<T> : BaseResponse
	{
		public T Result { get; set; }
	}

	public class BaseResponse
	{
		public string Error { get; set; }
	}
}
