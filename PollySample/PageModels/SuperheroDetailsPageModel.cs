using PollySample.Models;

namespace PollySample.PageModels
{
	public class SuperheroDetailsPageModel : BasePageModel
	{
		public Superhero Superhero { get; }

		public SuperheroDetailsPageModel(Superhero superhero)
		{
			Superhero = superhero;
		}
	}
}