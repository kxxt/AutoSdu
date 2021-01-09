using AutoSdu.Models;
using AutoSdu.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AutoSdu.ViewModels
{
	public class ItemsViewModel : BaseViewModel
	{

		public ItemsViewModel()
		{
			Title = "主页";
		}


		public void OnAppearing()
		{
			IsBusy = true;
		}
	}
}