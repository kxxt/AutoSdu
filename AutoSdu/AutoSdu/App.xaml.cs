using AutoSdu.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace AutoSdu
{
	public partial class App : Application
	{
		public string DataDir { get; private set; }
		public string ConfigFilePath { get; private set; }
		public App()
		{
			InitializeComponent();

			MainPage = new AppShell();
			DataDir = FileSystem.AppDataDirectory;
			ConfigFilePath = DataDir +Path.DirectorySeparatorChar+"config";
		}

		public (string,string) ReadConfig()
		{
			if (!File.Exists(ConfigFilePath))
			{
				File.Create(ConfigFilePath).Close();
				return ("", "");
			}
			var lines=File.ReadAllLines(ConfigFilePath);
			try
			{
				return (lines[0], lines[1]);
			}
			catch
			{
				SaveConfig("","");
				return ("", "");
			}
		}

		public void SaveConfig(string url, string username)
		{
			File.WriteAllText(ConfigFilePath,url+'\n'+username);
		}
		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
