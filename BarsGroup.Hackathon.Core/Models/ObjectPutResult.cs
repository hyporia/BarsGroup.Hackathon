namespace BarsGroup.Hackathon.Core.Models
{
	public class ObjectPutResult
	{
		public ObjectPutResult(string bucket, string key)
		{
			Bucket = bucket;
			Key = key;
		}

		public string Bucket { get; set; }

		public string Key { get; set; }
	}
}
