using Newtonsoft.Json;

namespace PollySample.Models
{
	public class Comic
	{
		[JsonProperty("superheroId")]
		public int superheroId { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }
	}
}