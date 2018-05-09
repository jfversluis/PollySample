using System.ComponentModel;

namespace PollySample.PageModels
{
	public class BasePageModel : INotifyPropertyChanged
	{
		public bool IsLoading { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;
	}
}