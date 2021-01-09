using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AutoSdu.ViewModels
{
	public class AboutViewModel : BaseViewModel
	{
		public AboutViewModel()
		{
			Title = "关于";
			OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://github.com/kxxt"));
		}

		public ICommand OpenWebCommand { get; }
	}
}