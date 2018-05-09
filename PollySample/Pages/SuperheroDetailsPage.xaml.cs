using System;
using PollySample.PageModels;
using Xamarin.Forms;

namespace PollySample.Pages
{
	public partial class SuperheroDetailsPage : ContentPage
	{
		public SuperheroDetailsPage()
		{
			InitializeComponent();
		}

		public void Handle_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new SuperheroComicsPage
			{
				BindingContext = new SuperheroComicsPageModel(
					(BindingContext as SuperheroDetailsPageModel).Superhero)
			});
		}
	}
}