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
            _model = (App.Current as App).Model;

            _model.SearchCompleted += new EventHandler(_model_SearchCompleted);

            DataContext = _model;
            InitializeComponent();
        }

        void _model_SearchCompleted(object sender, EventArgs e)
        {
            this.ProgressBar.Visibility = System.Windows.Visibility.Collapsed;
            _model.SearchCompleted -= new EventHandler(_model_SearchCompleted);
        }

        private AppViewModel _model;

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/TrialInfo.xaml", UriKind.Relative));
        }
    }
}