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
using Microsoft.Phone.Tasks;

namespace WordSolver
{
    public partial class AboutPage : PhoneApplicationPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        public void ContactClick(object sender, RoutedEventArgs re)
        {
            var composer = new Microsoft.Phone.Tasks.EmailComposeTask();
            composer.Body = "WordSolver Version 1.0";
            composer.Subject = "WordSolver Feedback";
            composer.To = "johngallardo@live.com";
            composer.Show();
        }

        private void Review_Click(object sender, RoutedEventArgs e)
        {
            var market = new MarketplaceDetailTask();
            market.Show();
        }
    }
}