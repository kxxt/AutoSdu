using AutoSdu.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace AutoSdu.Views
{
	public partial class ItemDetailPage : ContentPage
	{
		public ItemDetailPage()
		{
			InitializeComponent();
			BindingContext = new ItemDetailViewModel();
		}
	}
}