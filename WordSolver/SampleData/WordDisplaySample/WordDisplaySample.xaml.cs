﻿//      *********    DO NOT MODIFY THIS FILE     *********
//      This file is regenerated by a design tool. Making
//      changes to this file can cause errors.
namespace Expression.Blend.SampleData.WordDisplaySample
{
	using System; 

// To significantly reduce the sample data footprint in your production application, you can set
// the DISABLE_SAMPLE_DATA conditional compilation constant and disable sample data at runtime.
#if DISABLE_SAMPLE_DATA
	internal class WordDisplaySample { }
#else

	public class WordDisplaySample : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		public WordDisplaySample()
		{
			try
			{
				System.Uri resourceUri = new System.Uri("/WordSolver;component/SampleData/WordDisplaySample/WordDisplaySample.xaml", System.UriKind.Relative);
				if (System.Windows.Application.GetResourceStream(resourceUri) != null)
				{
					System.Windows.Application.LoadComponent(this, resourceUri);
				}
			}
			catch (System.Exception)
			{
			}
		}

		private string _Text = string.Empty;

		public string Text
		{
			get
			{
				return this._Text;
			}

			set
			{
				if (this._Text != value)
				{
					this._Text = value;
					this.OnPropertyChanged("Text");
				}
			}
		}
	}
#endif
}
