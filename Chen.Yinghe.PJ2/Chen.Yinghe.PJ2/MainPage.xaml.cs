using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Net;
using Xamarin.Forms;
using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace Chen.Yinghe.PJ2
{

	public partial class MainPage : ContentPage
	{
		class ImageList
		{
			public List<string> Photos = null;
		}

		/// <summary>
		/// is valued url?
		/// </summary>
		double time;
		String url;
		WebRequest request;
		List<string> PhotoList = null;
		int imageIndex = 0;
		public MainPage()
		{
			InitializeComponent();
			string resourceID = "Chen.Yinghe.PJ2.Image.image1.jpg";
			currentImage.Source = ImageSource.FromStream(() =>
			{
			Assembly assembly = GetType().GetTypeInfo().Assembly;
			Stream stream = assembly.GetManifestResourceStream(resourceID);
			return stream;
			});
			 
			///currentImage.Source = ImageSource.FromResource("Chen.Yinghe.PJ2.Image.image1.jpg");
		}
		void OnValueChanged(object sender, ValueChangedEventArgs args)
		{
			if (TimeEntry != null)
			{
				if (sender == TimeSlider)
				{
					TimeStepper.Value = args.NewValue;
					TimeEntry.Text = String.Format("{0}", args.NewValue);
					time = args.NewValue;

				}
				if (sender == TimeStepper)
				{
					TimeEntry.Text = String.Format("{0}", args.NewValue);
					TimeSlider.Value = args.NewValue;
					time = args.NewValue;
				}
				///  Task<bool> task = DisplayAlert("Simple Alert", "Decide on an option",
				///"yes or ok", "no or cancel");
			}
		}
		void OnEntryValueChanged(object sender, EventArgs args)
		{
			if (sender == TimeEntry)
			{
				double num = Double.Parse(TimeEntry.Text);
				if (num > 60 || num < 1)
				{
					Task task = DisplayAlert("Interval Time Error",
											 "Number must be in range of 1 to 60 exclusively.", "OK");

				}else
				{
					TimeStepper.Value = num;
					TimeSlider.Value = num;
					time = num;
				}
			}
		}
		void OnUrlChanged(object sender, EventArgs args)
		{
			url = "http://" + UrlEntry.Text;
			bool isUri = Uri.IsWellFormedUriString(url, UriKind.Absolute);
			if (!isUri)
			{
				Task task = DisplayAlert("URL Format Error",
											 "You URL must be absolute like www.example.com", "OK");
				UrlEntry.Text = "";
			}
			else 
			{
				request = WebRequest.Create(url);
				request.BeginGetResponse(WebRequestCallback, null);
			}
		}
		void WebRequestCallback(IAsyncResult result)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				try
				{
					///Stream stream = request.EndGetResponse(result).GetResponseStream();
					///var jsonSerializer = new DataContractJsonSerializer(typeof(ImageList));

				}
				catch (Exception exc)
				{
				}
			});
		}
		void OnImagePropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			if (args.PropertyName == "Isloading")
			{
				activityIndicator.IsRunning = ((Image)sender).IsLoading;
			}
		}
		void OnSwitch(object sender, ToggledEventArgs args)
		{
			if (args.Value) { }
			else { }
		}
	}
}
