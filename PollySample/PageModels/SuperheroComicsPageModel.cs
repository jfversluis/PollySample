using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using PollySample.Models;
using PollySample.Services;
using Xamarin.Forms;

namespace PollySample.PageModels
{
	public class SuperheroComicsPageModel : BasePageModel
	{
		private BackendService _backendService = new BackendService();
		private ICommand _refreshListCommand;

		public Superhero Superhero { get; }

		public ObservableCollection<Comic> Comics { get; set; }
			= new ObservableCollection<Comic>();

		public ICommand RefreshListCommand
		{
			get
			{
				return _refreshListCommand ?? (_refreshListCommand = new Command(async () => await RefreshList()));
			}
		}

		public SuperheroComicsPageModel(Superhero superhero)
		{
			Superhero = superhero;
			RefreshListCommand.Execute(null);
		}

		private async Task RefreshList()
		{
			IsLoading = true;

			await LoadComics();

			IsLoading = false;
		}

		private async Task LoadComics()
		{
			var comics = await _backendService.GetComicsForSuperhero(Superhero.Id);

			Comics.Clear();

			foreach (var comic in comics)
			{
				Comics.Add(comic);
			}
		}
	}
}