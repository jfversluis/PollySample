using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using PollySample.Models;
using PollySample.Services;
using Xamarin.Forms;

namespace PollySample.PageModels
{
	public class MainPageModel : BasePageModel
	{
		private BackendService _backendService = new BackendService();
		private ICommand _refreshListCommand;

		public ObservableCollection<Superhero> Superheroes { get; set; }
			= new ObservableCollection<Superhero>();

		public ICommand RefreshListCommand
		{
			get
			{
				return _refreshListCommand ?? (_refreshListCommand = new Command(async () => await RefreshList()));
			}
		}

		public MainPageModel()
		{
			RefreshListCommand.Execute(null);
		}

		private async Task RefreshList()
		{
			IsLoading = true;

			await LoadSuperheroes();

			IsLoading = false;
		}

		private async Task LoadSuperheroes()
		{
			var superheroes = await _backendService.GetSuperheroes();

			Superheroes.Clear();

			foreach (var hero in superheroes)
			{
				Superheroes.Add(hero);
			}
		}
	}
}