using Android.Provider;
using Android.Media;
using AutoSdu.Models;
using AutoSdu.ViewModels;
using AutoSdu.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace AutoSdu.Views
{
	public partial class MainPage : ContentPage
	{
		ItemsViewModel _viewModel;

		public const string ImagePath = "/mnt/sdcard/AutoSdu/data.png";
		public MainPage()
		{
			InitializeComponent();
			BindingContext = _viewModel = new ItemsViewModel();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			_viewModel.OnAppearing();
			LoadConfig();
			Debug.WriteLine((App.Current as App).ConfigFilePath);
		}

		private void LoadConfig()
		{
			var cfg = (App.Current as App).ReadConfig();
			URL.Text = cfg.Item1;
			Username.Text = cfg.Item2;
		}

		private void RetrieveScreenShot(object sender, EventArgs e)
		{
			var src = new UriImageSource();
			src.Uri = new Uri(URL.Text + (URL.Text.EndsWith("/") ? "" : "/") + Username.Text + ".png");
			//Debug.WriteLine(src.Uri.AbsolutePath);
			Image.Source = src;
		}

		private async void SaveToDCIM(object sender, EventArgs e)
		{
			WebClient wc = new WebClient();
			if (!Directory.Exists("/mnt/sdcard/AutoSdu")) Directory.CreateDirectory("/mnt/sdcard/AutoSdu");
			bool success = false;
			for (int i = 0; i <= 5; i++)
			{
				try
				{
					await wc.DownloadFileTaskAsync(new Uri(URL.Text + (URL.Text.EndsWith("/") ? "" : "/") + Username.Text + ".png"), 
						Path.Combine((App.Current as App).DataDir, ImagePath));
					success = true;
					break;
				}

				catch(Exception ex)
				{
					
				}
			}

			if (!success)
			{
				await DisplayAlert("获取并保存图像失败", "在获取图像或保存到本地时出错\r\n请检查程序是否具有存储权限且当前网络连接良好.\r\n请检查URL和username是否正确.","确定");
				if (sender == null) await DisplayAlert("提示", "因下载失败, 将使用上次下载的截图", "确定");
			}
			else
			{
				var s = new FileImageSource();
				s.File = ImagePath;
				Image.Source = s;
				if (sender!=null)
					await DisplayAlert("保存成功", "已保存到AutoSdu/data.png","确定");
			}
		}

		private async void Share(object sender, EventArgs e)
		{
			SaveToDCIM(null,null);
			if (!File.Exists(ImagePath)) return;
			await Xamarin.Essentials.Share.RequestAsync(new ShareFileRequest
			{
				Title = Title,
				File = new ShareFile(ImagePath)
			});
		}

		private void UpdateConfig()
		{
			(App.Current as App).SaveConfig(URL.Text, Username.Text);
		}
		private void URL_TextChanged(object sender, TextChangedEventArgs e)
		{
			UpdateConfig();
		}

		private void Username_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			UpdateConfig();
		}
	}
}