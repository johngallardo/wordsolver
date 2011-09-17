using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            this.Model = (Application.Current as App).Model;
            this.DataContext = Model;
        }

        private void SolveClick(object sender, EventArgs e)
        {
            WorkaroundAppBarBug();
            Model.EnsureSolution();
            NavigationService.Navigate(new Uri("/WordList.xaml", UriKind.Relative));
        }

        public AppViewModel Model
        { get; set; }

        private void WorkaroundAppBarBug()
        {
            FocusStealer.Focus();
            tileTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            templateTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void AboutMenuClick(object sender, EventArgs e)
        {
            WorkaroundAppBarBug();
            NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
        }
    }
}
