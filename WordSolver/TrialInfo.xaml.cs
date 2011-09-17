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
    public partial class TrialInfo : PhoneApplicationPage
    {
        public TrialInfo()
        {
            InitializeComponent();
        }
        
        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            var marketplace = new MarketplaceDetailTask();
            marketplace.Show();
        }
    }
}