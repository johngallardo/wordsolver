using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace WordSolver
{
    public partial class WordList : PhoneApplicationPage
    {
        public WordList()
        {
            InitializeComponent();
            this.Unloaded += WordList_Unloaded;
        }

        void _model_SearchCompleted(object sender, EventArgs e)
        {
            this.ProgressBar.Visibility = System.Windows.Visibility.Collapsed;
            UnwireEvents();
        }

        private void UnwireEvents()
        {
            if (_model != null)
            {
                _model.SearchCompleted -= new EventHandler(_model_SearchCompleted);
                _eventWired = false;
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (_model == null)
            {
                DataContext = _model = (App.Current as App).Model;
                _model.SearchBeginning += new EventHandler(_model_SearchCompleted);
                _eventWired = true;
            }
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {   
            if (_eventWired)
            {
                UnwireEvents();
            }
        }

        void WordList_Unloaded(object sender, RoutedEventArgs e)
        {
            DataContext = _model = null;
        }

        private AppViewModel _model;
        private bool _eventWired;

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/TrialInfo.xaml", UriKind.Relative));
        }
    }
}